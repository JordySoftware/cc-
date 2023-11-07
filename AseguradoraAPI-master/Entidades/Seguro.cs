using System.ComponentModel.DataAnnotations;

namespace AseguradoraViamatica.Entidades
{
    public class Seguro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string NombreSeguro { get; set; }
        [Required]
        public int CodigoSeguro { get; set; }
        [Required]
        public double SumadaAsegurada { get; set; }
        [Required]
        public double Prima { get; set; }
        public List<SegurosAsegurados> SegurosAsegurados { get; set; }
    }
}
