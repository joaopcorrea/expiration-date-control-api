using ExpirationDateControl_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpirationDateControl_API.Context
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
