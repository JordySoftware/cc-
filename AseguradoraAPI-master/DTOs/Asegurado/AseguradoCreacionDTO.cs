using AseguradoraViamatica.DTOs.Seguro;
using AseguradoraViamatica.Entidades;
using System.ComponentModel.DataAnnotations;

namespace AseguradoraViamatica.DTOs.Asegurado
{
    public class AseguradoCreacionDTO
    {
        [Required]
        public string Cedula { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int Edad { get; set; }
        public List<SeguroAseguradosDTO> Seguros { get; set; }
    }
}
