using Domain.Entities;

using Microsoft.EntityFrameworkCore;


namespace ImpactBackend.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }


        public DbSet<AboutUs> aboutUs { get; set; }
        public DbSet<AdditionalCost> additionalCosts { get; set; }
        public DbSet<Ads> ads { get; set; }
        public DbSet<Attendance> attendances { get; set; }
        public DbSet<Center> centers { get; set; }
        public DbSet<Client> clients  { get; set; }
        public DbSet<ClientAccount> clientAccounts { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<EmployeeAccount> employeeAccounts { get; set; }
        public DbSet<ReceiptFromClient> receiptsFromClient { get; set; }
        public DbSet<ReceiptToEmployee> receiptsToEmployee { get; set; }
        public DbSet<ReceiptToRestaurant> receiptsToRestaurant { get; set; }
        public DbSet<Receipt> receipts { get; set; }
        public DbSet<Hall> halls { get; set; }
        public DbSet<Mail> mails { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<FinancialFund> financialFunds { get; set; }
        public DbSet<Restaurant> restaurants { get; set; }
        public DbSet<RestaurantAccount> restaurantAccounts { get; set; }
        public DbSet<SubTraining> subTrainings { get; set; }
        public DbSet<Trainee> trainees { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Training> trainings { get; set; }
        public DbSet<TrainingInvoice> trainingInvoices { get; set; }
        public DbSet<TrainingType> trainingTypes { get; set; }
        public DbSet<OtherExpenses> otherExpenses { get; set; }
        public DbSet<User> users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "YourConnectionString",
                    b => b.MigrationsAssembly("Impact.Api"));
            }
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Training>()
        //       .HasMany(t => t.Reservations)
        //       .WithOne(r => r.Training)
        //       .OnDelete(DeleteBehavior.NoAction);
        //}


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // استخدام محرك قاعدة البيانات SQL Server المحلية (localdb) باسم "Cardb"
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ImpactDb");
        //}

    }
}

