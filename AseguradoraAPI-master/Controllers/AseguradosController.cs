using AseguradoraViamatica.DTOs.Asegurado;
using AseguradoraViamatica.DTOs.Seguro;
using AseguradoraViamatica.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AseguradoraViamatica.Controllers
{
    [ApiController]
    [Route("api/asegurados")]
    public class AseguradosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AseguradosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AseguradoDTO>>> Get()
        {
            var entidades = await context.Asegurados
                .Include(x => x.SegurosAsegurados)
                .ThenInclude(sa => sa.Seguro)
                .ToListAsync();

            var dtos = mapper.Map<List<AseguradoDTO>>(entidades);
            return dtos;
        }




        [HttpGet("{id:int}", Name = "obtenerAsegurado")]
        public async Task<ActionResult> Get(int id)
        {
            var entidad = await context.Asegurados
                .Include(x => x.SegurosAsegurados)
                .ThenInclude(sa => sa.Seguro)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<AseguradoDTO>(entidad);
            return Ok(dto);
        }
        [HttpGet("cedula2/{numeroCedula}")]
        public async Task<ActionResult<AseguradoDTO>> GetAseguradoByNumeroCedula(string numeroCedula)
        {
            var asegurado = await context.Asegurados
                .Include(a => a.SegurosAsegurados)
                .ThenInclude(sa => sa.Seguro)
                .FirstOrDefaultAsync(a => a.Cedula == numeroCedula);

            if (asegurado == null)
            {
                return NotFound();
            }

            var aseguradoDTO = mapper.Map<AseguradoDTO>(asegurado);
            return Ok(aseguradoDTO);
        }
        [HttpGet("cedula/{numeroCedula}")]
        public async Task<ActionResult<List<SeguroDTO>>> GetSegurosByNumeroCedula(string numeroCedula)
        {
            var asegurado = await context.Asegurados
                .Include(a => a.SegurosAsegurados)
                .ThenInclude(sa => sa.Seguro)
                .FirstOrDefaultAsync(a => a.Cedula == numeroCedula);

            if (asegurado == null)
            {
                return NotFound();
            }

            var seguros = asegurado.SegurosAsegurados.Select(sa => sa.Seguro).ToList();
            var segurosDTO = mapper.Map<List<SeguroDTO>>(seguros);
            return Ok(segurosDTO);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AseguradoCreacionDTO aseguradoCreacionDTO)
        {
            var asegurado = mapper.Map<Asegurado>(aseguradoCreacionDTO);

            // Verificar si se proporcionaron seguros
            if (aseguradoCreacionDTO.Seguros != null && aseguradoCreacionDTO.Seguros.Any())
            {
                var segurosAsegurados = aseguradoCreacionDTO.Seguros.Select(s => new SegurosAsegurados { SeguroId = s.SeguroId }).ToList();
                asegurado.SegurosAsegurados = segurosAsegurados;
            }

            context.Add(asegurado);
            await context.SaveChangesAsync();

            var aseguradoDTO = mapper.Map<AseguradoDTO>(asegurado);
            return new CreatedAtRouteResult("obtenerAsegurado", new { id = aseguradoDTO.Id }, aseguradoDTO);
        }
        [HttpPost("{id}/seguros")]
        public async Task<ActionResult> AddSegurosToAsegurado(int id, [FromBody] List<int> segurosIds)
        {
            var asegurado = await context.Asegurados.Include(x => x.SegurosAsegurados).FirstOrDefaultAsync(x => x.Id == id);
            if (asegurado == null)
            {
                return NotFound();
            }

            foreach (var seguroId in segurosIds)
            {
                if (asegurado.SegurosAsegurados.Any(sa => sa.SeguroId == seguroId))
                {
                    // El asegurado ya tiene asignado este seguro, devuelve una respuesta de error
                    return BadRequest("El seguro ya está asignado al asegurado.");
                }

                asegurado.SegurosAsegurados.Add(new SegurosAsegurados() { SeguroId = seguroId, AseguradoId = asegurado.Id });
            }

            await context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AseguradoCreacionDTO aseguradoCreacionDTO)
        {
            var aseguradoDB = await context.Asegurados
                .Include(x => x.SegurosAsegurados)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (aseguradoDB == null)
            {
                return NotFound();
            }

            mapper.Map(aseguradoCreacionDTO, aseguradoDB);

            // Obtener el seguro por ID
            var seguroId = aseguradoCreacionDTO.Seguros.FirstOrDefault()?.SeguroId;
            if (seguroId.HasValue)
            {
                var seguroAsegurado = aseguradoDB.SegurosAsegurados
                    .FirstOrDefault(sa => sa.SeguroId == seguroId.Value);

                if (seguroAsegurado != null)
                {
                    // Actualizar los atributos del seguro si es necesario
                    mapper.Map(aseguradoCreacionDTO.Seguros.FirstOrDefault(), seguroAsegurado.Seguro);
                }
            }

            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Asegurados.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Asegurado() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
