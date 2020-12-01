using CRM.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data
{
    public class CRMContext : IdentityDbContext<HsUser>
    {
        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((_, __) => true, true)
        });
        public CRMContext() : base((new DbContextOptionsBuilder())
            .UseLazyLoadingProxies()
            //.UseLoggerFactory(loggerFactory)  
            //.EnableSensitiveDataLogging()
            .UseSqlServer(@"Server=210.2.92.202;Database=CRMDB000;user id=sa;password=zaq@123;Trusted_Connection=True;Integrated Security=false;")
            //.UseSqlServer(@"Server=202.78.227.89;Database=CRMDB000_Test;user id=sa;password=an@0906782333;Trusted_Connection=True;Integrated Security=false;")
            .Options)
        {

        }

        public DbSet<CooperationContract> CooperationContracts { get; set; }


        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<EventLogFile> EventLogFiles { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public DbSet<Log> Log { get; set; }

        public DbSet<GlobalVariable> GlobalVariables { get; set; }
        public DbSet<GlobalVariableValue> GlobalVariableValues { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<Form> Forms { get; set; }
        public DbSet<FormGroup> FormGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<PredefinedValue> PredefinedValues { get; set; }

        public DbSet<HsGroup> HsGroups { get; set; }
        public DbSet<HsRole> HsRoles { get; set; }
        public DbSet<HsPermission> HsPermissions { get; set; }

        public DbSet<HsGroupUser> HsGroupUsers { get; set; }
        public DbSet<HsRoleOfUser> HsRoleOfUsers { get; set; }
        public DbSet<HsRoleOfGroup> HsRoleOfGroups { get; set; }
        public DbSet<HsPermissionOfRole> HsPermissionOfRoles { get; set; }

        public DbSet<HsWorkFlow> HsWorkFlows { get; set; }
        public DbSet<HsWorkFlowInstance> HsWorkFlowInstances { get; set; }
        public DbSet<HsWorkFlowConnection> HsWorkFlowConnections { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Amount> Amounts { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Deputy> Deputys { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<CustomerWorkFlow> CustomerWorkFlows { get; set; }
        public DbSet<WorkFlowHistory> WorkFlowHistories { get; set; }
        public DbSet<WorkFlowHistoryFile> WorkFlowHistoryFiles { get; set; }
        public DbSet<HubUserConnection> HubUserConnections { get; set; }
        public DbSet<HsNotification> Notifications { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<CustomerProperty> CustomerProperties { get; set; }
        public DbSet<CustomerValue> CustomerValues { get; set; }
        public DbSet<CareHistory> CareHistories { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractAppendix> ContractAppendices { get; set; }

        public DbSet<ContractTelecom> ContractTelecoms { get; set; }
        public DbSet<ContractTelecomAppendix> ContractTelecomAppendices { get; set; }
        public DbSet<Telecomservice> Telecomservices { get; set; }
        public DbSet<CommonTelecomservice> CommonTelecomservices { get; set; }
        public DbSet<TelecomserviceParameter> TelecomserviceParameters { get; set; }
        public DbSet<TelecomserviceContractAppendix> TelecomserviceContractAppendices { get; set; }

        public DbSet<DashBoardChart> DashBoardCharts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<HsWorkFlowConnection>()
                .HasOne<HsWorkFlowInstance>(c => c.FromInstance)
                .WithMany(i => i.ToInstances)
                .HasForeignKey(c => c.FromInstanceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<HsWorkFlowConnection>()
                .HasOne<HsWorkFlowInstance>(c => c.ToInstance)
                .WithMany(i => i.FromInstances)
                .HasForeignKey(c => c.ToInstanceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Customer>().Property(e => e.No).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            base.OnModelCreating(builder);

            builder.Entity<Customer>().Property(_ => _.ActiveDay).HasDefaultValue(DateTime.Now);

            builder.Entity<Customer>()
                .HasOne<Personnel>(c => c.Personnel)
                .WithOne(p => p.Customer)
                .HasForeignKey<Personnel>(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>()
                .HasOne<Owner>(c => c.Owner)
                .WithOne(o => o.Customer)
                .HasForeignKey<Owner>(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Customer>()
                .HasOne<Amount>(c => c.Amount)
                .WithOne(a => a.Customer)
                .HasForeignKey<Amount>(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Customer>()
                .HasOne<Deputy>(c => c.Deputy)
                .WithOne(d => d.Customer)
                .HasForeignKey<Deputy>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Log>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        }

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}
