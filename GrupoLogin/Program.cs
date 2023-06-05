using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication()
    .AddCookie()
    .AddFacebook(options =>
    {
        options.AppId = configuration["Authentication:Facebook:AppId"]; 
        options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
        options.CallbackPath = "/signin-facebook"; // Ruta de callback personalizada
        options.SaveTokens = true;
    }
    )
    .AddGoogle(options => {
        options.ClientId = "61424984830-md9arlf0q5cis0v7qfir3pkrue0mr01f.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "signin-facebook",
        pattern: "/signin-facebook",
        defaults: new { controller = "Usuario", action = "ExternalLogin", provider = "Facebook" });
});



app.Run();
