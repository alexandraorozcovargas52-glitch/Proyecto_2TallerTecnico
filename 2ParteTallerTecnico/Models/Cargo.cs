using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2tallertecnico.Models
{
    [Table("Cargo")]
    public class Cargo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdCargo")]
        public int IdCargo { get; set; }

        [NotMapped]
        public int CargoId
        {
            get { return IdCargo; }
            set { IdCargo = value; }
        }

        [Required(ErrorMessage = "El nombre del cargo es requerido")]
        [StringLength(100)]
        [Column("NombreCargo")]
        [Display(Name = "Nombre del Cargo")]
        public string NombreCargo { get; set; }

        [NotMapped]
        public string Descripcion { get; set; }

        [NotMapped]
        public bool Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        public Cargo()
        {
            Usuarios = new HashSet<Usuario>();
            Estado = true;
            Descripcion = "";
        }
    }
}