using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio.Model
{

    [Table("consultas")]
    public class Consulta
    {
        [Key]
        public int id_consulta { get; set; }

        public int id_medico { get; set; }
        public int id_paciente { get; set; }
        public DateTime fecha_consulta { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento { get; set; }
        public string estado { get; set; }

        [ForeignKey("id_medico")]
        public virtual Usuario medico { get; set; }

        [ForeignKey("id_paciente")]
        public virtual Paciente paciente { get; set; }
    }
}
