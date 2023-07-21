using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Infra;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Configuration
{
     public static class DataBaseManagement
    {
        public static void MigrationInitialization (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ProductContext>();

                _db.Database.Migrate();
            }
        }
    }
}
