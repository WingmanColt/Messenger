using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HireMe.Models;

namespace HireMe.Data
{
    public class BaseDbContext : IdentityDbContext<User>
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }
    }
    public class FeaturesDbContext : DbContext
    {
        public FeaturesDbContext(DbContextOptions<FeaturesDbContext> options) : base(options) { }

        public DbSet<Message> Message { get; set; }
    }
}
