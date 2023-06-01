using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DayOneAssignment.Models
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        
        
        }
        public DbSet<Employee>  Employees { get; set;}
        public DbSet<Company>  Companys { get; set;}
    }
}
