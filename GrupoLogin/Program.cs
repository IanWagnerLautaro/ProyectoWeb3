using GrupoLogin.BL.Services;
using GrupoLogin.BL.Services.Interfaces;
using GrupoLogin.DATA;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Autenticación Microsoft
var initialScopes = builder.Configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');



//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddGoogle(options =>
//{
//    options.ClientId = builder.Configuration["GoogleAuthentication:ClientId"];
//    options.ClientSecret = builder.Configuration["GoogleAuthentication:ClientSecret"];
//})
//            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
//                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
//                .AddMicrosoftGraph(builder.Configuration.GetSection("DownstreamApi"))
//                .AddInMemoryTokenCaches();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//})
//    .AddGoogle(options =>
//    {
//        options.ClientId = builder.Configuration["GoogleAuthentication:ClientId"];
//        options.ClientSecret = builder.Configuration["GoogleAuthentication:ClientSecret"];

//    })
//    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
//        .EnableTokenAcquisitionToCallDownstreamApi(builder.Configuration.GetSection("DownstreamApi:Scopes").Value.Split(' '))
//        .AddMicrosoftGraph(builder.Configuration.GetSection("DownstreamApi"))
//        .AddInMemoryTokenCaches();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddGoogle(options =>
    {
        options.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            context.Response.Redirect(context.RedirectUri + "&prompt=consent");
            return Task.CompletedTask;
        };
        options.ClientId = builder.Configuration["GoogleAuthentication:ClientId"];
        options.ClientSecret = builder.Configuration["GoogleAuthentication:ClientSecret"];
        options.SaveTokens = true;
    })
    .AddLinkedIn(linkedinOptions =>
    {
        linkedinOptions.ClientId = builder.Configuration["LinkedinAuthentication:ClientId"];
        linkedinOptions.ClientSecret = builder.Configuration["LinkedinAuthentication:ClientSecret"];
        linkedinOptions.SaveTokens = true;
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.ClientId = builder.Configuration["FacebookAuthentication:ClientId"];
        facebookOptions.ClientSecret = builder.Configuration["FacebookAuthentication:ClientSecret"];
        facebookOptions.SaveTokens = true;
    })
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi(builder.Configuration.GetSection("DownstreamApi:Scopes").Value.Split(' '))
        .AddMicrosoftGraph(builder.Configuration.GetSection("DownstreamApi"))
        .AddInMemoryTokenCaches(); 



#endregion

builder.Services.AddTransient<IProductoService, ProductoService>();

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
