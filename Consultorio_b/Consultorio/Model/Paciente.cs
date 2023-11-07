
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Consultorio.Model
{

    [Table("paciente")]
    public class Paciente
    {
        [Key]
        public int id_paciente { get; set; }
        public string nombre_paciente { get; set; }
        public string apellido_paciente { get; set; }
        public DateTime fecha_nac { get; set; }

        public string direccion { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; }



    }
}
