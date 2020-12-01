using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: true),
                    Permissions = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    PredefinedValue = table.Column<string>(nullable: true),
                    ReadPermission = table.Column<Guid>(nullable: false),
                    WritePermission = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    PermissionIdR = table.Column<Guid>(nullable: true),
                    PermissionIdW = table.Column<Guid>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Formulas = table.Column<string>(nullable: true),
                    NumbertoWordFields = table.Column<string>(nullable: true),
                    HsWorkflowId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HsGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HsPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HsRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HsWorkFlows",
                columns: table => new
                {
                    PermissionIdR = table.Column<Guid>(nullable: true),
                    PermissionIdW = table.Column<Guid>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsWorkFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    RouterLink = table.Column<string>(nullable: true),
                    IconType = table.Column<string>(nullable: true),
                    IconName = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityId = table.Column<Guid>(nullable: false),
                    DateChanged = table.Column<DateTime>(nullable: false),
                    FunctionType = table.Column<string>(nullable: true),
                    ByUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "HubUserConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Connection = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubUserConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HubUserConnections_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    NData = table.Column<string>(nullable: true),
                    IsSeen = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormGroups",
                columns: table => new
                {
                    PermissionIdR = table.Column<Guid>(nullable: true),
                    PermissionIdW = table.Column<Guid>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    FormId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormGroups_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HsGroupUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HsGroupUsers_HsGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "HsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HsGroupUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HsPermissionOfRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsPermissionOfRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HsPermissionOfRoles_HsPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "HsPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HsPermissionOfRoles_HsRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "HsRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HsRoleOfGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsRoleOfGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HsRoleOfGroups_HsGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "HsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HsRoleOfGroups_HsRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "HsRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HsRoleOfUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsRoleOfUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HsRoleOfUsers_HsRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "HsRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HsRoleOfUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GlobalVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkflowId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalVariables_HsWorkFlows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "HsWorkFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HsWorkFlowInstances",
                columns: table => new
                {
                    PermissionIdR = table.Column<Guid>(nullable: true),
                    PermissionIdW = table.Column<Guid>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    WorkFlowId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    SubType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FormId = table.Column<Guid>(nullable: true),
                    PermissionId = table.Column<Guid>(nullable: true),
                    PermissionIdNoti = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsWorkFlowInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HsWorkFlowInstances_HsWorkFlows_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "HsWorkFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PredefinedValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ProductAttributeId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredefinedValues_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PropertyName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    TransactionLogId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeLogs_TransactionLogs_TransactionLogId",
                        column: x => x.TransactionLogId,
                        principalTable: "TransactionLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HsTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    InstanceId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    FormId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HsTemplate_HsWorkFlowInstances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "HsWorkFlowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HsWorkFlowConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    FromInstanceId = table.Column<Guid>(nullable: false),
                    ToInstanceId = table.Column<Guid>(nullable: false),
                    Command = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsWorkFlowConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HsWorkFlowConnections_HsWorkFlowInstances_FromInstanceId",
                        column: x => x.FromInstanceId,
                        principalTable: "HsWorkFlowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HsWorkFlowConnections_HsWorkFlowInstances_ToInstanceId",
                        column: x => x.ToInstanceId,
                        principalTable: "HsWorkFlowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductAttributeId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    PredefinedId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeValues_PredefinedValues_PredefinedId",
                        column: x => x.PredefinedId,
                        principalTable: "PredefinedValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttributeValues_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerPropertyId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerValues_CustomerProperties_CustomerPropertyId",
                        column: x => x.CustomerPropertyId,
                        principalTable: "CustomerProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.AccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerWorkFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
                    WorkFlowId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerWorkFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerWorkFlows_HsWorkFlows_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "HsWorkFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlobalVariableValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GlobalVariableId = table.Column<Guid>(nullable: false),
                    CustomerWorkflowId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsObject = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalVariableValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalVariableValues_CustomerWorkFlows_CustomerWorkflowId",
                        column: x => x.CustomerWorkflowId,
                        principalTable: "CustomerWorkFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerWorkFlowId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    InstanceId = table.Column<Guid>(nullable: false),
                    InstanceName = table.Column<string>(nullable: true),
                    PreviousStep = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    FormData = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowHistories_CustomerWorkFlows_CustomerWorkFlowId",
                        column: x => x.CustomerWorkFlowId,
                        principalTable: "CustomerWorkFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    WorkFlowHistoryId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventLogs_WorkFlowHistories_WorkFlowHistoryId",
                        column: x => x.WorkFlowHistoryId,
                        principalTable: "WorkFlowHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowHistoryFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    WorkFlowHistoryId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsTemplate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowHistoryFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowHistoryFiles_WorkFlowHistories_WorkFlowHistoryId",
                        column: x => x.WorkFlowHistoryId,
                        principalTable: "WorkFlowHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventLogFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    EventLogId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventLogFiles_EventLogs_EventLogId",
                        column: x => x.EventLogId,
                        principalTable: "EventLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityCard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    IssueAt = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    No = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CountryType = table.Column<string>(nullable: true),
                    TotalInvestment = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    Investment = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    YearStarted = table.Column<string>(nullable: true),
                    YearEnded = table.Column<string>(nullable: true),
                    StaffCount = table.Column<int>(nullable: false, defaultValue: 0),
                    TransactionName = table.Column<string>(nullable: true),
                    CompanyType = table.Column<string>(nullable: true),
                    Career = table.Column<string>(nullable: true),
                    MainCareer = table.Column<string>(nullable: true),
                    ProductHighlight = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    Room = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    InvestmentCertificate = table.Column<string>(nullable: true),
                    InvestmentCertificateDate = table.Column<DateTime>(nullable: true),
                    InvestmentCertificateTime = table.Column<string>(nullable: true),
                    BusinessLicense = table.Column<string>(nullable: true),
                    BusinessLicenseDate = table.Column<DateTime>(nullable: true),
                    BusinessLicenseTime = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    DateEstablish = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2019, 8, 27, 11, 3, 46, 202, DateTimeKind.Local)),
                    DeputyGender = table.Column<int>(nullable: false, defaultValue: 0),
                    DeputyName = table.Column<string>(nullable: true),
                    DeputyPosition = table.Column<string>(nullable: true),
                    DeputyNation = table.Column<string>(nullable: true),
                    DeputyTel = table.Column<string>(nullable: true),
                    DeputyMail = table.Column<string>(nullable: true),
                    ContractNo = table.Column<string>(nullable: true),
                    ContractNoDateRegister = table.Column<DateTime>(nullable: true),
                    ContractNoDateOut = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    MarketType = table.Column<string>(nullable: true),
                    ObjectType = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    CustomerTypeId = table.Column<Guid>(nullable: false),
                    IdentityCardId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_IdentityCard_IdentityCardId",
                        column: x => x.IdentityCardId,
                        principalTable: "IdentityCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_PredefinedId",
                table: "AttributeValues",
                column: "PredefinedId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_ProductAttributeId",
                table: "AttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_ProductId",
                table: "AttributeValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_CustomerId",
                table: "BankAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_TransactionLogId",
                table: "ChangeLogs",
                column: "TransactionLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CustomerId",
                table: "Contact",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdentityCardId",
                table: "Customers",
                column: "IdentityCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerValues_CustomerId",
                table: "CustomerValues",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerValues_CustomerPropertyId",
                table: "CustomerValues",
                column: "CustomerPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWorkFlows_CustomerId",
                table: "CustomerWorkFlows",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWorkFlows_WorkFlowId",
                table: "CustomerWorkFlows",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLogFiles_EventLogId",
                table: "EventLogFiles",
                column: "EventLogId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_WorkFlowHistoryId",
                table: "EventLogs",
                column: "WorkFlowHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FormGroups_FormId",
                table: "FormGroups",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalVariables_WorkflowId",
                table: "GlobalVariables",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalVariableValues_CustomerWorkflowId",
                table: "GlobalVariableValues",
                column: "CustomerWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_HsGroupUsers_GroupId",
                table: "HsGroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HsGroupUsers_UserId",
                table: "HsGroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HsPermissionOfRoles_PermissionId",
                table: "HsPermissionOfRoles",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_HsPermissionOfRoles_RoleId",
                table: "HsPermissionOfRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HsRoleOfGroups_GroupId",
                table: "HsRoleOfGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HsRoleOfGroups_RoleId",
                table: "HsRoleOfGroups",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HsRoleOfUsers_RoleId",
                table: "HsRoleOfUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HsRoleOfUsers_UserId",
                table: "HsRoleOfUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HsTemplate_InstanceId",
                table: "HsTemplate",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_HsWorkFlowConnections_FromInstanceId",
                table: "HsWorkFlowConnections",
                column: "FromInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_HsWorkFlowConnections_ToInstanceId",
                table: "HsWorkFlowConnections",
                column: "ToInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_HsWorkFlowInstances_WorkFlowId",
                table: "HsWorkFlowInstances",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_HubUserConnections_UserId",
                table: "HubUserConnections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityCard_CustomerId",
                table: "IdentityCard",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PredefinedValues_ProductAttributeId",
                table: "PredefinedValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceHistories_ProductId",
                table: "PriceHistories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowHistories_CustomerWorkFlowId",
                table: "WorkFlowHistories",
                column: "CustomerWorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowHistoryFiles_WorkFlowHistoryId",
                table: "WorkFlowHistoryFiles",
                column: "WorkFlowHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerValues_Customers_CustomerId",
                table: "CustomerValues",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Customers_CustomerId",
                table: "BankAccount",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Customers_CustomerId",
                table: "Contact",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerWorkFlows_Customers_CustomerId",
                table: "CustomerWorkFlows",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityCard_Customers_CustomerId",
                table: "IdentityCard",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityCard_Customers_CustomerId",
                table: "IdentityCard");

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
                name: "AttributeValues");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "CustomerValues");

            migrationBuilder.DropTable(
                name: "EventLogFiles");

            migrationBuilder.DropTable(
                name: "FormGroups");

            migrationBuilder.DropTable(
                name: "GlobalVariables");

            migrationBuilder.DropTable(
                name: "GlobalVariableValues");

            migrationBuilder.DropTable(
                name: "HsGroupUsers");

            migrationBuilder.DropTable(
                name: "HsPermissionOfRoles");

            migrationBuilder.DropTable(
                name: "HsRoleOfGroups");

            migrationBuilder.DropTable(
                name: "HsRoleOfUsers");

            migrationBuilder.DropTable(
                name: "HsTemplate");

            migrationBuilder.DropTable(
                name: "HsWorkFlowConnections");

            migrationBuilder.DropTable(
                name: "HubUserConnections");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PriceHistories");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "WorkFlowHistoryFiles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PredefinedValues");

            migrationBuilder.DropTable(
                name: "TransactionLogs");

            migrationBuilder.DropTable(
                name: "CustomerProperties");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "HsPermissions");

            migrationBuilder.DropTable(
                name: "HsGroups");

            migrationBuilder.DropTable(
                name: "HsRoles");

            migrationBuilder.DropTable(
                name: "HsWorkFlowInstances");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "WorkFlowHistories");

            migrationBuilder.DropTable(
                name: "CustomerWorkFlows");

            migrationBuilder.DropTable(
                name: "HsWorkFlows");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "IdentityCard");
        }
    }
}
