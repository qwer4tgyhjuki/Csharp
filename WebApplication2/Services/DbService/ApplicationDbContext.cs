using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Services.DbService
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<GameModel> Games { get; set; }
    }
}
