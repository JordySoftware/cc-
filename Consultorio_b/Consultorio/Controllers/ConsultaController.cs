using Consultorio.Data;
using Consultorio.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Consultorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {

        public readonly ConsultorioDBContext _dbcontext;

        public ConsultaController(ConsultorioDBContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Consulta> lista = new List<Consulta>();

            try
            {
                lista = _dbcontext.Consultas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });

            }

        }

        [HttpGet]
        [Route("Lista Completa")]
        public IActionResult ListaC()
        {
            List<Consulta> lista = new List<Consulta>();

            try
            {
                lista = _dbcontext.Consultas.Include(i => i.medico).Include(c => c.paciente).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });

            }

        }

        [HttpPost]
        [Route("Nuevo Registro")]
        public IActionResult Guardar([FromBody] Consulta objeto)
        {


            try
            {
                // Verificar si ya existe una consulta con la misma fecha y hora
                bool consultaExistente = _dbcontext.Consultas.Any(c => c.fecha_consulta == objeto.fecha_consulta);

                if (consultaExistente)
                {
                    // Existe una consulta con la misma fecha y hora, devuelve un error
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Ya existe una consulta en la misma fecha y hora." });
                }
                else
                {
                    // No hay consulta existente con la misma fecha y hora, guarda el nuevo registro
                    _dbcontext.Consultas.Add(objeto);
                    _dbcontext.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
                }
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });
                /*return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });*/
            }

        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Consulta objeto)
        {
            Consulta Consul = _dbcontext.Consultas.Find(objeto.id_consulta);

            if (Consul == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {

                // Verificar si ya existe una consulta con la misma fecha y hora (excepto la consulta que se está editando)
                bool horaRepetida = _dbcontext.Consultas.Any(c =>
                    c.id_consulta != objeto.id_consulta && // Excluir la consulta que se está editando
                    c.fecha_consulta == objeto.fecha_consulta);

                if (horaRepetida)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Ya existe una consulta en la misma fecha y hora." });
                }


                Consul.id_consulta = objeto.id_consulta is 0 ? Consul.id_consulta : objeto.id_consulta;
                Consul.id_medico = objeto.id_consulta is 0 ? Consul.id_consulta : objeto.id_consulta;
                Consul.id_paciente = objeto.id_paciente is 0 ? Consul.id_paciente : objeto.id_paciente;
                Consul.fecha_consulta = objeto.fecha_consulta.ToString() is null ? Consul.fecha_consulta : objeto.fecha_consulta;
                Consul.diagnostico = objeto.diagnostico is null ? Consul.diagnostico : objeto.diagnostico;
                Consul.tratamiento = objeto.tratamiento is null ? Consul.tratamiento : objeto.tratamiento;
                Consul.estado = objeto.estado is null ? Consul.estado : objeto.estado;



                _dbcontext.Consultas.Update(Consul);
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
        public IActionResult Eliminar(int id_consulta)
        {

            Consulta oProducto = _dbcontext.Consultas.Find(id_consulta);

            if (oProducto == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {


                _dbcontext.Consultas.Remove(oProducto);
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
