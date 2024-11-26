using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using MvcWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// var conStrBuilder = new SqlConnectionStringBuilder(
//         builder.Configuration.GetConnectionString("MvcWebAppContext") ?? throw new InvalidOperationException("Connection string 'MvcWebAppContext' not found.")
//         );
// conStrBuilder["Server"] = builder.Configuration["mysql:server"];
// conStrBuilder["Port"] = builder.Configuration["mysql:port"];
// conStrBuilder["Database"] = builder.Configuration["mysql:database"];
// conStrBuilder["User"] = builder.Configuration["mysql:user"];
// conStrBuilder["Password"] = builder.Configuration["mysql:password"];
// var connectionString = conStrBuilder.ConnectionString;
var server = builder.Configuration["mysql:server"];
var port = builder.Configuration["mysql:port"];
var database = builder.Configuration["mysql:database"];
var user = builder.Configuration["mysql:user"];
var password = builder.Configuration["mysql:password"];
var connectionString = $"Server={server};Port={port};Uid={user};Pwd={password};Database={database};";

builder.Services.AddDbContext<MvcWebAppContext>(options => options.UseMySQL(connectionString));

// Localisation
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "zh", "en" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add cookie authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Add authorization.
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
});
app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
