using DataAccess.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Domain.DataAccess.Configurations;
using Domain.DataAccess.Models;
using Payment.DataAccess.Enitities;
using System.IO;
using System.Linq;
using Domain.DataAccess.Entities.KidProfile;
using Domain.DataAccess.Entities.KidProfile.Education;
using UserManagement.DataAccess.UserManagement.Location;
using FileManagement.DataAccess;

namespace Domain.DataAccess
{
    public class DomainDbContext : Microsoft.EntityFrameworkCore.DbContext, IDomainDbContext
    {

        #region UserManagement

        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<SubscriptionFeature> SubscriptionFeatures { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

        #endregion

        #region KidProfile
        public DbSet<KidProfile> KidProfiles { get; set; }
        public DbSet<SuperPower> SuperPowers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<PersonalityQuestion> PersonalityQuestions { get; set; }
        public DbSet<PersonalityAnswer> PersonalityAnswers { get; set; }
        public DbSet<KidPortfolioItem> KidPortfolioItems { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        #endregion

        public DbSet<Message> ChatMessages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<UserCustomer> UserCustomers { get; set; }
        public DbSet<StripeProductInfo> StripeProductsInfo { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }

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
            builder.ApplyConfiguration(new SuperPowerConfiguration());
            builder.ApplyConfiguration(new FeatureConfiguration());
            builder.ApplyConfiguration(new SubscriptionFeatureConfiguration());
            builder.ApplyConfiguration(new SubscriptionPlanConfiguration());
            builder.ApplyConfiguration(new PersonalityQuestionConfiguration());
            #endregion


            #region Relationships
            builder.Entity<PersonalityAnswer>()
                .HasKey(p => new { 
                    p.KidId,
                    p.QuestionId
                });


            builder.Entity<SuperPowerToKid>()
                .HasKey(p => new
                {
                    p.KidId,
                    p.SuperPowerId
                });

            builder.Entity<UserRoles>()
                  .HasKey(ur => new { ur.UserId, ur.RoleId });
            
            builder.Entity<UserRoles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId);

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
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../WebApi/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<DomainDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new DomainDbContext(builder.Options);
        }
    }
}
