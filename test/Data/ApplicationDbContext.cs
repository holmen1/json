using test.Models;

namespace test.Data;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Assumption> Assumptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=assumptions.db");
        //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=assumptions;Trusted_Connection=True;");
    }
}