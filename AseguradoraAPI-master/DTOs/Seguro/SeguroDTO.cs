using AseguradoraViamatica.DTOs.Asegurado;
using System.ComponentModel.DataAnnotations;

namespace AseguradoraViamatica.DTOs.Seguro
{
    public class SeguroDTO
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
        public List<AseguradoDTO> Asegurados { get; set; }
    }
}
