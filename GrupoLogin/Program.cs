using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Autenticación Microsoft
var initialScopes = builder.Configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');



builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
            .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            .AddMicrosoftGraph(builder.Configuration.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();
            //.AddOpenIdConnect(options =>
            //{
            //    options.Events = new OpenIdConnectEvents
            //    {
            //        OnTokenValidated = context =>
            //        {
            //            // Aquí puedes acceder al token de acceso y al token de identidad después de un inicio de sesión exitoso
            //            // Puedes acceder al token de acceso a través de 'context.SecurityToken.RawData'
            //            // Puedes acceder al token de identidad a través de 'context.SecurityToken.Claims'

            //            // Ejemplo: Imprimir el token de acceso en la consola
            //            Console.WriteLine($"Access Token: {context.SecurityToken.RawData}");

            //            return Task.CompletedTask;
            //        }
            //    };
            //});

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddOpenIdConnect


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(opt =>
//    {
//        opt.Audience = "payment";
//        opt.Authority = "https://localhost:6001";

//        opt.Events.OnTokenValidated = context =>
//        {
//            Console.WriteLine("as");
//        // Token has passed validation and a ClaimsIdentity has been generated.
//        return Task.CompletedTask;
//        };
//    });




#endregion

//Con esta configuracion se obliga a los usuarios a iniciar sesion antes de ingresar a la aplicacion

//builder.Services.AddControllersWithViews(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});

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