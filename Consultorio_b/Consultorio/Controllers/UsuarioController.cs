using Consultorio.Data;
using Consultorio.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace Consultorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly ConsultorioDBContext _dbcontext;

        public UsuarioController(ConsultorioDBContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                lista = _dbcontext.Usuarios.ToList();
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
            List<Usuario> lista = new List<Usuario>();

            try
            {
                lista = _dbcontext.Usuarios.Include(i => i.Perfil).ToList();
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
        public IActionResult Guardar([FromBody] Usuario objeto)
        {


            try
            {
                // Genera una contraseña aleatoria
                string contrasenaAleatoria = GenerarContrasenaAleatoria(8); // Cambia la longitud según tus necesidades

                // Asigna la contraseña al objeto Usuario
                objeto.contrasena = contrasenaAleatoria;

                // Guarda el objeto Usuario en la base de datos
                _dbcontext.Usuarios.Add(objeto);
                _dbcontext.SaveChanges();

                // Envía la contraseña al correo del usuario (aquí debes implementar la lógica para enviar correos electrónicos)
                // Envía la contraseña al correo del usuario
                EnviarCorreo(objeto.email, "Nueva contraseña", $"Su nueva contraseña es: {contrasenaAleatoria}");

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario creado exitosamente" });

            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException;
                if (innerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = innerException.Message });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error desconocido al guardar en la base de datos" });
            }
            catch (Exception ex)
            {

                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });
            }




        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Usuario objeto)
        {
            Usuario user = _dbcontext.Usuarios.Find(objeto.id_usuario);

            if (user == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {
                user.id_usuario = objeto.id_usuario is 0 ? user.id_usuario : objeto.id_usuario;
                user.nombre_usuario = objeto.nombre_usuario is null ? user.nombre_usuario : objeto.nombre_usuario;
                user.email = objeto.email is null ? user.email : objeto.email;
                user.contrasena = objeto.contrasena is null ? user.contrasena : objeto.contrasena;
                user.id_perfil = objeto.id_perfil is 0 ? user.id_perfil : objeto.id_perfil;
                user.estado = objeto.estado is null ? user.estado : objeto.estado;

                _dbcontext.Usuarios.Update(user);
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
        public IActionResult Eliminar(int id_usuario)
        {

            Usuario user = _dbcontext.Usuarios.Find(id_usuario);

            if (user == null)
            {
                return BadRequest("Producto no encontrado");

            }

            try
            {

                _dbcontext.Usuarios.Remove(user);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar cambios", detalle = innerException.Message });
            }


        }



        private string GenerarContrasenaAleatoria(int longitud)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var contrasena = new string(Enumerable.Repeat(caracteres, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return contrasena;
        }

        private void EnviarCorreo(string destino, string asunto, string mensaje)
        {
            // Configura los detalles del servidor SMTP de Outlook
            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587, // Puerto SMTP para Outlook
                Credentials = new NetworkCredential("jordyfelipe2@outlook.com", "jordy2017"), // Cambia a tus credenciales
                EnableSsl = true, // Habilita SSL para Outlook
            };

            // Configura el correo electrónico
            var mailMessage = new MailMessage
            {
                From = new MailAddress("jordyfelipe2@outlook.com"), // Cambia a tu dirección de correo
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(destino);

            // Envía el correo
            smtpClient.Send(mailMessage);
        }



    }
}
