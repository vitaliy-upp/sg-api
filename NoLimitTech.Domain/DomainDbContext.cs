using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NoLimitTech.Domain.Configurations;
using NoLimitTech.Domain.Models;
using Payment.DataAccess.Enitities;
using System.IO;
using System.Linq;

namespace NoLimitTech.Domain
{
    public class DomainDbContext : Microsoft.EntityFrameworkCore.DbContext, IDomainDbContext
    {

        #region UserManagement

        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<SubscriptionFeature> SubscriptionFeatures { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

        #endregion

        public DbSet<Message> ChatMessages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<UserCustomer> UserCustomers { get; set; }
        public DbSet<StripeProductInfo> StripeProductsInfo { get; set; }


        public DomainDbContext(DbContextOptions<DomainDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            #region Apply Configurations

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRolesConfiguration());
            builder.ApplyConfiguration(new FeatureConfiguration());
            builder.ApplyConfiguration(new SubscriptionFeatureConfiguration());
            builder.ApplyConfiguration(new SubscriptionPlanConfiguration());


            #endregion

            #region Relationships

            builder.Entity<UserRoles>()
                  .HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<UserRoles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId);
            builder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId);


            builder.Entity<SubscriptionFeature>()
                .HasOne(f => f.SubscriptionPlan)
                .WithMany(p => p.SubscriptionFeatures)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer()
            }

            base.OnConfiguring(optionsBuilder);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DomainDbContext>
    {
        public DomainDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../NoLimitTech.WebApi/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<DomainDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new DomainDbContext(builder.Options);
        }
    }
}
