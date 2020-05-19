namespace TagsService.Domain.Entities
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}