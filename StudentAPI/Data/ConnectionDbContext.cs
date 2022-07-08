using Microsoft.EntityFrameworkCore;
using StudentAPI.Model;

namespace StudentAPI.Data
{
    public class ConnectionDbContext:DbContext
  
    {
        public ConnectionDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Student> studentDetail { get; set; }

        public DbSet<SignupProperties> signupDetails { get; set; }

    }
}
