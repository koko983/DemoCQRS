using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Demo.API.Domain.Entities.Auth;
using Demo.API.Domain.Repositories;

namespace Demo.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
