using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Catalog.API.Extensions
{
    public static class DbMappingExtension
    {
        /// <summary>
        /// Define default property for column types
        /// </summary>
        /// <param name="builder"></param>
        public static void ApplyDefaultColumnTypes(this ModelBuilder builder)
        {
            foreach (var model in builder.Model.GetEntityTypes().SelectMany(x => x.GetProperties()))
            {
                if (model.ClrType == typeof(string))
                    model.SetColumnType("VARCHAR(255)");

                if (model.ClrType == typeof(decimal))
                    model.SetColumnType("DECIMAL(18, 3)");
            }

            builder
                .Model
                .GetEntityTypes()
                .SelectMany(x => x.GetProperties().Where(y => y.ClrType == typeof(string)))
                .ToList()
                .ForEach(x => x.SetColumnType("VARCHAR(255)"));
        }
    }
}
