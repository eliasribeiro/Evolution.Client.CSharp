using Evolution.Client.CSharp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adiciona o cliente da API Evolution
builder.Services.AddEvolutionApi(options => {
    options.BaseUrl = builder.Configuration["EvolutionApi:BaseUrl"] ?? "http://localhost:8080/";
    options.ApiKey = builder.Configuration["EvolutionApi:ApiKey"] ?? string.Empty;
    options.TimeoutSeconds = int.Parse(builder.Configuration["EvolutionApi:TimeoutSeconds"] ?? "30");
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
