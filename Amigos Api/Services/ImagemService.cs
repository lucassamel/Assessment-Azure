using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amigos_Api.Services
{
    public interface ImagemService
    {
        public Task<string> ImagemPerfil(IFormFile files, int id);
    }
}
