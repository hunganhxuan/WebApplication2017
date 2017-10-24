using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication7.Models;

namespace WebApplication7.ViewModels
{
    public class ArticlesViewModel
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public byte CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}