﻿// <auto-generated />
using System;
using API_Portofolio.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_Portofolio.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250210171242_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API_Portofolio.Database.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("AcceptedTerms")
                        .HasColumnType("boolean");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("RefreshTokenExpiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Chat", b =>
                {
                    b.Property<Guid>("Chat_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("ChatTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Chat_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Chat_ReferenceCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CretedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdUser_Secondary")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastAccess")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Chat_Id");

                    b.HasIndex("ChatTypeId");

                    b.HasIndex("IdUser");

                    b.HasIndex("IdUser_Secondary");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.ChatType", b =>
                {
                    b.Property<Guid>("ChatTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("ChatType_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ChatTypeId");

                    b.ToTable("ChatTypes");

                    b.HasData(
                        new
                        {
                            ChatTypeId = new Guid("60bf9886-9060-4c86-a1c8-85ab67446efa"),
                            ChatType_Name = "Personal Conversation"
                        },
                        new
                        {
                            ChatTypeId = new Guid("5bfb2b27-7424-4500-a2cd-d216771307c1"),
                            ChatType_Name = "Order"
                        });
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.HostingPreference", b =>
                {
                    b.Property<Guid>("HostingPreference_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("HostingPreference_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("HostingPreference_ReferenceCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.HasKey("HostingPreference_Id");

                    b.HasIndex("HostingPreference_ReferenceCode")
                        .IsUnique();

                    b.ToTable("HostingPreferences");

                    b.HasData(
                        new
                        {
                            HostingPreference_Id = new Guid("40ec495d-d114-48b5-9163-01b4ca87dc9d"),
                            HostingPreference_Name = "Cloud",
                            HostingPreference_ReferenceCode = new Guid("3a459302-ab60-4382-ad2e-a6066aff4432")
                        },
                        new
                        {
                            HostingPreference_Id = new Guid("d7ed8483-e2f0-4f95-a50d-faa1f8925a20"),
                            HostingPreference_Name = "On-Premise",
                            HostingPreference_ReferenceCode = new Guid("14aab7e2-4cea-4073-88f5-68b08612b446")
                        });
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Message", b =>
                {
                    b.Property<Guid>("Message_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("Chat_Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Message_ReferenceCode")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Message_SendTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message_Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Message_Id");

                    b.HasIndex("Chat_Id");

                    b.HasIndex("UserID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Order", b =>
                {
                    b.Property<Guid>("Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Order_IdUser")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Order_ReferenceCode")
                        .HasColumnType("uuid");

                    b.HasKey("Order_Id");

                    b.HasIndex("Order_IdUser");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepFourOrder", b =>
                {
                    b.Property<Guid>("StepFourOrder_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("CollaborationWorkflow")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("HostingPreference_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("LegalConstraints")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Order_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("PreferredTechnologies")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("StepFourOrder_Id");

                    b.HasIndex("HostingPreference_Id");

                    b.HasIndex("Order_Id")
                        .IsUnique();

                    b.ToTable("StepFourOrders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepOneOrder", b =>
                {
                    b.Property<Guid>("StepOneOrder_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("CurrentChallengesOrProblems")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MainPurpose")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Order_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("TargetAudience")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TypeOfApplication_Id")
                        .HasColumnType("uuid");

                    b.HasKey("StepOneOrder_Id");

                    b.HasIndex("Order_Id")
                        .IsUnique();

                    b.HasIndex("TypeOfApplication_Id");

                    b.ToTable("StepOneOrders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepThreeOrder", b =>
                {
                    b.Property<Guid>("StepThreeOrder_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("AccessibilityNeeds")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomizationOptions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DesignPreferences")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EndUserDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Order_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("UsageContext")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("StepThreeOrder_Id");

                    b.HasIndex("Order_Id")
                        .IsUnique();

                    b.ToTable("StepThreeOrders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepTwoOrder", b =>
                {
                    b.Property<Guid>("StepTwoOrder_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("IntegrationWithExternalSystems")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KeyFeatures")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Order_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("SecurityRequirements")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SuportedPlatform_Id")
                        .HasColumnType("uuid");

                    b.HasKey("StepTwoOrder_Id");

                    b.HasIndex("Order_Id")
                        .IsUnique();

                    b.HasIndex("SuportedPlatform_Id");

                    b.ToTable("StepTwoOrders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.SuportedPlatform", b =>
                {
                    b.Property<Guid>("SuportedPlatform_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("SuportedPlatform_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SuportedPlatform_ReferenceCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.HasKey("SuportedPlatform_Id");

                    b.HasIndex("SuportedPlatform_ReferenceCode")
                        .IsUnique();

                    b.ToTable("SuportedPlatforms");

                    b.HasData(
                        new
                        {
                            SuportedPlatform_Id = new Guid("5c6d0b42-eec4-481e-80d3-786c82d725dc"),
                            SuportedPlatform_Name = "Android",
                            SuportedPlatform_ReferenceCode = new Guid("024cfbca-eb5f-43d6-8e35-e4f6ab81a6fa")
                        },
                        new
                        {
                            SuportedPlatform_Id = new Guid("4e1065a7-97d2-4dba-9f9d-3fac61b7435d"),
                            SuportedPlatform_Name = "IOS",
                            SuportedPlatform_ReferenceCode = new Guid("e4028817-8c05-4bc0-a4e6-bf03e093d0af")
                        },
                        new
                        {
                            SuportedPlatform_Id = new Guid("9d216415-036a-4e0f-9c65-4ef34995cd0a"),
                            SuportedPlatform_Name = "WEB Browser",
                            SuportedPlatform_ReferenceCode = new Guid("c8a504d4-b4fa-4b22-ac83-4ca5017f0fcf")
                        },
                        new
                        {
                            SuportedPlatform_Id = new Guid("05133f57-3508-4373-9e96-8edd5131764b"),
                            SuportedPlatform_Name = "Windows",
                            SuportedPlatform_ReferenceCode = new Guid("f2afae61-0d04-4e11-984f-ff7db3b13995")
                        },
                        new
                        {
                            SuportedPlatform_Id = new Guid("d1e46fd5-e0ec-4163-aedd-f29c452b6549"),
                            SuportedPlatform_Name = "Mac",
                            SuportedPlatform_ReferenceCode = new Guid("6e032983-e444-4b87-b7e6-d6676372bda8")
                        });
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.TypeOfApplication", b =>
                {
                    b.Property<Guid>("TypeOfApplication_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("TypeOfApplication_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TypeOfApplication_ReferenceCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.HasKey("TypeOfApplication_Id");

                    b.HasIndex("TypeOfApplication_ReferenceCode")
                        .IsUnique();

                    b.ToTable("TypeOfApplications");

                    b.HasData(
                        new
                        {
                            TypeOfApplication_Id = new Guid("95937444-283b-4b54-b391-53f965978806"),
                            TypeOfApplication_Name = "Web Application",
                            TypeOfApplication_ReferenceCode = new Guid("a6f02127-1525-44ff-a400-7dc52694b953")
                        },
                        new
                        {
                            TypeOfApplication_Id = new Guid("9a2b3f8c-8ed8-44f1-b9a6-e85262661b21"),
                            TypeOfApplication_Name = "Mobile Application",
                            TypeOfApplication_ReferenceCode = new Guid("c8f4b8cc-8b78-4e8d-9cb4-e0d3c4e28f4a")
                        },
                        new
                        {
                            TypeOfApplication_Id = new Guid("283eac3a-2c9a-438d-88da-b9f95bf1b672"),
                            TypeOfApplication_Name = "Desktop Application",
                            TypeOfApplication_ReferenceCode = new Guid("a3291dee-630d-4fd3-ba82-8645cd3d0b27")
                        },
                        new
                        {
                            TypeOfApplication_Id = new Guid("382539cf-215a-4763-a329-8bed44ca9ef8"),
                            TypeOfApplication_Name = "SaaS",
                            TypeOfApplication_ReferenceCode = new Guid("246796ff-9ccc-4c00-9e2e-335694d5671a")
                        },
                        new
                        {
                            TypeOfApplication_Id = new Guid("f78f5ba8-7ac6-4e0c-b435-0a73664b3a54"),
                            TypeOfApplication_Name = "IoT Application",
                            TypeOfApplication_ReferenceCode = new Guid("87b81a02-84a4-4919-91ee-58be6e924279")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
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

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
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

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Chat", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.ChatType", "ChatType")
                        .WithMany("ChatList")
                        .HasForeignKey("ChatTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Portofolio.Database.Entities.Account", "User1")
                        .WithMany("ChatsAsUser1")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API_Portofolio.Database.Entities.Account", "User2")
                        .WithMany("ChatsAsUser2")
                        .HasForeignKey("IdUser_Secondary")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChatType");

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Message", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("Chat_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Portofolio.Database.Entities.Account", "Account")
                        .WithMany("Messages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Order", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("Order_IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepFourOrder", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.HostingPreference", "HostingPreference")
                        .WithMany("StepFourOrders")
                        .HasForeignKey("HostingPreference_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Portofolio.Database.Entities.Order", "Order")
                        .WithOne("StepFourOrder")
                        .HasForeignKey("API_Portofolio.Database.Entities.StepFourOrder", "Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HostingPreference");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepOneOrder", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.Order", "Order")
                        .WithOne("StepOneOrder")
                        .HasForeignKey("API_Portofolio.Database.Entities.StepOneOrder", "Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Portofolio.Database.Entities.TypeOfApplication", "TypeOfApplication")
                        .WithMany("StepOneOrders")
                        .HasForeignKey("TypeOfApplication_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("TypeOfApplication");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepThreeOrder", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.Order", "Order")
                        .WithOne("StepThreeOrder")
                        .HasForeignKey("API_Portofolio.Database.Entities.StepThreeOrder", "Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.StepTwoOrder", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.Order", "Order")
                        .WithOne("StepTwoOrder")
                        .HasForeignKey("API_Portofolio.Database.Entities.StepTwoOrder", "Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Portofolio.Database.Entities.SuportedPlatform", "SuportedPlatform")
                        .WithMany("StepTwoOrders")
                        .HasForeignKey("SuportedPlatform_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("SuportedPlatform");
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
                    b.HasOne("API_Portofolio.Database.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.Account", null)
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

                    b.HasOne("API_Portofolio.Database.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("API_Portofolio.Database.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Account", b =>
                {
                    b.Navigation("ChatsAsUser1");

                    b.Navigation("ChatsAsUser2");

                    b.Navigation("Messages");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.ChatType", b =>
                {
                    b.Navigation("ChatList");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.HostingPreference", b =>
                {
                    b.Navigation("StepFourOrders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.Order", b =>
                {
                    b.Navigation("StepFourOrder")
                        .IsRequired();

                    b.Navigation("StepOneOrder")
                        .IsRequired();

                    b.Navigation("StepThreeOrder")
                        .IsRequired();

                    b.Navigation("StepTwoOrder")
                        .IsRequired();
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.SuportedPlatform", b =>
                {
                    b.Navigation("StepTwoOrders");
                });

            modelBuilder.Entity("API_Portofolio.Database.Entities.TypeOfApplication", b =>
                {
                    b.Navigation("StepOneOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
