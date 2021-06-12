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

        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetById(int id)
        {
            var mascota = await context.Mascotas.FirstOrDefaultAsync(x => x.Id == id);
            if (mascota == null)
            {
                return BadRequest("La Mascota fue eliminada o no existe");
            }

            return mascota;
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Mascota mascota, int id)
        {
            if (mascota.Id != id)
            {
                return BadRequest("La mascota no coincide con el Id de la URL ");
            }

            var existe = await context.Mascotas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(mascota);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Mascotas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Mascota() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
