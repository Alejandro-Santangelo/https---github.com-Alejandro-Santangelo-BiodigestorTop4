using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Biodigestor.Data; // Asegúrate de que esta ruta sea correcta
using Microsoft.AspNetCore.Identity;
using Biodigestor.Models; // Asegúrate de que esta ruta sea correcta
 // Asegúrate de que esta ruta sea correcta
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Agregar servicios al contenedor.
builder.Services.AddControllers();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// Registrar la base de datos con EF Core
builder.Services.AddDbContext<BiodigestorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biodigestor API", Version = "v1" });
});

// Configurar JWT Authentication


// Agregar políticas de autorización


var app = builder.Build();

// Método para inicializar los roles


// Configurar la tubería de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biodigestor API V1");
        c.RoutePrefix = "swagger"; // Puedes ajustar esto según tus necesidades
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Método para inicializar los roles


















