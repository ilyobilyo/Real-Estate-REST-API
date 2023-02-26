using bgbrokersapi.Data.Models;
using bgbrokersapi.Data.Models.OfferLocation;
using bgbrokersapi.Data.Models.Types;
using bgbrokersapi.Data.Models.User;
using bgbrokersapi.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bgbrokersapi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Offer
        public DbSet<Image> Images { get; set; }
        public DbSet<Offer> Offers { get; set; }

        //Offer Location
        public DbSet<City> Cities { get; set; }
        public DbSet<Hood> Hoods { get; set; }

        //Offer Types
        public DbSet<Construction> Constructions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Exposition> Expositions { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Heating> Heatings { get; set; }
        public DbSet<SellType> SellTypes { get; set; }
        public DbSet<OfferType> OfferTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Offer>()
                .HasOne(x => x.CreateUser)
                .WithMany(x => x.CreatedOffers)
                .HasForeignKey(x => x.CreateUserId);

            builder.Entity<Offer>()
               .HasOne(x => x.UpdateUser)
               .WithMany(x => x.UpdatedOffers)
               .HasForeignKey(x => x.UpdateUserId);

            builder.Entity<Offer>()
                .HasOne(x => x.Construction)
                .WithMany(x => x.Offers)
                .HasForeignKey(x => x.ConstructionId);

            builder.Entity<Offer>()
                .HasOne(x => x.Currency)
                .WithMany(x => x.Offers)
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
