using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Paises_Api.Data;
using Paises_Api.Models;

namespace Paises_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly PaisesContext _context;

        public EstadoController(PaisesContext context)
        {
            _context = context;
        }

        // GET: api/<EstadoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            var estados = new List<Estado>();
            estados = _context.Estados.FromSqlRaw("EXEC GetEstados").ToList();

            if (estados == null)
            {
                return NoContent();
            }

            return estados;

        }

        // GET api/<EstadoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstado(int id)
        {
            var estado = new List<Estado>();
            var estadoId = new SqlParameter("@EstadoId", id);

            estado = _context.Estados.FromSqlRaw("EXEC GetEstado @EstadoId", estadoId).ToList();

            if (estado == null)
            {
                return NoContent();
            }

            return estado;
        }

        // POST api/<EstadoController>
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            var nome = new SqlParameter("@Nome", estado.Nome);

            var affected = _context.Database.ExecuteSqlRaw("EXEC PostEstado @Nome", nome);

            if (affected > 0)
            {
                return Created("Amigo criado", estado);
            }
            else
            {
                throw new Exception();
            }
        }

        // PUT api/<EstadoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Estado>> UpdateEstado(int id, [FromBody] Estado estado)
        {
            if (id != estado.EstadoId)
            {
                return BadRequest();
            }

            var estadoId = new SqlParameter("@EstadoId", estado.EstadoId);
            var nome = new SqlParameter("@Nome", estado.Nome);

            var affected = _context.Database.ExecuteSqlRaw("EXEC UpdateEstado @EstadoId ,@Nome",
                estadoId, nome);

            if (affected > 0)
            {
                return Ok();
            }
            else
            {
                throw new Exception();
            }

        }

        // DELETE api/<EstadoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Estado>> Delete(int id)
        {
            var del = new SqlParameter("@EstadoId", id);
            _context.Database.ExecuteSqlRaw("EXEC DeleteEstado @EstadoId", del);

            return NoContent();
        }
    }
}
