using System.ComponentModel.DataAnnotations;

namespace GestionEntrenamientoDeportivo.Models
{
    public class RegistroProgreso
    {
        public int Id { get; set; }

        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int EjercicioId { get; set; }
        public Ejercicio? Ejercicio { get; set; }


        public DateTime Fecha { get; set; }

        public int? Repeticiones { get; set; }
        public int? Series { get; set; }
        public float? Peso { get; set; }
        public string Comentarios { get; set; }
    }
}
