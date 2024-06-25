using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionEntrenamientoDeportivo.Models
{
    public class Rutina
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        //para las imagenes
        public string? PublicId { get; set; }
        public string? Url { get; set; }
        public string? SecureUrl { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [JsonIgnore]
        public List<Ejercicio> Ejercicios { get; set; }

    }
}
