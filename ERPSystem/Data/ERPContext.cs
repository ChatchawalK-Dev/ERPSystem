using ERPSystem.Models.Cr;
using ERPSystem.Models.Data_Analytics;
using ERPSystem.Models.Document_M;
using ERPSystem.Models.Finance;
using ERPSystem.Models.HumanResources;
using ERPSystem.Models.Manufacturing;
using ERPSystem.Models.Project_M;
using ERPSystem.Models.Supply_Chain;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Data
{
    public class ERPDbContext(DbContextOptions<ERPDbContext> options): DbContext(options)
    { 
        public DbSet<ProductionPlan> ProductionPlans { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<QualityControl> QualityControls { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<DataReport> DataReports { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Analytics> Analytics { get; set; }

        // Finance
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        // Human Resources
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TimeRecord> TimeRecords { get; set; }

        // Supply Chain Management
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        // CRM
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<MarketingCampaign> MarketingCampaigns { get; set; }

        // Project Management
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ResourceAllocation> ResourceAllocations { get; set; }
        public DbSet<Resource> Resources { get; set; }

        // Document Management
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocVersion> DocVersions { get; set; }
        public DbSet<AccessControl> AccessControls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductionPlan>().HasKey(pp => pp.Id);
            modelBuilder.Entity<ProductionOrder>().HasKey(po => po.Id);
            modelBuilder.Entity<Inventory>().HasKey(i => i.Id);
            modelBuilder.Entity<QualityControl>().HasKey(qc => qc.Id);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            // ProductionPlan - ProductionOrder relationship
            modelBuilder.Entity<ProductionPlan>()
                .HasMany(pp => pp.ProductionOrders)
                .WithOne(po => po.ProductionPlan)
                .HasForeignKey(po => po.ProductionPlanID)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductionOrder - Product relationship
            modelBuilder.Entity<ProductionOrder>()
                .HasOne(po => po.Product)
                .WithMany(p => p.ProductionOrders)
                .HasForeignKey(po => po.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // Inventory - Product relationship
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            // QualityControl - Product relationship
            modelBuilder.Entity<QualityControl>()
                .HasOne(qc => qc.Product)
                .WithMany(p => p.QualityControls)
                .HasForeignKey(qc => qc.ProductID)
                .OnDelete(DeleteBehavior.Cascade);



            // Configuration for Analytics models
            modelBuilder.Entity<DataReport>()
                .HasKey(dr => dr.Id);

            modelBuilder.Entity<Dashboard>()
                .HasKey(db => db.Id);

            modelBuilder.Entity<Analytics>()
                .HasKey(a => a.Id);

            // DataReport and Dashboard relationship
            modelBuilder.Entity<DataReport>()
                .HasOne(dr => dr.Dashboard)
                .WithMany(db => db.DataReports)
                .HasForeignKey(dr => dr.DashboardID)
                .OnDelete(DeleteBehavior.Cascade);

            // DataReport and Analytics relationship
            modelBuilder.Entity<DataReport>()
                .HasOne(dr => dr.Analytics)
                .WithMany(a => a.DataReports)
                .HasForeignKey(dr => dr.AnalyticsID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for Finance models
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Budget>().HasKey(b => b.Id);
            modelBuilder.Entity<Expense>().HasKey(e => e.Id);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Expenses)
                .WithOne(e => e.Budget)
                .HasForeignKey(e => e.BudgetID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for Human Resources models
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Recruitment>().HasKey(r => r.Id);
            modelBuilder.Entity<Training>().HasKey(t => t.Id);
            modelBuilder.Entity<TimeRecord>().HasKey(tr => tr.Id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Trainings)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TimeRecords)
                .WithOne(tr => tr.Employee)
                .HasForeignKey(tr => tr.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recruitment>()
                .HasMany(r => r.Employees)
                .WithOne(e => e.Recruitment)
                .HasForeignKey(e => e.RecruitmentID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for Supply Chain models
            modelBuilder.Entity<PurchaseOrder>().HasKey(po => po.Id);
            modelBuilder.Entity<PurchaseOrderDetail>().HasKey(pod => pod.Id);
            modelBuilder.Entity<Delivery>().HasKey(d => d.Id);
            modelBuilder.Entity<Warehouse>().HasKey(w => w.Id);

            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(po => po.PurchaseOrderDetails)
                .WithOne(pod => pod.PurchaseOrder)
                .HasForeignKey(pod => pod.PurchaseOrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(po => po.Deliveries)
                .WithOne(d => d.PurchaseOrder)
                .HasForeignKey(d => d.PurchaseOrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Inventories)
                .WithOne(i => i.Warehouse)
                .HasForeignKey(i => i.WarehouseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for CRM models
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<SalesOrder>().HasKey(so => so.Id);
            modelBuilder.Entity<SalesOrderDetail>().HasKey(sod => sod.Id);
            modelBuilder.Entity<MarketingCampaign>().HasKey(mc => mc.Id);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.SalesOrders)
                .WithOne(so => so.Customer)
                .HasForeignKey(so => so.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalesOrder>()
                .HasMany(so => so.SalesOrderDetails)
                .WithOne(sod => sod.SalesOrder)
                .HasForeignKey(sod => sod.SalesOrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MarketingCampaign>()
                .HasMany(mc => mc.Customers)
                .WithOne(c => c.MarketingCampaign)
                .HasForeignKey(c => c.MarketingCampaignID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for Project Management models
            modelBuilder.Entity<Project>().HasKey(p => p.Id);
            modelBuilder.Entity<ProjectTask>().HasKey(t => t.Id);
            modelBuilder.Entity<ResourceAllocation>().HasKey(ra => ra.Id);
            modelBuilder.Entity<Resource>().HasKey(r => r.Id);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectTasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.ResourceAllocations)
                .WithOne(ra => ra.Project)
                .HasForeignKey(ra => ra.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Resource>()
                .HasMany(r => r.ResourceAllocations)
                .WithOne(ra => ra.Resource)
                .HasForeignKey(ra => ra.ResourceID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration for Document Management models
            modelBuilder.Entity<Document>().HasKey(d => d.Id);
            modelBuilder.Entity<DocVersion>().HasKey(v => v.Id);
            modelBuilder.Entity<AccessControl>().HasKey(ac => ac.Id);

            modelBuilder.Entity<Document>()
                .HasMany(d => d.DocVersions)
                .WithOne(v => v.Document)
                .HasForeignKey(v => v.DocumentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Document>()
                .HasMany(d => d.AccessControls)
                .WithOne(ac => ac.Document)
                .HasForeignKey(ac => ac.DocumentID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
