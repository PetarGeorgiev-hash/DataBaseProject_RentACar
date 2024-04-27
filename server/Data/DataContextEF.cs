using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{

    public class DataContextEF : DbContext
    {
        private IConfiguration _config;
        public DataContextEF(IConfiguration configuration)
        {
            _config = configuration;
        }

        public virtual DbSet<Client> Client { set; get; }
        public virtual DbSet<CarYear> CarYear { set; get; }

        public virtual DbSet<Category> Category { set; get; }
        public virtual DbSet<Price> Price { set; get; }
        public virtual DbSet<Brand> Brand { set; get; }

        public virtual DbSet<Model> Models { set; get; }


        public virtual DbSet<CarsForRent> CarsForRent { set; get; }

        public virtual DbSet<Report> Report { set; get; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("Dev"), opt => opt.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Id);

            modelBuilder.Entity<CarYear>().HasKey(c => c.Id);

            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<Price>().HasKey(p => p.Id);
            modelBuilder.Entity<Price>().Property(p => p.CarPrice).HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Brand>().HasKey(b => b.Id);

            modelBuilder.Entity<Model>().HasKey(m => m.Id);



            modelBuilder.Entity<CarsForRent>().HasKey(c => c.Id);
            modelBuilder.Entity<Report>().HasKey(c => c.Id);


        }
    }
}