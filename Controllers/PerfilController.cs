using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudAPI.Entities;

using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Services;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilServices _perfilService;
        public PerfilController(PerfilServices perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<List<PerfilDTO>>> Get()
        {
            return Ok(await _perfilService.Lista());
        }
    }
}
