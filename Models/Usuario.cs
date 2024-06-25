using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionEntrenamientoDeportivo.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }

        public string CorreoElectronico { get; set; }

        public string Contrasena { get; set; }

        [JsonIgnore]
        public List<Rutina> Rutinas { get; set; }
        [JsonIgnore]
        public List<RegistroProgreso> RegistrosProgreso { get; set; }


    }
}
