using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Letra
    {
        //clave primaria para la base de datos
        [Key]
        public string Id { get; set; }
        //establecemos las propiedades de modo de "solo lectura" al no tener el Set para cambiar sus valores
        [Required]
        public char Name { get; }
        [Required]
        public double Probability { get; }
        public double Information { get; }
        public string Codigo { get; set; } = string.Empty; //el codigo es vacio, la fuente es quien luego lo establece
        public int FrecuenciaDeAparicion { get; set; }

        public string IdFuente { get; set; }
        [ForeignKey("IdFuente")]
        public virtual Fuente Fuente { get; set; }
        public Letra()
        {
            Id = Guid.NewGuid().ToString();
        }
        //recibimos el nombre y la probabilidad al crear la letra, la informacion se calcula en funcoin de la probabilidad
        public Letra(char name, double probability, int freq, string idFuente)
        {
            Id = name.GetHashCode().ToString() + idFuente;
            Name = name;
            this.Probability = probability;
            //calculamos la informacion segun la ecuacion que depende de la probabilidad
            Information = Math.Log(1 / probability)/Math.Log(2);
            FrecuenciaDeAparicion = freq;
            IdFuente = idFuente;
        }

        public Letra(char name, double probability)
        {
            Id = new Guid().ToString();
            Name = name;
            this.Probability = probability;
            //calculamos la informacion segun la ecuacion que depende de la probabilidad
            Information = Math.Log(1 / probability)/Math.Log(2);
        }
    }
}