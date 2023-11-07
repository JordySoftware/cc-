using AseguradoraViamatica.DTOs.Asegurado;
using AseguradoraViamatica.DTOs.Seguro;
using AseguradoraViamatica.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AseguradoraViamatica.Controllers
{
    [ApiController]
    [Route("api/seguros")]
    public class SegurosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SegurosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<SeguroDTO>>> Get()
        {
            var seguros = await context.Seguros.Include(s => s.SegurosAsegurados).ThenInclude(sa => sa.Asegurado).ToListAsync();
            return mapper.Map<List<SeguroDTO>>(seguros);
        }

        [HttpGet("{id:int}", Name = "obtenerSeguro")]
        public async Task<ActionResult> Get(int id)
        {
            var entidad = await context.Seguros.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<SeguroDTO>(entidad);
            return Ok(dto);
        }
        [HttpGet("asegurados/{codigoSeguro}")]
        public async Task<ActionResult<List<AseguradoDTO>>> GetAseguradosByCodigoSeguro(int codigoSeguro)
        {
            var asegurados = await context.Asegurados
                .Include(a => a.SegurosAsegurados)
                .Where(a => a.SegurosAsegurados.Any(sa => sa.Seguro.CodigoSeguro == codigoSeguro))
                .ToListAsync();

            var aseguradosDTO = mapper.Map<List<AseguradoDTO>>(asegurados);
            return Ok(aseguradosDTO);
        }
        [HttpGet("codigo/{codigoSeguro}")]
        public async Task<ActionResult<List<SeguroDTO>>> GetSegurosByCodigo(string codigoSeguro)
        {
            List<Seguro> seguros;
            if (int.TryParse(codigoSeguro, out int codigoSeguroNumerico))
            {
                seguros = await context.Seguros
                    .Include(s => s.SegurosAsegurados)
                    .ThenInclude(sa => sa.Asegurado)
                    .Where(s => s.CodigoSeguro == codigoSeguroNumerico)
                    .ToListAsync();
            }
            else
            {
                seguros = await context.Seguros
                    .Include(s => s.SegurosAsegurados)
                    .ThenInclude(sa => sa.Asegurado)
                    .Where(s => s.CodigoSeguro.ToString() == codigoSeguro)
                    .ToListAsync();
            }

            var segurosDTO = mapper.Map<List<SeguroDTO>>(seguros);
            return Ok(segurosDTO);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SeguroCreacionDTO seguroCreacionDTO)
        {
            // Verificar si algún campo requerido está vacío o nulo
            if (string.IsNullOrEmpty(seguroCreacionDTO.NombreSeguro) || seguroCreacionDTO.CodigoSeguro == 0 || seguroCreacionDTO.SumadaAsegurada == 0 || seguroCreacionDTO.Prima == 0)
            {
                // Al menos uno de los campos requeridos está vacío, devuelve una respuesta de error
                return BadRequest("Todos los campos son requeridos");
            }

            var entidad = mapper.Map<Seguro>(seguroCreacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();
            var seguroDTO = mapper.Map<SeguroDTO>(entidad);
            return new CreatedAtRouteResult("obtenerSeguro", new { id = seguroDTO.Id }, seguroDTO);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SeguroCreacionDTO seguroCreacionDTO)
        {
            // Verificar si algún campo requerido está vacío o nulo
            if (string.IsNullOrEmpty(seguroCreacionDTO.NombreSeguro) || seguroCreacionDTO.CodigoSeguro == 0 || seguroCreacionDTO.SumadaAsegurada == 0 || seguroCreacionDTO.Prima == 0)
            {
                // Al menos uno de los campos requeridos está vacío, devuelve una respuesta de error
                return BadRequest("Todos los campos son requeridos");
            }

            var entidad = mapper.Map<Seguro>(seguroCreacionDTO);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Seguros.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Seguro() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
