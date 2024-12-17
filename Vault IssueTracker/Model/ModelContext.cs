using Microsoft.EntityFrameworkCore;

namespace Vault_IssueTracker.Model
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
            
        }
        public DbSet<Report> Reports { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
