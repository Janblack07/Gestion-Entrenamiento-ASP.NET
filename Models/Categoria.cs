using System.Text.Json.Serialization;

namespace GestionEntrenamientoDeportivo.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [JsonIgnore]
        public List<CategoriaEjercicio> EjercicioCategorias { get; set; }
    }
}
