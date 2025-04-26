using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using TicketsADN7.Services;
using TicketsADN7.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using WHATSAPPSERVICES;
using INTELISIS.APPCORE.BL;
using EmailService;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TicketsContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Registrar WhatsAppConfiguration desde appsettings.json
builder.Services.Configure<WhatsAppConfiguration>(builder.Configuration.GetSection("WhatsAppConfiguration"));

// Registrar EmailConfiguration desde appsettings.json
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailSettings"));

// Configurar autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirigir si no está autenticado
        options.AccessDeniedPath = "/Account/AccessDenied";
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
    });
AppSettingsBusiness.CadenaConexion(builder.Configuration.GetConnectionString("TicketsConnection"));

builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient();

//Dependencias
builder.Services.AddScoped<IWhatsAppSender, WhatsAppSender>();
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.Use(async (context, next) =>
//{
//    if (context.Request.Cookies.TryGetValue("JwtToken", out var token))
//    {
//        context.Request.Headers["Authorization"] = $"Bearer {token}";
//    }
//    await next.Invoke();
//});

app.UseStatusCodePages(async context => {
    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        response.Redirect("/Account/Login");
    }
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();