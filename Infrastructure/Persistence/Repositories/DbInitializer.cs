﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreContext storeContext,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();

                ////////////////////////////////////////////
                if (!_storeContext.productsTypes.Any())
                {
                    var TypesData = await File.ReadAllTextAsync(path: @"..\Infrastructure\Persistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    if (types is not null && types.Any())
                    {
                        await _storeContext.productsTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                //////////////////////////////////////////
                if (!_storeContext.productBrands.Any())
                {
                    var BrandsData = await File.ReadAllTextAsync(path: @"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    if (Brands is not null && Brands.Any())
                    {
                        await _storeContext.productBrands.AddRangeAsync(Brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                //////////////////////////////////////////
                if (!_storeContext.products.Any())
                {
                    var ProductsData = await File.ReadAllTextAsync(path: @"..\Infrastructure\Persistence\Data\Seeding\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                    {
                        await _storeContext.products.AddRangeAsync(Products);
                        await _storeContext.SaveChangesAsync();
                    }
                }
            }
            catch {  }


        }

        public async Task InitializeIdentityAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!_userManager.Users.Any())
            {
                var SuperAdminUser = new User
                {
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdminUser",
                    PhoneNumber = "010123"
                };
                var AdminUser = new User
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "AdminUser",
                    PhoneNumber = "012345"
                };
                await _userManager.CreateAsync(SuperAdminUser,"P@$$w0rd");
                await _userManager.CreateAsync(AdminUser, "P@$$w0rd");

                await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");

            }
        }
    }
}
