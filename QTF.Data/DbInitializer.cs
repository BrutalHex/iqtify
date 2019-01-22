using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QTF.Data.Models;
using System.Linq;

namespace QTF.Data
{
    public class DbInitializer
    {
        //private RoleManager<IdentityRole> _roleManager;
        private IServiceProvider _serviceProvider;
        private QtfDbContext _db;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _db = serviceProvider.GetRequiredService<QtfDbContext>();
        }

        public async Task InitializeAsync()
        {
            var schemaVersion = await _db.Metadata.SingleOrDefaultAsync(_ =>
                _.Category == MetaCategory.DatabaseStatus &&
                _.Key == "SchemaVersion");

            if (schemaVersion?.Value != "1")
            {
                CreateAdmin().Wait();
                _db.Metadata.Add(new Metadata
                {
                    Category = MetaCategory.DatabaseStatus,
                    Key = "SchemaVersion",
                    Value = "1"
                });
                await _db.SaveChangesAsync();
            }
        }

        private async Task CreateAdmin()
        {
            //initializing custom roles 
            var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here you could create a super user who will maintain the web app
            var adminUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };

            //Ensure you have these values in your appsettings.json file
            string userPWD = "Admin123!";
            var _user = await UserManager.FindByEmailAsync("admin@admin.com");

            if (_user == null)
            {
                var createResult = await UserManager.CreateAsync(adminUser, userPWD);
                if (createResult.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}