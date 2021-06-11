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
    [Route("api/veterinarios")]
    public class VeterinarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public VeterinarioController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Veterinario>>> Get()
        {
            return await context.Veterinarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> GetById(int id)
        {
            var veterinario = await context.Veterinarios.FirstOrDefaultAsync(x => x.Id == id);
            if(veterinario == null)
            {
                return NotFound();
            }

            return veterinario;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Veterinario veterinario)
        {
            context.Add(veterinario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Veterinario veterinario, int id)
        {
            if(veterinario.Id != id)
            {
                return BadRequest("El Veterianrio no coincide con el Id de la URL ");
            }

            var existe = await context.Veterinarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(veterinario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Veterinarios.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound();
            }

            context.Remove(new Veterinario() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
