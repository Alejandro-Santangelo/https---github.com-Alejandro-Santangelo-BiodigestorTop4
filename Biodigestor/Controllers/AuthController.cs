
/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biodigestor.Data;
using System.Linq; // Para LINQ
using Biodigestor.Models; // Asegúrate de importar el espacio de nombres correcto
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Biodigestor.Controllers
{


[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly BiodigestorContext _context;

    public AuthController(BiodigestorContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UsuarioRegistradoModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verificación de la contraseña
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest(new { message = "Las contraseñas no coinciden" });
        }

        // Verificar si el DNI está en la tabla Clientes o Personal
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.DNI == model.DNI);
        var personal = await _context.Personal.FirstOrDefaultAsync(p => p.DNI == model.DNI);

        if (cliente != null)
        {
            model.Rol = "Cliente";
        }
        else if (personal != null)
        {
            model.Rol = personal.Rol; // Asignar el rol basado en la tabla Personal
        }
        else
        {
            return BadRequest(new { message = "DNI no encontrado en Clientes o Personal" });
        }

        // Crear el nuevo usuario
        var nuevoUsuario = new UsuarioRegistradoModel
        {
           Username = model.Username,
           Email = model.Email,
           Password = model.Password,
           ConfirmPassword = model.ConfirmPassword,  // Asegúrate de asignarlo
           DNI = model.DNI,
           Rol = "Cliente"  // O el rol que corresponda
        };

        _context.UsuariosRegistrados.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Registro exitoso" });
    }

    [HttpPost("verificarDNI")]
public IActionResult VerificarDNI([FromBody] VerificarDNIRequest request)
{
    if (request == null || request.DNI <= 0)
    {
        return BadRequest(new { message = "DNI inválido." });
    }

    var dni = request.DNI;

    // Verifica si el DNI existe en la tabla Clientes
    var cliente = _context.Clientes.FirstOrDefault(c => c.DNI == dni);

    if (cliente != null)
    {
        return Ok(new { message = "Cliente encontrado. Proceda al registro." });
    }

    // Verifica si el DNI existe en la tabla Personal
    var personal = _context.Personal.FirstOrDefault(p => p.DNI == dni);

    if (personal != null)
    {
        return Ok(new { message = "Personal encontrado. Proceda al registro." });
    }

    // Si el DNI no está en ninguna tabla
    return BadRequest(new { message = "DNI no encontrado." });
}


}
}
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biodigestor.Data;
using Biodigestor.Models;
using System.Threading.Tasks;
using Biodigestor.DTOs;

namespace Biodigestor.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly BiodigestorContext _context;

        public AuthController(BiodigestorContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
public async Task<IActionResult> Register([FromBody] UsuarioRegistradoDto dto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Verificar si el DNI está en la tabla Clientes o Personal
    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.DNI == dto.DNI);
    var personal = await _context.Personal.FirstOrDefaultAsync(p => p.DNI == dto.DNI);

    string rol;
    if (cliente != null)
    {
        rol = "Cliente";
    }
    else if (personal != null)
    {
        rol = personal.Rol; // Asignar el rol basado en la tabla Personal
    }
    else
    {
        return BadRequest(new { message = "DNI no encontrado en Clientes o Personal" });
    }

    // Verificar si el username o email ya están registrados
    var existingUser = await _context.UsuariosRegistrados
        .AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email);
    
    if (existingUser)
    {
        return BadRequest(new { message = "El username o email ya están en uso." });
    }

    // Crear el nuevo usuario a partir del DTO
    var nuevoUsuario = new UsuarioRegistradoModel
    {
        Username = dto.Username,
        Email = dto.Email,
        Password = dto.Password, // Solo guardar la contraseña
        DNI = dto.DNI,
        Rol = rol // Asignar rol
    };

    _context.UsuariosRegistrados.Add(nuevoUsuario);
    await _context.SaveChangesAsync();

    return Ok(new { message = "Registro exitoso" });
}



        [HttpPost("verificarDNI")]
        public IActionResult VerificarDNI([FromBody] VerificarDNIRequest request)
        {
            if (request == null || request.DNI <= 0)
            {
                return BadRequest(new { message = "DNI inválido." });
            }

            var dni = request.DNI;

            // Verifica si el DNI existe en la tabla Clientes
            var cliente = _context.Clientes.FirstOrDefault(c => c.DNI == dni);
            if (cliente != null)
            {
                return Ok(new { message = "Cliente encontrado. Proceda al registro." });
            }

            // Verifica si el DNI existe en la tabla Personal
            var personal = _context.Personal.FirstOrDefault(p => p.DNI == dni);
            if (personal != null)
            {
                return Ok(new { message = "Personal encontrado. Proceda al registro." });
            }

            // Si el DNI no está en ninguna tabla
            return BadRequest(new { message = "DNI no encontrado." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Buscar al usuario en la tabla UsuariosRegistrados por Username
            var usuario = await _context.UsuariosRegistrados
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (usuario == null)
            {
                // Usuario no encontrado
                return Unauthorized(new { message = "Usuario no encontrado" });
            }

            // Verificar que la contraseña coincida
            if (usuario.Password != loginDto.Password)
            {
                // Contraseña incorrecta
                return Unauthorized(new { message = "Contraseña incorrecta" });
            }

            // Si el username y la password coinciden, se considera un inicio de sesión exitoso
            return Ok(new { message = "Inicio de sesión exitoso", usuario = usuario.Username });
        }
    }
}
