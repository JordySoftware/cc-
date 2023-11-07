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
    public class PacienteController : ControllerBase
    {

        public readonly ConsultorioDBContext _dbcontext;

        public PacienteController(ConsultorioDBContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Paciente> lista = new List<Paciente>();

            try
            {
                lista = _dbcontext.Pacientes.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });

            }

        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Paciente objeto)
        {


            try
            {
                _dbcontext.Pacientes.Add(objeto);
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
        public IActionResult Editar([FromBody] Paciente objeto)
        {
            Paciente pac = _dbcontext.Pacientes.Find(objeto.id_paciente);

            if (pac == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {
                pac.id_paciente = objeto.id_paciente is 0 ? pac.id_paciente : objeto.id_paciente;
                pac.nombre_paciente = objeto.nombre_paciente is null ? pac.nombre_paciente : objeto.nombre_paciente;
                pac.apellido_paciente = objeto.apellido_paciente is null ? pac.apellido_paciente : objeto.apellido_paciente;
                pac.fecha_nac = objeto.fecha_nac.ToString() is null ? pac.fecha_nac : objeto.fecha_nac;
                pac.direccion = objeto.direccion is null ? pac.direccion : objeto.direccion;
                pac.telefono = objeto.telefono is null ? pac.telefono : objeto.telefono;
                pac.estado = objeto.estado is null ? pac.estado : objeto.estado;



                _dbcontext.Pacientes.Update(pac);
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
        public IActionResult Eliminar(int id_paciente)
        {

            Paciente pac = _dbcontext.Pacientes.Find(id_paciente);

            if (pac == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {

                _dbcontext.Pacientes.Remove(pac);
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
