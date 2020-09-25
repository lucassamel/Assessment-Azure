using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Paises_Api.Data;
using Paises_Api.Models;

namespace Paises_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemPaisController : ControllerBase
    {
        private readonly PaisesContext _context;
        private readonly IConfiguration _configuration;

        public ImagemPaisController(PaisesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Create(int id, IFormFile files)
        {
            //var account = await _userManager.GetUserAsync(this.User);
            // var perfilLogado = await _context.Amigos
            // .FirstAsync(p => p.Usuario.Email == account.Email);

            string dir = id + "/";
            string systemFileName = dir + files.FileName;
            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");


            // Retrieve storage account from connection string.
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);

            // Create the blob client.
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("imagens");

            container.GetDirectoryReference(dir);

            // This also does not make a service call; it only creates a local object.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
            await using (var data = files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }

            var imagem = new Uri("https://lucassamelsocialnetwork.blob.core.windows.net/imagens/" +
                systemFileName).ToString();

            var paisId = new SqlParameter("@PaisId", id);
            var imagemPais = new SqlParameter("@ImagemPais", imagem);


            var affected = _context.Database.ExecuteSqlRaw("EXEC ImagemPais @PaisId, @ImagemPais", paisId, imagemPais);

            if (affected > 0)
            {
                return Ok();
            }
            else
            {
                throw new Exception();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllBlobs(int id)
        {
            //var account = await _userManager.GetUserAsync(this.User);
            //var perfilLogado = await _context.Perfis
            //    .FirstAsync(p => p.Usuario.Email == account.Email);

            string dir = id.ToString() + "/";


            string blobstorageconnection =
           _configuration.GetValue<string>("blobstorage");
            CloudStorageAccount cloudStorageAccount =
           CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container =
           blobClient.GetContainerReference("imagens");
            CloudBlobDirectory dirb =
           container.GetDirectoryReference(dir);
            BlobResultSegment resultSegment = await
           container.ListBlobsSegmentedAsync(string.Empty,
            true, BlobListingDetails.Metadata, 100, null, null, null);

            List<FileData> fileList = new List<FileData>();
            foreach (var blobItem in resultSegment.Results)
            {
                // A flat listing operation returns only blobs, not virtual directories.

                var blob = (CloudBlob)blobItem;
                fileList.Add(new FileData()
                {
                    FileName = blob.Name,
                    FileSize = Math.Round((blob.Properties.Length / 1024f) / 1024f,
               2).ToString(),
                    ModifiedOn =
               DateTime.Parse(blob.Properties.LastModified.ToString()).ToLocalTime().ToString(
               )
                });
            }
            return Ok();
        }


    }
}
