using Microsoft.EntityFrameworkCore;

namespace CaarInsurance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<CaarInsurance.Models.Insuree> Insurees { get; set; }
    }
}
