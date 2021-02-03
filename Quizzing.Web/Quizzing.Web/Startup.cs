using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Quizzing.Web.Data;
using Quizzing.Web.Validators;

namespace Quizzing.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    _configuration.GetConnectionString("AppConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddControllersWithViews()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<QuizValidator>();
                    config.RegisterValidatorsFromAssemblyContaining<QuestionValidator>();
                    config.RegisterValidatorsFromAssemblyContaining<AnswerValidator>();
                    config.ImplicitlyValidateChildProperties = true;
                });

            services.AddControllersWithViews();

            // For Authorisation -- building and using a policy
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("edit",
                    policy => policy.RequireRole("edit"));
                options.AddPolicy("view",
                    policy => policy.RequireRole("view"));
                options.AddPolicy("restricted",
                    policy => policy.RequireRole("restricted"));
            });

            services.AddScoped<AppDbContext>();
            services.AddScoped<IQuizRepository, QuizRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Quizzes}/{action=Index}/{id?}");
            });
        }
    }
}
