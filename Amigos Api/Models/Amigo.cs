using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Amigos_Api.Models
{
    public class Amigo
    {
        [Key]
        public int AmigoId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Aniversario { get; set; }
        public List<Amigo> Amigos { get; set; }
        public string PaisOrigem { get; set; }
        public string EstadoOrigem { get; set; }
    }
}
