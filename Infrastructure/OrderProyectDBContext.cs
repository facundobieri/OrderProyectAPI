using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class OrderProyectDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

    }
}
