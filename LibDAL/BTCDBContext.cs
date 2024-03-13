using LibDAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace LibDAL
{
    public class BTCDBContext : DbContext
    {
        public DbSet<WeightPositions> WeightPositions { get; set; }
        public DbSet<Weight> Weights { get; set; }
        public DbSet<NN> NNs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("<Add SQL Connection String Here>");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
