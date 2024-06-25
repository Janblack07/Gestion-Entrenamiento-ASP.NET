using System.ComponentModel.DataAnnotations;

namespace GestionEntrenamientoDeportivo.DTO
{
    public class UsuarioRegistrado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
       
        public string Apellido { get; set; }
        
        public int Edad { get; set; }
  
        public string CorreoElectronico { get; set; }

        public string Contrasena { get; set; }
        public string UserName { get; set; }
    }
}
