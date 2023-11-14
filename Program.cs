using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart,  ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddRazorPages(); 

builder.Services.AddDbContext<BethanysPieShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BethanysPieShopDbContextConnection")));

//builder.Services.AddControllers();  already covered by AddControllersWithViews() higher up
builder.Services.AddServerSideBlazor();
var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapBlazorHub();

app.MapFallbackToPage("/app/{*catchall}", "/App/Index");
DbInitializer.Seed(app);
app.Run();
