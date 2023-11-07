namespace AseguradoraViamatica.Entidades
{
    public class SegurosAsegurados
    {
        public int Id { get; set; }

        public Asegurado Asegurado { get; set; }
        public int AseguradoId { get; set; }

  
        public Seguro Seguro { get; set; }
        public int SeguroId { get; set; }
    }
}
