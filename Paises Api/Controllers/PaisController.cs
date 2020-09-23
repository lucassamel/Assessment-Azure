using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Paises_Api.Data;
using Paises_Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Paises_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly PaisesContext _context;

        public PaisController(PaisesContext context)
        {
            _context = context;
        }

        // GET: api/<PaisController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            var paises = new List<Pais>();
            paises = _context.Paises.FromSqlRaw("EXEC GetPaises").ToList();

            if (paises == null)
            {
                return NoContent();
            }

            return paises;
        }

        // GET api/<PaisController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPais(int id)
        {
            var pais = new List<Pais>();
            var paisId = new SqlParameter("@PaisId", id);

            pais = _context.Paises.FromSqlRaw("EXEC GetPais @PaisId", paisId).ToList();

            if(pais == null)
            {
                return NoContent();
            }

            return pais;
        }

        // POST api/<PaisController>
        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais(Pais pais)
        {
            var nome = new SqlParameter("@Nome", pais.Nome);

            var affected = _context.Database.ExecuteSqlRaw("EXEC PostPais @Nome", nome);

            if (affected > 0)
            {
                return Created("Amigo criado", pais);
            }
            else
            {
                throw new Exception();
            }
        }

        // PUT api/<PaisController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Pais>> UpdatePais(int id, [FromBody] Pais pais)
        {
            if (id != pais.PaisId)
            {
                return BadRequest();
            }

            var paisId = new SqlParameter("@PaisId", pais.PaisId);
            var nome = new SqlParameter("@Nome", pais.Nome);

            var affected = _context.Database.ExecuteSqlRaw("EXEC UpdatePais @PaisId ,@Nome",
                paisId ,nome);

            if (affected > 0)
            {
                return Ok();
            }
            else
            {
                throw new Exception();
            }

        }

        // DELETE api/<PaisController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pais>> Delete(int id)
        {
            var del = new SqlParameter("@PaisId", id);
            _context.Database.ExecuteSqlRaw("EXEC DeletePais @PaisId", del);

            return NoContent();
        }
    }
}
