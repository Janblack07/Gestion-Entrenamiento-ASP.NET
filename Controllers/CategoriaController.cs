using GestionEntrenamientoDeportivo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEntrenamientoDeportivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly DBgestion _context;

        public CategoriaController(DBgestion context)
        {
            _context = context;
        }

        // Este método ya no retorna IActionResult, solo realiza la inicialización
        private async Task InicializarCategorias()
        {
            string[] categoryNames = {
                "pantorrilla: Ejercicio para trabajar los músculos de la pantorrilla.",
                "izquios: Ejercicio para fortalecer los músculos isquiotibiales.",
                "gluteos: Ejercicio enfocado en los músculos glúteos.",
                "cuadriceps: Ejercicio para desarrollar los músculos del cuádriceps.",
                "triceps: Ejercicio para fortalecer los músculos tríceps.",
                "biceps: Ejercicio para desarrollar los músculos bíceps.",
                "antebrazo: Ejercicio para fortalecer los músculos del antebrazo.",
                "pecho alto: Ejercicio para trabajar la parte superior del músculo pectoral.",
                "pecho bajo: Ejercicio para desarrollar la parte inferior del músculo pectoral.",
                "pecho medio: Ejercicio para fortalecer la parte media del músculo pectoral.",
                "espalda densidad: Ejercicio para aumentar la densidad muscular de la espalda.",
                "espalda anchura: Ejercicio para ensanchar la espalda.",
                "hombro: Ejercicio para fortalecer los músculos deltoides del hombro."
            };

            foreach (var categoryInfo in categoryNames)
            {
                var parts = categoryInfo.Split(":");
                var categoryName = parts[0].Trim();
                var description = parts[1].Trim();

                if (!await _context.Categorias.AnyAsync(c => c.Nombre == categoryName))
                {
                    _context.Categorias.Add(new Categoria { Nombre = categoryName, Descripcion = description });
                }
            }

            await _context.SaveChangesAsync();
        }

        [Authorize]
        [HttpGet]
        [Route("VerCategorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategorias()
        {
            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Acceso no autorizado. Debes estar autenticado." });
            }

            // Inicializa las categorías si es necesario (accesible tanto para Admin como usuarios)
            await InicializarCategorias();

            // Ahora lista las categorías
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(new { message = "Todas las Categorias: ", categorias });
        }
    }
}
