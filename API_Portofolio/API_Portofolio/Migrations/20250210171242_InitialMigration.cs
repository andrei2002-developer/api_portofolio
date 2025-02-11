using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Portofolio.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    AcceptedTerms = table.Column<bool>(type: "boolean", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatTypes",
                columns: table => new
                {
                    ChatTypeId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChatType_Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatTypes", x => x.ChatTypeId);
                });

            migrationBuilder.CreateTable(
                name: "HostingPreferences",
                columns: table => new
                {
                    HostingPreference_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    HostingPreference_ReferenceCode = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    HostingPreference_Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostingPreferences", x => x.HostingPreference_Id);
                });

            migrationBuilder.CreateTable(
                name: "SuportedPlatforms",
                columns: table => new
                {
                    SuportedPlatform_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SuportedPlatform_ReferenceCode = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SuportedPlatform_Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuportedPlatforms", x => x.SuportedPlatform_Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfApplications",
                columns: table => new
                {
                    TypeOfApplication_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TypeOfApplication_ReferenceCode = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TypeOfApplication_Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfApplications", x => x.TypeOfApplication_Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Order_ReferenceCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Order_IdUser = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_Order_IdUser",
                        column: x => x.Order_IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Chat_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Chat_ReferenceCode = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Chat_Name = table.Column<string>(type: "text", nullable: false),
                    CretedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChatTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: false),
                    IdUser_Secondary = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Chat_Id);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_IdUser_Secondary",
                        column: x => x.IdUser_Secondary,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_ChatTypes_ChatTypeId",
                        column: x => x.ChatTypeId,
                        principalTable: "ChatTypes",
                        principalColumn: "ChatTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepFourOrders",
                columns: table => new
                {
                    StepFourOrder_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Order_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PreferredTechnologies = table.Column<string>(type: "text", nullable: false),
                    HostingPreference_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaborationWorkflow = table.Column<string>(type: "text", nullable: false),
                    LegalConstraints = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepFourOrders", x => x.StepFourOrder_Id);
                    table.ForeignKey(
                        name: "FK_StepFourOrders_HostingPreferences_HostingPreference_Id",
                        column: x => x.HostingPreference_Id,
                        principalTable: "HostingPreferences",
                        principalColumn: "HostingPreference_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepFourOrders_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepOneOrders",
                columns: table => new
                {
                    StepOneOrder_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Order_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeOfApplication_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetAudience = table.Column<string>(type: "text", nullable: false),
                    CurrentChallengesOrProblems = table.Column<string>(type: "text", nullable: false),
                    MainPurpose = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepOneOrders", x => x.StepOneOrder_Id);
                    table.ForeignKey(
                        name: "FK_StepOneOrders_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepOneOrders_TypeOfApplications_TypeOfApplication_Id",
                        column: x => x.TypeOfApplication_Id,
                        principalTable: "TypeOfApplications",
                        principalColumn: "TypeOfApplication_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepThreeOrders",
                columns: table => new
                {
                    StepThreeOrder_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Order_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EndUserDescription = table.Column<string>(type: "text", nullable: false),
                    UsageContext = table.Column<string>(type: "text", nullable: false),
                    DesignPreferences = table.Column<string>(type: "text", nullable: false),
                    AccessibilityNeeds = table.Column<string>(type: "text", nullable: false),
                    CustomizationOptions = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepThreeOrders", x => x.StepThreeOrder_Id);
                    table.ForeignKey(
                        name: "FK_StepThreeOrders_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepTwoOrders",
                columns: table => new
                {
                    StepTwoOrder_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Order_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KeyFeatures = table.Column<string>(type: "text", nullable: false),
                    IntegrationWithExternalSystems = table.Column<string>(type: "text", nullable: false),
                    SuportedPlatform_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SecurityRequirements = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepTwoOrders", x => x.StepTwoOrder_Id);
                    table.ForeignKey(
                        name: "FK_StepTwoOrders_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepTwoOrders_SuportedPlatforms_SuportedPlatform_Id",
                        column: x => x.SuportedPlatform_Id,
                        principalTable: "SuportedPlatforms",
                        principalColumn: "SuportedPlatform_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Message_Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Message_SendTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message_Text = table.Column<string>(type: "text", nullable: false),
                    Message_ReferenceCode = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: false),
                    Chat_Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Message_Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_Chat_Id",
                        column: x => x.Chat_Id,
                        principalTable: "Chats",
                        principalColumn: "Chat_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChatTypes",
                columns: new[] { "ChatTypeId", "ChatType_Name" },
                values: new object[,]
                {
                    { new Guid("5bfb2b27-7424-4500-a2cd-d216771307c1"), "Order" },
                    { new Guid("60bf9886-9060-4c86-a1c8-85ab67446efa"), "Personal Conversation" }
                });

            migrationBuilder.InsertData(
                table: "HostingPreferences",
                columns: new[] { "HostingPreference_Id", "HostingPreference_Name", "HostingPreference_ReferenceCode" },
                values: new object[,]
                {
                    { new Guid("40ec495d-d114-48b5-9163-01b4ca87dc9d"), "Cloud", new Guid("3a459302-ab60-4382-ad2e-a6066aff4432") },
                    { new Guid("d7ed8483-e2f0-4f95-a50d-faa1f8925a20"), "On-Premise", new Guid("14aab7e2-4cea-4073-88f5-68b08612b446") }
                });

            migrationBuilder.InsertData(
                table: "SuportedPlatforms",
                columns: new[] { "SuportedPlatform_Id", "SuportedPlatform_Name", "SuportedPlatform_ReferenceCode" },
                values: new object[,]
                {
                    { new Guid("05133f57-3508-4373-9e96-8edd5131764b"), "Windows", new Guid("f2afae61-0d04-4e11-984f-ff7db3b13995") },
                    { new Guid("4e1065a7-97d2-4dba-9f9d-3fac61b7435d"), "IOS", new Guid("e4028817-8c05-4bc0-a4e6-bf03e093d0af") },
                    { new Guid("5c6d0b42-eec4-481e-80d3-786c82d725dc"), "Android", new Guid("024cfbca-eb5f-43d6-8e35-e4f6ab81a6fa") },
                    { new Guid("9d216415-036a-4e0f-9c65-4ef34995cd0a"), "WEB Browser", new Guid("c8a504d4-b4fa-4b22-ac83-4ca5017f0fcf") },
                    { new Guid("d1e46fd5-e0ec-4163-aedd-f29c452b6549"), "Mac", new Guid("6e032983-e444-4b87-b7e6-d6676372bda8") }
                });

            migrationBuilder.InsertData(
                table: "TypeOfApplications",
                columns: new[] { "TypeOfApplication_Id", "TypeOfApplication_Name", "TypeOfApplication_ReferenceCode" },
                values: new object[,]
                {
                    { new Guid("283eac3a-2c9a-438d-88da-b9f95bf1b672"), "Desktop Application", new Guid("a3291dee-630d-4fd3-ba82-8645cd3d0b27") },
                    { new Guid("382539cf-215a-4763-a329-8bed44ca9ef8"), "SaaS", new Guid("246796ff-9ccc-4c00-9e2e-335694d5671a") },
                    { new Guid("95937444-283b-4b54-b391-53f965978806"), "Web Application", new Guid("a6f02127-1525-44ff-a400-7dc52694b953") },
                    { new Guid("9a2b3f8c-8ed8-44f1-b9a6-e85262661b21"), "Mobile Application", new Guid("c8f4b8cc-8b78-4e8d-9cb4-e0d3c4e28f4a") },
                    { new Guid("f78f5ba8-7ac6-4e0c-b435-0a73664b3a54"), "IoT Application", new Guid("87b81a02-84a4-4919-91ee-58be6e924279") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChatTypeId",
                table: "Chats",
                column: "ChatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_IdUser",
                table: "Chats",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_IdUser_Secondary",
                table: "Chats",
                column: "IdUser_Secondary");

            migrationBuilder.CreateIndex(
                name: "IX_HostingPreferences_HostingPreference_ReferenceCode",
                table: "HostingPreferences",
                column: "HostingPreference_ReferenceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Chat_Id",
                table: "Messages",
                column: "Chat_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserID",
                table: "Messages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Order_IdUser",
                table: "Orders",
                column: "Order_IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_StepFourOrders_HostingPreference_Id",
                table: "StepFourOrders",
                column: "HostingPreference_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StepFourOrders_Order_Id",
                table: "StepFourOrders",
                column: "Order_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepOneOrders_Order_Id",
                table: "StepOneOrders",
                column: "Order_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepOneOrders_TypeOfApplication_Id",
                table: "StepOneOrders",
                column: "TypeOfApplication_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StepThreeOrders_Order_Id",
                table: "StepThreeOrders",
                column: "Order_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepTwoOrders_Order_Id",
                table: "StepTwoOrders",
                column: "Order_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepTwoOrders_SuportedPlatform_Id",
                table: "StepTwoOrders",
                column: "SuportedPlatform_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SuportedPlatforms_SuportedPlatform_ReferenceCode",
                table: "SuportedPlatforms",
                column: "SuportedPlatform_ReferenceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfApplications_TypeOfApplication_ReferenceCode",
                table: "TypeOfApplications",
                column: "TypeOfApplication_ReferenceCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "StepFourOrders");

            migrationBuilder.DropTable(
                name: "StepOneOrders");

            migrationBuilder.DropTable(
                name: "StepThreeOrders");

            migrationBuilder.DropTable(
                name: "StepTwoOrders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "HostingPreferences");

            migrationBuilder.DropTable(
                name: "TypeOfApplications");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "SuportedPlatforms");

            migrationBuilder.DropTable(
                name: "ChatTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
