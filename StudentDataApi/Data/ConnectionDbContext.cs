using Microsoft.EntityFrameworkCore;
using StudentDataApi.Models;

namespace StudentDataApi.Data
{
    public class ConnectionDbContext:DbContext
    {

        public ConnectionDbContext(DbContextOptions Options):base(Options)
        {

        }
        public DbSet<Student> studentDetail { get; set; }
    }
}
