using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2tallertecnico.Models
{
    [Table("Persona")]
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [NotMapped]
        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}".Trim(); }
        }

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [StringLength(200)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        public Persona()
        {
            Estado = true;
            Nombre = "";
            Apellido = "";
            Telefono = "";
            Direccion = "";
        }
    }
}