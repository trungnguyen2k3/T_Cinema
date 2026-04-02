using CinemaBE.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBE.Tests.UnitTests.Helpers
{
    public static class TestDbFactory
    {
        public static DatabaseContext Create()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new DatabaseContext(options);
        }
    }
}