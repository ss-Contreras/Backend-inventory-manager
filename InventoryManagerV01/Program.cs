using InventoryManagerV01.Data;
using InventoryManagerV01.Repositorio;
using InventoryManagerV01.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using InventoryManagerV01.ProductosMappers;
using InventoryManagerV01.Models;
using Microsoft.AspNetCore.Identity;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
                opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

// Soporte para autenticación con .NET Identity
builder.Services.AddIdentity<AppUsuario, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

// Soporte para cache
var apiVersioningBuilder = builder.Services.AddApiVersioning(opcion =>
{
    opcion.AssumeDefaultVersionWhenUnspecified = true;
    opcion.DefaultApiVersion = new ApiVersion(1, 0);
    opcion.ReportApiVersions = true;
});

// Agregamos los Repositorios
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IProductosRepositorio, ProductosRepositorio>();
builder.Services.AddScoped<IProveedoresRepositorio, ProveedoresRepositorio>();
builder.Services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddScoped<IVentasRepositorio, VentasRepositorio>();
builder.Services.AddScoped<IEmpleadosRepositorio, EmpleadosRepositorio>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

// Agregamos el AutoMapper
builder.Services.AddAutoMapper(typeof(ProductosMapper));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllers(opcion =>
{
    // Cache profile. Un cache global y así no tener que ponerlo en todas partes
    opcion.CacheProfiles.Add("PorDefecto30Segundos", new CacheProfile() { Duration = 30 });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Soporte para CORS
// Configuración de CORS más flexible para el desarrollo, permitiendo cualquier origen.
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaCors",
        builder =>
        {
            builder.AllowAnyOrigin() // Permitir cualquier origen (esto se puede restringir en producción)
                   .AllowAnyMethod() // Permitir cualquier método HTTP (GET, POST, PUT, PATCH, DELETE, etc.)
                   .AllowAnyHeader(); // Permitir cualquier cabecera
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Soporte para CORS
app.UseCors("PoliticaCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
