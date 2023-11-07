using System.ComponentModel.DataAnnotations;

namespace AseguradoraViamatica.DTOs.Seguro
{
    public class SeguroCreacionDTO
    {
        [Required]
        [StringLength(120)]
        public string NombreSeguro { get; set; }
        [Required]
        public int CodigoSeguro { get; set; }
        [Required]
        public double SumadaAsegurada { get; set; }
        [Required]
        public double Prima { get; set; }
    }
}
