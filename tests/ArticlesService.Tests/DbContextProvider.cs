using System;
using ArticlesService.Persistence;
using ArticlesService.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ArticlesService.Tests
{
    public class DbContextProvider
    {
        internal static ArticlesDbContext GetInMemory()
        {
            var currentDatabaseName = "Database_" + DateTime.Now.ToFileTimeUtc();
            var builder = new DbContextOptionsBuilder<ArticlesDbContext>();
            builder.UseInMemoryDatabase(currentDatabaseName);
            var options = builder.Options;
            return new ArticlesDbContext(options);
        }

    }
}