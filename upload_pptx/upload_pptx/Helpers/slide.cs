using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upload_pptx.Helpers
{
    public class slide
    {
        [Key]
        public int SlideNumber { get; set; }
        public string name { get;set; }
        public string Yol { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }
    }
}
