﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amigos_Api.Data;
using Amigos_Api.Models;
using Amigos_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Amigos_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigoController : ControllerBase
    {
        private readonly AmigosContext _context;
        //private readonly ImagemService _ImageService;

        public AmigoController(AmigosContext context
           /* ImagemService imagemService*/)
        {
            _context = context;
            //_ImageService = imagemService;
        }

        // GET: api/<AmigoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigos()
        {
            var amigos = new List<Amigo>();
            amigos = _context.Amigos.FromSqlRaw("EXEC GetAmigos").ToList();

            if(amigos == null)
            {
                return NoContent();
            }

            return amigos;
        }

        // GET api/<AmigoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigo(int id)
        {
            var amigoList = new List<Amigo>();
            var amigoId = new SqlParameter("@AmigoId", id);

            amigoList = _context.Amigos.FromSqlRaw("EXEC GetAmigo @AmigoId", amigoId).ToList();

            if(amigoList == null)
            {
                return NoContent();
            }          


            return amigoList;
        }

        // POST api/<AmigoController>
        [HttpPost]
        public async Task<ActionResult<Amigo>> PostAmigo(Amigo amigo)
        {
            //var amigoId = new Amigo();
            var nome = new SqlParameter("@Nome", amigo.Nome);
            //var nome = amigo.Nome;
            var sobrenome = new SqlParameter("@Sobrenome", amigo.Sobrenome);
            var email = new SqlParameter("Email",amigo.Email);
            var telefone = new SqlParameter("@Telefone", amigo.Telefone);
            var aniversario = new SqlParameter("@Aniversario", amigo.Aniversario);
            var paisOrigem = new SqlParameter("@PaisOrigem", amigo.PaisOrigem);
            var estadoOrigem = new SqlParameter("EstadoOrigem", amigo.EstadoOrigem);

            var affected = _context.Database.ExecuteSqlRaw("EXEC PostAmigo @Nome, @Sobrenome, @Email, @Telefone, @Aniversario, " +
                "@PaisOrigem, @EstadoOrigem",
                nome, sobrenome, email, telefone, aniversario, paisOrigem, estadoOrigem);

            if (affected > 0)
            {
                return Created("Amigo criado", amigo);
            } else
            {
                throw new Exception();
            }
            
        }

        // PUT api/<AmigoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Amigo>>  UpdateAmigo(int id, [FromBody] Amigo amigo)
        {
            if(id != amigo.AmigoId)
            {
                return BadRequest();
            }

            var amigoId = new SqlParameter("@AmigoId", amigo.AmigoId);
            var nome = new SqlParameter("@Nome", amigo.Nome);
            var sobrenome = new SqlParameter("@Sobrenome", amigo.Sobrenome);
            var email = new SqlParameter("@Email", amigo.Email);
            var telefone = new SqlParameter("@Telefone", amigo.Telefone);
            var aniversario = new SqlParameter("@Aniversario", amigo.Aniversario);
            var pais = new SqlParameter("@PaisOrigem", amigo.PaisOrigem);
            var estado = new SqlParameter("@EstadoOrigem", amigo.EstadoOrigem);

            var affected = _context.Database.ExecuteSqlRaw("EXEC UpdateAmigo @AmigoId, @Nome, @Sobrenome," +
                " @Email, @Telefone, @Aniversario, " +
                "@PaisOrigem, @EstadoOrigem",
                amigoId, nome, sobrenome, email, telefone, aniversario, pais, estado);

            if (affected > 0)
            {
                return Ok();
            }
            else
            {
                throw new Exception();
            }           
        }

        // DELETE api/<AmigoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amigo>> Delete(int id)
        {
            var del = new SqlParameter("@AmigoId", id);
            _context.Database.ExecuteSqlRaw("EXEC DeleteAmigo @AmigoId", del);

            return NoContent();
        }

        //IMAGEM
        [HttpPut("{id}/imagem")]
        public async Task<IActionResult> ImagemAmigo(int id, string file)
        {
            var imagem = new Uri("https://lucassamelsocialnetwork.blob.core.windows.net/imagens/" + 
                file).ToString();

            var amigoId = new SqlParameter("@AmigoId", id);
            var imagemAmigo = new SqlParameter("@ImagemAmigo", imagem);


            var affected = _context.Database.ExecuteSqlRaw("EXEC ImagemAmigo @AmigoId, @ImagemAmigo", amigoId, imagemAmigo);

            if (affected > 0)
            {
                return Ok();
            }
            else
            {
                throw new Exception();
            }
        }

    }
}
