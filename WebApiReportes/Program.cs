using Microsoft.EntityFrameworkCore;
using WebApiReportes.context;
using WebApiReportes.Repositories;
using WebApiReportes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//variable cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//registrar servicio para la conexion a la base de datos
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<ICargoService, CargoService>(); // Inyección del servicio
builder.Services.AddScoped<ICargoRepository, CargoRepository>(); // Si usas repositorio
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
