using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true; // Indica se será necessário o consentimento do cliente para usar cookies
    options.MinimumSameSitePolicy = SameSiteMode.Strict; // Define a política de SameSite (recomenda-se Strict ou Lax para maior segurança)
    options.HttpOnly = HttpOnlyPolicy.Always; // Habilita HttpOnly, tornando os cookies inacessíveis pelo JavaScript
    options.Secure = CookieSecurePolicy.Always; // Exige HTTPS para os cookies serem transmitidos
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
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();