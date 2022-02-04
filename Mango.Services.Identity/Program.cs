using Duende.IdentityServer.Services;
using Mango.Services.Identity;
using Mango.Services.Identity.DbContext;
using Mango.Services.Identity.Initializer;
using Mango.Services.Identity.Models;
using Mango.Services.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer(option =>
{
    option.Events.RaiseErrorEvents = true;
    option.Events.RaiseInformationEvents = true;
    option.Events.RaiseFailureEvents = true;
    option.Events.RaiseSuccessEvents = true;
    option.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(SD.identityResources)
.AddInMemoryApiScopes(SD.scopes)
.AddInMemoryClients(SD.clients)
.AddAspNetIdentity<ApplicationUser>()
.AddDeveloperSigningCredential();

//builder.Services.AddScoped<IDbInitializer,DbInitializer>();

builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
