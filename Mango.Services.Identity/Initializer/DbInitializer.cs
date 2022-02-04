using IdentityModel;
using Mango.Services.Identity.DbContext;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            applicationDbContext = dbContext;
            userManager = user; 
            roleManager = role;
        }
        public void Initialize()
        {
            //if(userManager.FindByNameAsync(SD.Admin).Result==null)
            //{
            //    roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            //    roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            //}
            //else
            //{
            //    return;
            //}

           // ApplicationUser adminUser = new ApplicationUser { 
           // UserName="thisisacs@gmail.com",
           // Email="thisisacs@gmail.com",
           // EmailConfirmed=true,
           // PhoneNumber="0588123709",
           // FirstName="Aravind",
           // LastName="Admin"
           // };

           // userManager.CreateAsync(adminUser,"Acs@2020").GetAwaiter().GetResult();
           // userManager.AddToRoleAsync(adminUser, SD.Admin);

           //userManager.AddClaimsAsync(adminUser, new Claim[]{ 
           //      new Claim(JwtClaimTypes.Name,adminUser.FirstName+" "+ adminUser.LastName),
           //      new Claim(JwtClaimTypes.GivenName,adminUser.FirstName),
           //      new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
           //      new Claim(JwtClaimTypes.Role,SD.Admin)
           //     });

            //ApplicationUser customerUser = new ApplicationUser
            //{
            //    UserName = "thisisacs1@gmail.com",
            //    Email = "thisisacs1@gmail.com",
            //    EmailConfirmed = true,
            //    PhoneNumber = "0588123709",
            //    FirstName = "ACS",
            //    LastName = "Customer"
            //};

            //userManager.CreateAsync(customerUser, "Acs@2020").GetAwaiter().GetResult();
            //userManager.AddToRoleAsync(customerUser, SD.Customer);

            //userManager.AddClaimsAsync(customerUser, new Claim[]{
            //     new Claim(JwtClaimTypes.Name,customerUser.FirstName+" "+ customerUser.LastName),
            //     new Claim(JwtClaimTypes.GivenName,customerUser.FirstName),
            //     new Claim(JwtClaimTypes.FamilyName,customerUser.LastName),
            //     new Claim(JwtClaimTypes.Role,SD.Customer)
            //    });

        }
    }
}
