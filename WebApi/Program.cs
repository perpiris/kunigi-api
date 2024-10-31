using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddControllers();
}

var app = builder.Build();
{
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
        .WithOrigins("http://localhost:4200", "https://localhost:4200"));
    
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}