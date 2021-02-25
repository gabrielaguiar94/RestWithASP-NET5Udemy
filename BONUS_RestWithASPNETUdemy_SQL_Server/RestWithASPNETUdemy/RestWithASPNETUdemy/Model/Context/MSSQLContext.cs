using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext()
        {

        }

        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options)
        {

        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
