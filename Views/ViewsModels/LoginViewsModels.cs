using System.ComponentModel.DataAnnotations;

namespace prueba2maya.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        public bool RememberMe { get; set; }
    }
}
