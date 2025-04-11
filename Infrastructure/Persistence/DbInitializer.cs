using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            // Create Database If it doesn't Exist && Apply to any pending migrations

            if (_context.Database.GetPendingMigrations().Any())
            {
                await _context.Database.MigrateAsync();
            }

            //Data Seeding
            //Seeding ProductTypes from types json file
            if (!_context.ProductTypes.Any())
            {
                //1.Read All Data from types json file as string
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                //2.Convert string to C# objects[List<ProductType>]
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                //3.Add List<ProductType> to Database
                if (types is not null && types.Any()) 
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }

            //Seeding ProductBrands from brands json file
            if (!_context.ProductBrands.Any())
            {
                var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if(brands is not null && brands.Any())
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }


            //Seeding Products from products json file

            if (!_context.Products.Any())
            {
                var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products is not null && products.Any())
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

//..\Infrastructure\Persistence\Data\Seeding\types.json
//..\Infrastructure\Persistence\Data\Seeding\brands.json
//..\Infrastructure\Persistence\Data\Seeding\products.json