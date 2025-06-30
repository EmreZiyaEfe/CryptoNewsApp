using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Core.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string SourceUrl { get; set; }
        
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublished { get; set; } = true;

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
