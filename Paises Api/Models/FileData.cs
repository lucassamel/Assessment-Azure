using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Paises_Api.Models
{
    public class FileData
    {
        [Key]
        public int FileDataId { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string ModifiedOn { get; set; }

        
    }

}
