using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMVeterinario.Contexts;
using WebApiMVeterinario.Entidades;

namespace WebApiMVeterinario.Controllers
{
    [ApiController]
    [Route("api/mascotas")]
    public class MascotasController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public MascotasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mascota>>> Get()
        {
            return await context.Mascotas.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Mascota>> Get(int id)
        {
            return await context.Mascotas.Include(x => x.Veterinario).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Mascota mascota)
        {
            var existeVeterinario = await context.Veterinarios.AnyAsync(x => x.Id == mascota.VeterinarioId);
            if(!existeVeterinario)
            {
                return BadRequest($"No existe el veterinario de Id {mascota.VeterinarioId}");
            }

            context.Add(mascota);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
