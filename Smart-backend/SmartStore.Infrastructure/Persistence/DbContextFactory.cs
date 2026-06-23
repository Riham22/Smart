using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartStore.Infrastructure.Persistence;

public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // نضع الـ Connection String هنا مباشرة ليتعرف عليها الـ Migration وقت التصميم دون الاعتماد على ملف الـ JSON
        var connectionString = "Data Source=RuwainyDell\\MSSQLSERVER02;Initial Catalog=SmartStore;Integrated Security=True;Trust Server Certificate=True";

        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
