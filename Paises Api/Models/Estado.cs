using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paises_Api.Models
{
    public class Estado
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public string ImagemEstado { get; set; }
        public Pais Pais { get; set; }
    }
}
