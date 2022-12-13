using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class Empleado
    {
        [Key]
        [Required(ErrorMessage = "Debe Ingresar el Legajo del empleado")]
        [Display(Name = "Legajo: ")]
        public int Legajo { get; set; }
        [MaxLength(120)]
        [Required(ErrorMessage = "Debe Ingresar el Nombre del empleado")]
        [Display(Name = "Nombre: ")]
        public string Nombre { get; set; }
        [MaxLength(120)]
        [Required(ErrorMessage = "Debe Ingresar el Apellido del empleado")]
        [Display(Name = "Apellido: ")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe Ingresar el DNI del empleado")]
        [Display(Name = "DNI: ")]
        public int DNI { get; set; }
        [Required(ErrorMessage = "Debe Ingresar la Fecha de Nacimiento")]
        [Display(Name = "Fecha de Nacimiento: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Debe Ingresar la Fecha de Ingreso")]
        [Display(Name = "Fecha de Nacimiento: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
        public long Telefono { get; set; }

        [Display(Name = "Rol de Servicio")]
        public int Id_RolServicio { get; set; }

        [ForeignKey("Id_RolServicio")]
        public virtual RolEmp RolEmp { get; set; }

    }
}