using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Application.Dtos
{
    public class NewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public SourceInfo Source { get; set; }
        public string? Category { get; set; } // opsiyonel
    }
}
