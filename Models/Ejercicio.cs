using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionEntrenamientoDeportivo.Models
{
    public class Ejercicio
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        //para las imagenes
        public string? PublicId { get; set; }
        public string? Url { get; set; }
        public string? SecureUrl { get; set; }

        // Propiedades para videos
        public string? VideoPublicId { get; set; }
        public string? VideoUrl { get; set; }
        public string? VideoSecureUrl { get; set; }

        public int RutinaId { get; set; }
        public Rutina? Rutina { get; set; }


        [JsonIgnore]
        public List<RegistroProgreso> RegistrosProgreso { get; set; }
        [JsonIgnore]
        public List<CategoriaEjercicio> EjercicioCategorias { get; set; }


    }
}
