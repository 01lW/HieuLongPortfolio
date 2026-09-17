using HieuLongPortfolio.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;

var builder =
    WebApplication.CreateBuilder(args);


// =========================================
// RAILWAY PORT
// =========================================

var port =
    Environment.GetEnvironmentVariable("PORT");

if (!string.IsNullOrWhiteSpace(port))
{
    builder.WebHost.UseUrls(
        $"http://0.0.0.0:{port}");
}


// =========================================
// MVC
// =========================================

builder.Services.AddControllersWithViews();


// =========================================
// PROJECT STORAGE
// =========================================

builder.Services.AddSingleton<
    IProjectRepository,
    JsonProjectRepository>();


// =========================================
// RESUME STORAGE
// =========================================

builder.Services.AddSingleton<
    IResumeRepository,
    JsonResumeRepository>();


// =========================================
// FORWARDED HEADERS
// =========================================

builder.Services.Configure<ForwardedHeadersOptions>(
    options =>
    {
        options.ForwardedHeaders =
            ForwardedHeaders.XForwardedFor |
            ForwardedHeaders.XForwardedProto;

        options.KnownIPNetworks.Clear();
        options.KnownProxies.Clear();
    });


// =========================================
// COOKIE AUTHENTICATION
// =========================================

builder.Services
    .AddAuthentication(
        CookieAuthenticationDefaults
            .AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath =
            "/Admin/Login";

        options.AccessDeniedPath =
            "/Admin/Login";


        options.Cookie.Name =
            "HieuLongPortfolio.Admin";


        options.Cookie.HttpOnly =
            true;


        options.Cookie.SecurePolicy =
            CookieSecurePolicy.SameAsRequest;


        options.Cookie.SameSite =
            SameSiteMode.Lax;


        options.ExpireTimeSpan =
            TimeSpan.FromHours(8);


        options.SlidingExpiration =
            true;
    });


var app =
    builder.Build();


// =========================================
// FORWARDED HEADERS
// =========================================

app.UseForwardedHeaders();


// =========================================
// ERRORS
// =========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Home/Error");

    app.UseHsts();
}


// =========================================
// MIDDLEWARE
// =========================================

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


// =========================================
// ROUTING
// =========================================

app.MapControllerRoute(
    name: "default",
    pattern:
        "{controller=Home}/{action=Index}/{id?}");


// =========================================
// START APPLICATION
// =========================================

app.Run();