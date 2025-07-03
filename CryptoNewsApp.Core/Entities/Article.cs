using CryptoNewsApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Core.Entities
{
    public class Article : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ArticleStatus Status { get; set; } = ArticleStatus.Published;
        public string ImageUrl { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; } = new List<ArticleCategory>();
    }
}
