using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pk => new { pk.PokemonId, pk.CategoryId });

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pk => pk.PokemonCategories)
                .HasForeignKey(c => c.PokemonId);

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Category)
                .WithMany(pk => pk.PokemonCategories)
                .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<PokemonOwner>()
                .HasKey(pk => new { pk.PokemonId, pk.OwnerId });

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(pk => pk.PokemonOwners)
                .HasForeignKey(c => c.PokemonId);

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(pk => pk.PokemonOwners)
                .HasForeignKey(c => c.OwnerId);


        }
    }
}
