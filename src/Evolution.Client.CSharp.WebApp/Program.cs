using Evolution.Client.CSharp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Evolution API
builder.Services.AddEvolutionApi(options =>
{
    options.BaseUrl = builder.Configuration.GetConnectionString("EvolutionApi") ?? "http://localhost:8080";
    options.ApiKey = builder.Configuration["EvolutionApi:ApiKey"] ?? "";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();