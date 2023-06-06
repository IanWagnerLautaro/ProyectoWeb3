using GrupoLogin.BL.Services;
using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA;
using GrupoLogin.DATA.Repositories;
using GrupoLogin.DATA.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Autenticación Microsoft
var initialScopes = builder.Configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');



builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddGoogle(options =>
//{
//    options.ClientId = builder.Configuration["GoogleAuthentication:ClientId"];
//    options.ClientSecret = builder.Configuration["GoogleAuthentication:ClientSecret"];
//})
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
                .AddMicrosoftGraph(builder.Configuration.GetSection("DownstreamApi"))
                .AddInMemoryTokenCaches();


#endregion

builder.Services.AddTransient<IProductoService, ProductoService>();

builder.Services.AddTransient<IProductoRepository, ProductoRepository>();

builder.Services.AddDbContext<GrupoLoginContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("EFCoreContext")));


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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
