using System;
using System.ComponentModel.DataAnnotations;
namespace Model.Domain
{
    public class Historial_Roles
    {
        [Key]
        public int Legajo_Empleado { get; set; }
        public int Id_Rol { get; set; }
        public DateTime Fecha_Inicio { get; set; }
    }
}