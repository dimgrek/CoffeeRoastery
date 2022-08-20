using CoffeeRoastery.DAL.PostgreSQL.Context;
using webapi.Tests.Consts;

namespace webapi.Tests.Utilities;

static class Setup
{
    public static void InitializeDbForTests(CoffeeRoasteryContext context)
    {

        context.SaveChanges();
    }

    public static void ClearDb(CoffeeRoasteryContext context)
    {
        context.Database.EnsureDeleted();
    }
}