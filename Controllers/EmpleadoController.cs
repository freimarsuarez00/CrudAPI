using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.Entities;
using CrudAPI.DTOs;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly AppDbContext _Context;
        public EmpleadoController(AppDbContext context)
        {
            _Context = context;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get()
        {
            var listaDTO = new List<EmpleadoDTO>();
            var listaDB = await _Context.Empleados.Include(p => p.PerfilReferencia).ToListAsync();
            foreach (var item in listaDB)
            {
                listaDTO.Add(new EmpleadoDTO
                {
                    IdEmpleado = item.IdEmpleado,
                    NombreCompleto = item.NombreCompleto,
                    Sueldo = item.Sueldo,
                    IdPerfil = item.IdPerfil,
                    NombrePerfil = item.PerfilReferencia.Nombre
                });
            }
            return Ok(listaDTO);
        }


        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get(int id)
        {
            var empleadoDTO = new EmpleadoDTO();
            var empleadoDB = await _Context.Empleados.Include(p => p.PerfilReferencia)
                .Where(e => e.IdEmpleado == id).FirstAsync();

            empleadoDTO.IdEmpleado = id;
            empleadoDTO.NombreCompleto = empleadoDB.NombreCompleto;
            empleadoDTO.Sueldo = empleadoDB.Sueldo;
            empleadoDTO.IdPerfil = empleadoDB.IdPerfil;
            empleadoDTO.NombrePerfil = empleadoDB.PerfilReferencia.Nombre;
            return Ok(empleadoDTO);

        }



        [HttpPost]
        [Route("Guardar")]
        public async Task<ActionResult<EmpleadoDTO>> Post(EmpleadoDTO empleadoDTO)
        {
            var empleadoDB = new Empleado
            {
                NombreCompleto = empleadoDTO.NombreCompleto,
                Sueldo = empleadoDTO.Sueldo,
                IdPerfil = empleadoDTO.IdPerfil,
            };
            await _Context.Empleados.AddAsync(empleadoDB);
            await _Context.SaveChangesAsync();
            return Ok("Empleado Agregaro");
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<ActionResult<EmpleadoDTO>> Put(EmpleadoDTO empleadoDTO)
        {
            var empleadoDB = await _Context.Empleados.Include(p => p.PerfilReferencia)
                .Where(e => e.IdEmpleado == empleadoDTO.IdEmpleado).FirstAsync();

            empleadoDB.NombreCompleto = empleadoDTO.NombreCompleto;
            empleadoDB.Sueldo = empleadoDTO.Sueldo;
            empleadoDB.IdPerfil = empleadoDTO.IdPerfil;

            _Context.Empleados.Update(empleadoDB);
            await _Context.SaveChangesAsync();
            return Ok("Empleado Actualizado Satisfactoriamente");
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult<EmpleadoDTO>> Delete(int id)
        {
            //var empleadoDB = await _Context.Empleados.Where(e => e.IdEmpleado == id).FirstOrDefaultAsync();
            var empleadoDB = await _Context.Empleados.FindAsync(id);

            if (empleadoDB == null) return NotFound("Empleado No Encontrado");
            _Context.Empleados.Remove(empleadoDB);
            await _Context.SaveChangesAsync();

            return Ok("Empleado Eliminado Satisfactoriamente");

        }

    }
}
