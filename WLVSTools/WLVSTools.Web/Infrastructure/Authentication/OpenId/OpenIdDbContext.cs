using Microsoft.EntityFrameworkCore;
using WLVSTools.Web.Infrastructure.ExceptionsLogging;

namespace WLVSTools.Web.Infrastructure.Authentication.OpenId
{
    public class OpenIdDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public OpenIdDbContext(DbContextOptions<OpenIdDbContext> options) : base(options)
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
    }
}
