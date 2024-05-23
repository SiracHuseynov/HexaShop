using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Core.Models
{
    public class Product : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
