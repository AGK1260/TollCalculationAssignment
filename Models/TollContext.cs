using Microsoft.EntityFrameworkCore;
using TollCalculation.Models;
namespace TollCalculation.Models
{
    public class TollContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public TollContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<MovementLogs> MovementLogs { get; set; }
       
        public DbSet<Interchanges> Interchanges { get; set; }
        public DbSet<TollCalculationRespomse> tollCalculationRespomses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interchanges>()
                .HasKey(i => i.InterchangeId);
            modelBuilder.Entity<MovementLogs>()
                .HasKey(i => i.MovementId);
            modelBuilder.Entity<TollCalculationRespomse>()
                .HasNoKey();
        }
    }
}
