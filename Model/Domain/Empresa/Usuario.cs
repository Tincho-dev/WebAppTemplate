using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }
        public string Contraseña { get; set; }
        public int LegajoEmpleado { get; set; }
        [ForeignKey("LegajoEmpleado")]
        public virtual Empleado Empleado { get; set; }
    }
}