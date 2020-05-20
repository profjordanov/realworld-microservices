using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TagsService.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public HashSet<ArticleTag> ArticleTags { get; set; } = new HashSet<ArticleTag>();
    }
}