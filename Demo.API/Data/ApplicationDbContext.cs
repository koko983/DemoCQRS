using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Demo.API.Domain.Entities.Auth;
using Demo.API.Domain.Repositories;
using Demo.API.Domain.Entities.Catalog;
using Demo.API.Domain.Entities.Order;
using Demo.API.Domain.Entities.Logging;

namespace Demo.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IUnitOfWork
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LogEntry> Logs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
