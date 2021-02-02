﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Quizzing.Web.Data;

namespace Quizzing.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210202175500_InitialIdentity")]
    partial class InitialIdentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Quizzing.Web.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AnswerText")
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            AnswerId = 1,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 1
                        },
                        new
                        {
                            AnswerId = 2,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 1
                        },
                        new
                        {
                            AnswerId = 3,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 1
                        },
                        new
                        {
                            AnswerId = 4,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 1
                        },
                        new
                        {
                            AnswerId = 5,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 2
                        },
                        new
                        {
                            AnswerId = 6,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 2
                        },
                        new
                        {
                            AnswerId = 7,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 2
                        },
                        new
                        {
                            AnswerId = 8,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 2
                        },
                        new
                        {
                            AnswerId = 9,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 3
                        },
                        new
                        {
                            AnswerId = 10,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 3
                        },
                        new
                        {
                            AnswerId = 11,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 3
                        },
                        new
                        {
                            AnswerId = 12,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 3
                        },
                        new
                        {
                            AnswerId = 13,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 4
                        },
                        new
                        {
                            AnswerId = 14,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 4
                        },
                        new
                        {
                            AnswerId = 15,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 4
                        },
                        new
                        {
                            AnswerId = 16,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 4
                        },
                        new
                        {
                            AnswerId = 17,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 5
                        },
                        new
                        {
                            AnswerId = 18,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 5
                        },
                        new
                        {
                            AnswerId = 19,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 5
                        },
                        new
                        {
                            AnswerId = 20,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 5
                        },
                        new
                        {
                            AnswerId = 21,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 6
                        },
                        new
                        {
                            AnswerId = 22,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 6
                        },
                        new
                        {
                            AnswerId = 23,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 6
                        },
                        new
                        {
                            AnswerId = 24,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 6
                        },
                        new
                        {
                            AnswerId = 25,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 7
                        },
                        new
                        {
                            AnswerId = 26,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 7
                        },
                        new
                        {
                            AnswerId = 27,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 7
                        },
                        new
                        {
                            AnswerId = 28,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 7
                        },
                        new
                        {
                            AnswerId = 29,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 8
                        },
                        new
                        {
                            AnswerId = 30,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 8
                        },
                        new
                        {
                            AnswerId = 31,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 8
                        },
                        new
                        {
                            AnswerId = 32,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 8
                        },
                        new
                        {
                            AnswerId = 33,
                            AnswerText = "Answer 1",
                            IsCorrect = false,
                            QuestionId = 9
                        },
                        new
                        {
                            AnswerId = 34,
                            AnswerText = "Answer 2",
                            IsCorrect = false,
                            QuestionId = 9
                        },
                        new
                        {
                            AnswerId = 35,
                            AnswerText = "Answer 3",
                            IsCorrect = false,
                            QuestionId = 9
                        },
                        new
                        {
                            AnswerId = 36,
                            AnswerText = "Answer 4",
                            IsCorrect = false,
                            QuestionId = 9
                        });
                });

            modelBuilder.Entity("Quizzing.Web.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("QuestionText")
                        .HasColumnType("text");

                    b.Property<int>("QuizId")
                        .HasColumnType("integer");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            QuestionId = 1,
                            QuestionText = "Question 1",
                            QuizId = 1
                        },
                        new
                        {
                            QuestionId = 2,
                            QuestionText = "Question 2",
                            QuizId = 1
                        },
                        new
                        {
                            QuestionId = 3,
                            QuestionText = "Question 3",
                            QuizId = 1
                        },
                        new
                        {
                            QuestionId = 4,
                            QuestionText = "Question 1",
                            QuizId = 2
                        },
                        new
                        {
                            QuestionId = 5,
                            QuestionText = "Question 2",
                            QuizId = 2
                        },
                        new
                        {
                            QuestionId = 6,
                            QuestionText = "Question 3",
                            QuizId = 2
                        },
                        new
                        {
                            QuestionId = 7,
                            QuestionText = "Question 1",
                            QuizId = 3
                        },
                        new
                        {
                            QuestionId = 8,
                            QuestionText = "Question 2",
                            QuizId = 3
                        },
                        new
                        {
                            QuestionId = 9,
                            QuestionText = "Question 3",
                            QuizId = 3
                        });
                });

            modelBuilder.Entity("Quizzing.Web.Models.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("QuizId");

                    b.ToTable("Quizzes");

                    b.HasData(
                        new
                        {
                            QuizId = 1,
                            Title = "Quiz 1"
                        },
                        new
                        {
                            QuizId = 2,
                            Title = "Quiz 2"
                        },
                        new
                        {
                            QuizId = 3,
                            Title = "Quiz 3"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Quizzing.Web.Models.Answer", b =>
                {
                    b.HasOne("Quizzing.Web.Models.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Quizzing.Web.Models.Question", b =>
                {
                    b.HasOne("Quizzing.Web.Models.Quiz", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
