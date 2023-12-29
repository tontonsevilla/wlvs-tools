using Microsoft.EntityFrameworkCore;

namespace WLVSTools.Web.Infrastructure.Lotto
{
    public class LottoDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public LottoDbContext(DbContextOptions<LottoDbContext> options) : base(options)
        {
        }
        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //use this to configure the context
        }
        //OnModelCreating() method is used to configure the model using ModelBuilder Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //use this to configure the model
        }
        //Adding Domain Classes as DbSet Properties
        public DbSet<LottoResult> LottoResults { get; set; }
    }
}
