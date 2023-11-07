using AseguradoraViamatica.DTOs.Seguro;
using System.ComponentModel.DataAnnotations;

namespace AseguradoraViamatica.DTOs.Asegurado
{
    public class AseguradoDTO
    {
        public int Id { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int Edad { get; set; }
        public List<SeguroDTO> Seguros { get; set; }
    }
}
