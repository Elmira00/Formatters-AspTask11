using Formatters_AspTask11.Entities;
using Microsoft.EntityFrameworkCore;

namespace Formatters_AspTask11.DataAccess
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
