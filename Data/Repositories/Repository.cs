using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class Repository : DbContext
    {
        public Repository(DbContextOptions<Repository>options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
    }
}
