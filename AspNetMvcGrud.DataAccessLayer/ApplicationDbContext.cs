using AspNetMvcGrud.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcGrud.DataAccessLayer;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Employee> Employee { get; set; }
}
