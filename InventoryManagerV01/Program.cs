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


//Soporte para autenticación con .NET Identity
builder.Services.AddIdentity<AppUsuario, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Soporte para cache
var apiVersioningBuilder = builder.Services.AddApiVersioning(opcion =>
{
    opcion.AssumeDefaultVersionWhenUnspecified = true;
    opcion.DefaultApiVersion = new ApiVersion(1, 0);
    opcion.ReportApiVersions = true;
    //opcion.ApiVersionReader = ApiVersionReader.Combine(
    //    new QueryStringApiVersionReader("api-version")//?api-version=1.0
    //    //new HeaderApiVersionReader("X-Version"),
    //    //new MediaTypeApiVersionReader("ver"));
    //);
});
//Agregamos los Repositorios
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//Agregamos el AutoMapper
builder.Services.AddAutoMapper(typeof(ProductosMapper));

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddControllers(opcion =>
{
    //Cache profile. Un cache global y así no tener que ponerlo en todas partes
    opcion.CacheProfiles.Add("PorDefecto30Segundos", new CacheProfile() { Duration = 30 });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Soporte para CORS
//Se pueden habilitar: 1-Un dominio, 2-multiples dominios,
//3-cualquier dominio (Tener en cuenta seguridad)
//Usamos de ejemplo el dominio: http://localhost:3223, se debe cambiar por el correcto
//Se usa (*) para todos los dominios
builder.Services.AddCors(p => p.AddPolicy("PoliticaCors", build =>
{
    build.AllowAnyOrigin() // Esto permite cualquier dominio (bueno para desarrollo, no para producción).
        .AllowAnyMethod()   // Permite todos los métodos HTTP (GET, POST, PUT, DELETE, etc.).
        .AllowAnyHeader();  // Permite cualquier encabezado.
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Soporte para CORS
app.UseCors("PoliticaCors");
app.UseAuthorization();

app.MapControllers();

app.Run();
