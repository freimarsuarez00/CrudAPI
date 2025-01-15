using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;

namespace CrudAPI.Services
{
    public class PerfilServices
    {
        private readonly AppDbContext _context;
        public PerfilServices(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<PerfilDTO>> Lista()
        {
            var listaDTO = new List<PerfilDTO>();
            var listaDB = await _context.Perfiles.ToListAsync();
            foreach (var item in listaDB)
            {
                listaDTO.Add(new PerfilDTO
                {
                    idPerfil = item.idPerfil,
                    Nombre = item.Nombre,
                });
            }
            return listaDTO;
        }

    }
}
