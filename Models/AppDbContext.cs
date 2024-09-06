using System;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Models;

  public class AppDbContext : DbContext
  {
      public DbSet<User> Users { get; set; }

      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //   {
    //       optionsBuilder.UseSqlServer("Server=tcp:ssoappserver.database.windows.net,1433;Initial Catalog=WebApp1;Persist Security Info=False;User ID=spadmin;Password=Testdb@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    //   }
  }
