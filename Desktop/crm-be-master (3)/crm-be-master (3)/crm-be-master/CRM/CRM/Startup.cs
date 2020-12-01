using CRM.Auth;
using CRM.Data;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Hubs;
using CRM.MapsterCofig;
using CRM.Model;
using CRM.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NSwag.AspNetCore;
using System;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using NSwag.SwaggerGeneration.Processors.Security;
using CRM.HangfireJob;
using CRM.Helpers;
using NSwag;
using CRM.Extensions;
using Microsoft.AspNetCore.SignalR;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace CRM
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }

    public class Startup
    {
        private readonly SymmetricSecurityKey _signingKey = JwtSecurityKey.Create("iNivDmHLpUA223sqsfhqGbMRdRj1PVkH");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CRMContext>();

            #region DI Solutions

            //EventLogFile
            services.AddTransient<ICustomerTypeRepository, CustomerTypeRepository>();
            services.AddTransient<ICustomerTypeService, CustomerTypeService>();

            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ILogRepository, LogRepository>();

            //add for data
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Hangfire
            services.AddTransient<ITestBackgroud, TestBackgroud>();

            //EventLog
            services.AddTransient<IEventLogRepository, EventLogRepository>();
            services.AddTransient<IEventLogService, EventLogService>();

            //EventLogFile
            services.AddTransient<IEventLogFileRepository, EventLogFileRepository>();
            services.AddTransient<IEventLogFileService, EventLogFileService>();

            //ErrorLog
            services.AddTransient<ICooperationContractService, CooperationContractService>();
            services.AddTransient<ICooperationContractRepository, CooperationContractRepository>();

            //GlobalVariableValue
            services.AddTransient<ISubCoContractRepository, SubCoContractRepository>();
            services.AddTransient<ISubCoContractService, SubCoContractService>();

            //GlobalVariable
            services.AddTransient<ICoContractTelServiceRepository, CoContractTelServiceRepository>();
            services.AddTransient<ICoContractTelServiceService, CoContractTelServiceService>();

            services.AddTransient<ISubCoContractServiceItemRepository, SubCoContractServiceItemRepository>();
            services.AddTransient<ISubCoContractServiceItemService, SubCoContractServiceItemService>();
            //ErrorLog
            services.AddTransient<IErrorLogService, ErrorLogService>();
            services.AddTransient<IErrorLogRepository, ErrorLogRepository>();

            //GlobalVariableValue
            services.AddTransient<IGlobalVariableValueRepository, GlobalVariableValueRepository>();
            services.AddTransient<IGlobalVariableValueService, GlobalVariableValueService>();

            //GlobalVariable
            services.AddTransient<IGlobalVariableRepository, GlobalVariableRepository>();
            services.AddTransient<IGlobalVariableService, GlobalVariableService>();

            //Menu
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IMenuService, MenuService>();

            //PriceHistory
            services.AddTransient<IPriceHistoryRepository, PriceHistoryRepository>();
            services.AddTransient<IPriceHistoryService, PriceHistoryService>();

            //AttributeValue
            services.AddTransient<IAttributeValueRepository, AttributeValueRepository>();
            services.AddTransient<IAttributeValueService, AttributeValueService>();

            //PredefinedValue
            services.AddTransient<IPredefinedValueRepository, PredefinedValueRepository>();
            services.AddTransient<IPredefinedValueService, PredefinedValueService>();

            //ProductAttribute
            services.AddTransient<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddTransient<IProductAttributeService, ProductAttributeService>();

            //Product
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            //Building
            services.AddTransient<IBuildingRepository, BuildingRepository>();
            services.AddTransient<IBuildingService, BuildingService>();

            //ProductCategory
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();

            //ProductCategory
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();

            //Form
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<IFormRepository, FormRepository>();

            //FormGroup
            services.AddTransient<IFormGroupService, FormGroupService>();
            services.AddTransient<IFormGroupRepository, FormGroupRepository>();

            //HsWorkFlowInstance
            services.AddTransient<IHsWorkFlowInstanceService, HsWorkFlowInstanceService>();
            services.AddTransient<IHsWorkFlowInstanceRepository, HsWorkFlowInstanceRepository>();

            //HsWorkFlow
            services.AddTransient<IHsWorkFlowService, HsWorkFlowService>();
            services.AddTransient<IHsWorkFlowRepository, HsWorkFlowRepository>();

            //HsWorkFlowInstance
            services.AddTransient<IHsWorkFlowInstanceService, HsWorkFlowInstanceService>();
            services.AddTransient<IHsWorkFlowInstanceRepository, HsWorkFlowInstanceRepository>();

            //HsWorkFlowConnection
            services.AddTransient<IHsWorkFlowConnectionService, HsWorkFlowConnectionService>();
            services.AddTransient<IHsWorkFlowConnectionRepository, HsWorkFlowConnectionRepository>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();

            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<IRoleOfUserService, RoleOfUserService>();
            services.AddTransient<IRoleOfUserRepository, RoleOfUserRepository>();

            services.AddTransient<IPermissionOfRoleRepository, PermissionOfRoleRepository>();
            services.AddTransient<IPermissionOfRoleService, PermissionOfRoleService>();

            services.AddTransient<IGroupUserService, GroupUserService>();
            services.AddTransient<IGroupUserRepository, GroupUserRepository>();

            services.AddTransient<IRoleOfGroupService, RoleOfGroupService>();
            services.AddTransient<IRoleOfGroupRepository, RoleOfGroupRepository>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();

            services.AddTransient<ICustomerWorkFlowRepository, CustomerWorkFlowRepository>();
            services.AddTransient<ICustomerWorkFlowService, CustomerWorkFlowService>();

            services.AddTransient<IWorkFlowHistoryRepository, WorkFlowHistoryRepository>();
            services.AddTransient<IWorkFlowHistoryService, WorkFlowHistoryService>();

            services.AddTransient<IIdentityCardRepository, IdentityCardRepository>();
            services.AddTransient<IIdentityCardService, IdentityCardService>();

            services.AddTransient<IBankAccountRepository, BankAccountRepository>();
            services.AddTransient<IBankAccountService, BankAccountService>();

            services.AddTransient<IWorkFlowHistoryFileRepository, WorkFlowHistoryFileRepository>();
            services.AddTransient<IWorkFlowHistoryFileService, WorkFlowHistoryFileService>();

            services.AddTransient<IHubUserConnectionRepository, HubUserConnectionRepository>();
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();

            services.AddTransient<IHsNotificationRepository, HsNotificationRepository>();
            services.AddTransient<IHsNotificationService, HsNotificationService>();

            services.AddTransient<ITransactionLogService, TransactionLogService>();
            services.AddTransient<ITransactionLogRepository, TransactionLogRepository>();

            services.AddTransient<IChangeLogService, ChangeLogService>();
            services.AddTransient<IChangeLogRepository, ChangeLogRepository>();

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IHsTemplateRepository, HsTemplateRepository>();
            services.AddTransient<IHsTemplateService, HsTemplateService>();
            //Mail 
            services.AddTransient<IEmailService, EmailService>();

            //Contract
            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IContractService, ContractService>();

            //Contact
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactService, ContactService>();

            //CareHistory
            services.AddTransient<ICareHistoryRepository, CareHistoryRepository>();
            services.AddTransient<ICareHistoryService, CareHistoryService>();

            //CRM - [Nov - 1st][2019] [feeling exhausted with the mess which mr Long built.]
            services.AddTransient<ITelecomserviceService, TelecomserviceService>();
            services.AddTransient<ITelecomserviceRepository, TelecomserviceRepository>();

            services.AddTransient<ICommonTelecomserviceRepository, CommonTelecomserviceRepository>();
            services.AddTransient<ICommonTelecomserviceService, CommonTelecomserviceService>();

            services.AddTransient<IContractTelecomAppendixRepository, ContractTelecomAppendixRepository>();
            services.AddTransient<IContractTelecomAppendixService, ContractTelecomAppendixService>();

            services.AddTransient<IContractTelecomRepository, ContractTelecomRepository>();
            services.AddTransient<IContractTelecomService, ContractTelecomService>();

            services.AddTransient<ITelecomserviceContractAppendixRepository, TelecomserviceContractAppendixRepository>();
            services.AddTransient<ITelecomserviceContractAppendixService, TelecomserviceContractAppendixService>();

            services.AddTransient<ITelecomserviceParameterRepository, TelecomserviceParameterRepository>();
            services.AddTransient<ITelecomserviceParameterService, TelecomserviceParameterService>();

            services.AddTransient<IDashBoardChartRepository, DashBoardChartRepository>();
            services.AddTransient<IDashBoardChartService, DashBoardChartService>();

            //ContractAppendix
            services.AddTransient<IContractAppendixRepository, ContractAppendixRepository>();
            services.AddTransient<IContractAppendixService, ContractAppendixService>();
            #endregion
            #region Auth
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                            ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                            IssuerSigningKey = _signingKey
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            },
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                // If the request is for our hub...
                                var path = context.HttpContext.Request.Path;
                                if (path.StartsWithSegments("/centerHub"))
                                {
                                    // Read the token out of the query string
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });

            // api user claim policy
            services.AddAuthorization();

            // add identity
            var authBuilder = services.AddIdentityCore<HsUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            }).AddRoles<IdentityRole>();
            authBuilder = new IdentityBuilder(authBuilder.UserType, typeof(IdentityRole), authBuilder.Services);
            authBuilder.AddEntityFrameworkStores<CRMContext>().AddDefaultTokenProviders();
            #endregion

            // add cors
            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()
            ));

            // other
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSignalR();
            // Register the Swagger services
            services.AddSwagger();
            services.AddHangfire(x => x.UseSqlServerStorage(@"Server=210.2.92.202;Database=crm-qtsc-hangfire;user id=sa;password=zaq@123;Trusted_Connection=True;Integrated Security=false;"));
            //services.AddHangfire(x => x.UseSqlServerStorage(@"Server=202.78.227.89;database=crm-qtsc-hangfire_test;user id=sa;password=an@0906782333;trusted_connection=true;integrated security=false;"));

            services.AddElasticsearch(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            //IHsNotificationService hsNotificationService,
            //IHubContext<CenterHub> hubContext,
            //IHubUserConnectionService hubUserConnection,
            //IEmailService emailService,
            //UserManager<HsUser> userManager,
            //IContractService contractService,
            ILoggerFactory loggerFactory,
            ITestBackgroud _backgroud)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;

                settings.GeneratorSettings.Title = "CRM API";

                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));

                settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer",
                    new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = SwaggerSecurityApiKeyLocation.Header
                    }));
            });

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseSignalR(routes =>
            {
                routes.MapHub<CenterHub>("/centerHub");
            });
            //Master Mapper
            MapsterConfig map = new MapsterConfig();//contractService
            map.Run();
            app.UseMvc();

            #region Hangfire
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            //        TestBackgroud testBackgroud = new TestBackgroud(contractService, hsNotificationService, hubContext, hubUserConnection, userManager, emailService);
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
            RecurringJob.AddOrUpdate(
    () => _backgroud.NotiExpireContract(),
            "05 17 * * *");

            RecurringJob.AddOrUpdate(
    () => _backgroud.ExtensionTelecomContract(),"10 13 * * *"
            );
            RecurringJob.AddOrUpdate(
    () => _backgroud.AddDashBoardChart(), Cron.Monthly
            );
            #endregion

            #region log4net
            loggerFactory.AddLog4Net();
            Log4NetProvider customProvider = new Log4NetProvider("Log4Net.config");
            //customProvider.SetAdoConnectionString("data source=202.78.227.89;initial catalog=CRMDB000;integrated security=false;persist security info=True;user id=sa;password=an@0906782333");
            #endregion
        }
    }
}
