using AttendanceProAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace AttendanceProCloudFunctions.Data
{
    /// <summary>
    /// This class is used as a migration layer between C# domain classes and database through Entity Framework Core.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<FileRow> Students { get; set; }
        public class FunctionContextFactory : IDesignTimeDbContextFactory<DataContext>
        {
            public DataContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));
                return new DataContext(optionsBuilder.Options);
            }
        }
    }
}
