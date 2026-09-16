using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// =========================================
// SERVICES
// =========================================

builder.Services.AddControllersWithViews();


// =========================================
// COOKIE AUTHENTICATION
// =========================================

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.AccessDeniedPath = "/Admin/Login";

        options.Cookie.Name = "HieuLongPortfolio.Admin";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;

        options.ExpireTimeSpan = TimeSpan.FromHours(8);

        options.SlidingExpiration = true;
    });


var app = builder.Build();


// =========================================
// ERROR HANDLING
// =========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// =========================================
// MIDDLEWARE
// =========================================

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// IMPORTANT:
// Authentication must come before Authorization.
app.UseAuthentication();

app.UseAuthorization();


// =========================================
// ROUTES
// =========================================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();