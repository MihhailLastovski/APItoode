using Microsoft.EntityFrameworkCore;
using ToodeAPI.Models;

namespace APItoode.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Toode> Tooted { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
