using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using GestorCuentasTarjetas.WebAPI.Models;

namespace GestorCuentasTarjetas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly DbContexto _dbContexto;

        public TarjetaController(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Tarjeta> lista = new List<Tarjeta>();

            try
            {
                lista = _dbContexto.TarjetasCredito.ToList();

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }


        [HttpGet("ObtenerEstadoCuentaDetallado/{tarjetaCreditoId}")]
        public IActionResult ObtenerEstadoCuentaDetallado(int tarjetaCreditoId)
        {
            try
            {
                using (var connection = _dbContexto.Database.GetDbConnection())
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("@TarjetaCreditoId", tarjetaCreditoId, DbType.Int32, ParameterDirection.Input);

                    using (var result = connection.QueryMultiple("Sp_ObtenerEstadoCuentaDetallado", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var tarjeta = result.Read<Tarjeta>().SingleOrDefault();
                        var transacciones = result.Read<EstadoCuentaDetallado>().ToList();

                        return Ok(new { Tarjeta = tarjeta, Transacciones = transacciones });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener el estado de cuenta: {ex.Message}" });
            }
        
        }
        [HttpGet("BuscarPorNombre/{nombreTitular}")]
        public IActionResult BuscarTarjetasPorNombre(string nombreTitular)
        {
            try
            {
                using (var connection = _dbContexto.Database.GetDbConnection())
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("@NombreTitular", nombreTitular, DbType.String, ParameterDirection.Input);

                    var tarjetas = connection.Query<Tarjeta>("Sp_BuscarTarjetasPorNombre", parameters, commandType: CommandType.StoredProcedure);

                    return Ok(tarjetas);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al buscar tarjetas por nombre: {ex.Message}" });
            }
        }


    }
}

