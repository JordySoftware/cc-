using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio.Model
{

    [Table("perfiles")]
    public class Perfil
    {
        [Key]
        public int id_perfil { get; set; }
        public string nombre_perfil { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
    }
}
