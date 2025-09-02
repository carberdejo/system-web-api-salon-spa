using Microsoft.AspNetCore.Mvc;
using ProySpaHelena.Models;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProySpaHelena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private string cad_cn = "";

        public ReportesController(IConfiguration icfg)
        {
            cad_cn = icfg.GetConnectionString("cn1")!;
        }


        [HttpGet("ReporteServicios")]
        public IActionResult ReporteServicios()
        {
            var lista = new List<Reporte_Servicios>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(cad_cn, "Reporte_Servicios"))
            {
                while (reader.Read())
                {
                    lista.Add(new Reporte_Servicios()
                    {
                        id = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        CantidadVariantes = reader.GetInt32(2),
                        PrecioMinimo = reader.GetDecimal(3),
                        PrecioMaximo = reader.GetDecimal(4)
                    });
                }
            }

            return Ok(lista);

        }



        // GET: api/<ReportesController>
        [HttpGet("ReporteTrabajadores")]
        public IActionResult ReporteTrabajadores()
        {
            var lista = new List<Reporte_Trabajadores>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(cad_cn, "Reporte_Trabajadores"))
            {
                while (reader.Read())
                {
                    lista.Add(new Reporte_Trabajadores()
                    {
                        id = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        dni = reader.GetInt32(2),
                        telefono = reader.GetString(3),
                        HorasTotales = reader.GetInt32(4),
                        DiasaTrabajar = reader.GetInt32(5)

                    });
                }
            }

            return Ok(lista);

        }

        // GET api/<ReportesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReportesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
