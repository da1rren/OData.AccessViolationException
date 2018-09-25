using Microsoft.EntityFrameworkCore;

namespace OData.AccessViolationException.Models
{
    public class Context : DbContext
    {
        public virtual DbSet<Person> People { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }
    }
}
