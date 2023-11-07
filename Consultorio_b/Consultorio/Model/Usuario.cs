using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio.Model
{

    [Table("usuario")]
    public class Usuario
    {

        [Key]
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }

        public string email { get; set; }
        public string contrasena { get; set; }
        public int id_perfil { get; set; }
        public string estado { get; set; }

        [ForeignKey("id_perfil")]
        public virtual Perfil Perfil { get; set; }

    }
}
