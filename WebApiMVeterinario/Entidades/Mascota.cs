using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMVeterinario.Entidades
{
    public class Mascota
    {
        public int Id { get; set; }
        public string NombreMascota { get; set; }
        public int VeterinarioId { get; set; }
        public Veterinario Veterinario { get; set; }
    }
}
