using Api.Middleware;
using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Services = builder.Services;

#region Services

Services.AddControllers();
// more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
Services.AddEndpointsApiExplorer();
Services.AddSwaggerGen();
Services.AddDbContext<ShopContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

Services.AddScoped<IProduct, ProductService>();
Services.AddScoped<IBasketService, BasketService>();

Services.AddCors();
#endregion

var app = builder.Build();
#region CreateDataBase
using var scop = app.Services.CreateScope();
var context = scop.ServiceProvider.GetRequiredService<ShopContext>();

try
{
    context.Database.Migrate();
    SeedData.Initialize(context);
}
catch (Exception e)
{

}
#endregion
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(opt => {
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
