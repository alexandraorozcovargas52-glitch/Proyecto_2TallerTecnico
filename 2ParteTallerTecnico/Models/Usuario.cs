using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2tallertecnico.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [NotMapped]
        public int Id
        {
            get { return IdUsuario; }
            set { IdUsuario = value; }
        }

        [Required(ErrorMessage = "El usuario es requerido")]
        [StringLength(200)]
        [Column("Usuario")]
        [Display(Name = "Usuario")]
        public string UsuarioNombre { get; set; }

        [NotMapped]
        public string Nombre
        {
            get { return UsuarioNombre; }
            set { UsuarioNombre = value; }
        }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(200)]
        [Column("Apellido")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        [StringLength(300)]
        [Column("Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [NotMapped]
        public string Correo
        {
            get { return Email; }
            set { Email = value; }
        }

        [StringLength(400)]
        [Column("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Column("Estado")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Column("Fecha_Registro")]
        [Display(Name = "Fecha de Registro")]
        [DataType(DataType.DateTime)]
        public DateTime? Fecha_Registro { get; set; }

        [Column("IdCargo")]
        [Display(Name = "Cargo")]
        public int IdCargo { get; set; }

        [ForeignKey("IdCargo")]
        public virtual Cargo Cargo { get; set; }

        [NotMapped]
        public string NombreCargo
        {
            get { return Cargo?.NombreCargo ?? "Sin cargo"; }
        }

        public Usuario()
        {
            Estado = true;
            Fecha_Registro = DateTime.Now;
            Password = ""; // Valor por defecto
        }
    }
}