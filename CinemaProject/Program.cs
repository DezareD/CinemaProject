using CinemaProject;
using Microsoft.AspNetCore.Authentication.Cookies;

// запускаем builder для приложения

var builder = WebApplication.CreateBuilder(args);

// настраиваем сервисы, контексты

builder.Services.AddRazorPages();
builder.Services.AddMvc();

builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Application/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// настраиваем приложение

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Application}/{action=Index}/{id?}");

// запускаем приложение

app.Run();
