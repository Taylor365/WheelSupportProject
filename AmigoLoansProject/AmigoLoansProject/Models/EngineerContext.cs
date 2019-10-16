using Microsoft.EntityFrameworkCore;

namespace AmigoLoansProject.Models
{
    public class EngineerContext : DbContext
    {
        public EngineerContext(DbContextOptions<EngineerContext> options)
            : base(options)
        {
        }

        public DbSet<Engineer> ListOfEngineers { get; set; }
    }
}
