using System.ComponentModel.DataAnnotations;

namespace Model.Custom
{
    public class UserGrid
    {
        public string Id { get; set; }
        [Display(Name = "Nombre: ")]
        public string Name { get; set; }
        [Display(Name = "Apellido: ")]
        public string LastName { get; set; }
        [Display(Name = "Email: ")]
        public string Email { get; set; }
        public bool enabled { get; set; }
        public string Role { get; set; }
    }
}
