namespace GestionEntrenamientoDeportivo.Models
{
    public class CategoriaEjercicio
    {
        public int Id { get; set; }
        public int EjercicioId { get; set; }
        public Ejercicio? Ejercicio { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
