using Consultorio.Data;
using Consultorio.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Consultorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        public readonly ConsultorioDBContext _dbcontext;

        public PerfilController(ConsultorioDBContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Perfil> lista = new List<Perfil>();

            try
            {
                lista = _dbcontext.Perfiles.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }

        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Perfil objeto)
        {


            try
            {
                _dbcontext.Perfiles.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });
            }




        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Perfil objeto)
        {
            Perfil perf = _dbcontext.Perfiles.Find(objeto.id_perfil);

            if (perf == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {
                perf.id_perfil = objeto.id_perfil is 0 ? perf.id_perfil : objeto.id_perfil;
                perf.nombre_perfil = objeto.nombre_perfil is null ? perf.nombre_perfil : objeto.nombre_perfil;
                perf.descripcion = objeto.descripcion is null ? perf.descripcion : objeto.descripcion;
                perf.estado = objeto.estado is null ? perf.estado : objeto.estado;



                _dbcontext.Perfiles.Update(perf);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });
            }




        }

        [HttpDelete]
        [Route("Eliminar por el id")]
        public IActionResult Eliminar(int id_perfil)
        {

            Perfil perf = _dbcontext.Perfiles.Find(id_perfil);

            if (perf == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {

                _dbcontext.Perfiles.Remove(perf);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });
            }


        }
    }
}
