using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutheticationLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using TransporteTerresteCore.Data;
using TransporteTerresteCore.Data.Entities;
using TransporteTerresteCore.Models.DTOs;
using TransporteTerresteCore.Models.Responses;

namespace TransporteTerresteCore.Controllers
{
    [Route("api/v1/TransporteTerrestre")]
    [ApiController]
    public class TransporteTerrestreController : ControllerBase
    {
        private readonly ILogger<TransporteTerrestreController> Logger;
        private readonly ApplicationDbContext _db;
        public TransporteTerrestreController(ILogger<TransporteTerrestreController> logger, ApplicationDbContext context)
        {
            Logger = logger;
            _db = context;
        }
        [HttpPost]
        [Route("ConsultarTransporteTerrestre")]
        public IActionResult ConsultarTransporteTerrestre([FromBody] ConsultarTransporteTerrestre model)
        {
            try
            {
                DateTime dateTimeInicio;
                DateTime dateTimeFinal;
                if (!DateTime.TryParseExact(model.FechaInicio, "dd'/'MM'/'yyyy",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None,
                                          out dateTimeInicio))
                {
                    return BadRequest("Formato de fecha invalido, formato permitido dd/MM/aaaa");
                }
                if (!DateTime.TryParseExact(model.FechaFinal, "dd'/'MM'/'yyyy",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None,
                                          out dateTimeFinal))
                {
                    return BadRequest("Formato de fecha invalido, formato permitido dd/MM/aaaa");
                }
                ParametrosDTO parametros = new ParametrosDTO();
                Consulta3 consultaTransporteTerrestre = new Consulta3
                {
                    Origin = model.Origen,
                    Destination = model.Destino,
                    QuantityTravellers = model.CantidadPasajeros.ToString(),
                    Class = "Bar",
                    EndDate = model.FechaFinal,
                    StartDate = model.FechaInicio
                };
                parametros.parameters.transporteTerrestre.consulta = consultaTransporteTerrestre;
                ResponseConsultaTransporteTerrestre response = new ResponseConsultaTransporteTerrestre();
                List<ResponseBase> transportes = new List<ResponseBase>
                {
                    new ResponseBase
                    {
                        Origin = model.Origen,
                        Destination = model.Destino,
                        Stardate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(5),
                        TicketCode = "4456d81asd9",
                        Price = 2000000
                    },
                    new ResponseBase
                    {
                      Origin = model.Origen,
                        Destination = model.Destino,
                        Stardate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(5),
                        TicketCode = "4456d81asd9",
                        Price = 2000000
                    },
                    new ResponseBase
                    {
                     Origin = model.Origen,
                        Destination = model.Destino,
                        Stardate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(5),
                        TicketCode = "4456d81asd9",
                        Price = 2000000
                    },
                    new ResponseBase
                    {
                  Origin = model.Origen,
                        Destination = model.Destino,
                        Stardate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(5),
                        TicketCode = "4456d81asd9",
                        Price = 2000000
                    }
                };
                response.transportes = transportes;
                return Ok(response);
            }
            catch (Exception ex)
            {
                Logger.LogError("Excepcion generada en ConsultarVuelos: " + ex.Message);
                return StatusCode(500, "Ocurrio un error");
                throw;
            }
        }
        [HttpPost]
        [Route("ReservaTransporteTerrestre")]
        public IActionResult ReservaTransporteTerrestre([FromBody] ReservaDTO model, [FromHeader] string Token)
        {
            try
            {
                Logger.LogInformation("INICIA PROCESO DE RESERVA DE TRANSPORTE");
                JwtProvider jwt = new JwtProvider("TouresBalon.com", "UsuariosPlataforma");
                var accessToken = Request.Headers[HeaderNames.Authorization];
                var first = accessToken.FirstOrDefault();
                if (string.IsNullOrEmpty(accessToken) || !first.Contains("Bearer"))
                {
                    return BadRequest();
                }
                string token = first.Replace("Bearer", "").Trim();
                Logger.LogInformation("INICIA PROCESO DE VALIDACION DE TOKEN :" + token);
                var a = jwt.ValidateToken(token);
                if (!a)
                {
                    return Unauthorized();
                }
                ParametrosDTO parametros = new ParametrosDTO();
                Reserva3 reserva = new Reserva3
                {
                    TicketCode = model.CodigoTransporte,
                    LastName = model.Apellido,
                    Name = model.Nombre
                };
                parametros.parameters.transporteTerrestre.reserva = reserva;

                _db.ReservaTransporteTerrestres.Add(new ReservaTransporteTerrestre
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Token = token,
                    CodigoTransporte = model.CodigoTransporte
                });
                _db.SaveChanges();
                return Ok(new ResponseReservaTransporteTerrestre
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                Logger.LogError("Excepcion generada en ReservaHotel: " + ex.Message);
                return StatusCode(500, "Ocurrio un error");
                throw;
            }
        }
        [HttpGet]
        [Route("Healty")]
        public IActionResult Healty()
        {
            return Ok("Todo Bien");
        }
    }
}
