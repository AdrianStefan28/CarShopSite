var builder = WebApplication.CreateBuilder(args);

// Adaugă serviciile necesare în containerul de dependențe
builder.Services.AddControllersWithViews();

// Configurează HttpClient cu adresa de bază a microserviciilor
builder.Services.AddHttpClient("Microservices", client =>
{
    client.BaseAddress = new Uri("https://localhost:7137"); // Adresa microserviciilor tale
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configurează pipeline-ul de cereri HTTP
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
