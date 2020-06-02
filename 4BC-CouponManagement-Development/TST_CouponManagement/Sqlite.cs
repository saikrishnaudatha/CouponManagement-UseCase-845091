using CouponManagementDBEntity.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SHR_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouponManagementTestCase.DATA
{
    public class Sqlite
    {
        public CouponManagementContext CreateSqliteConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var option = new DbContextOptionsBuilder<CouponManagementContext>().UseSqlite(connection).Options;
            var context = new CouponManagementContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            return context;
        }

    }
}
