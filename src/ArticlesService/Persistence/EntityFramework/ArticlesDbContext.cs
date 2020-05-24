using ArticlesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesService.Persistence.EntityFramework
{
    public class ArticlesDbContext : DbContext
    {
        public ArticlesDbContext(DbContextOptions<ArticlesDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleFavorite> ArticleFavorites { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureGuidPrimaryKeys();
            builder.SpecifyTablesNames();
            builder.SpecifyArticleColumnsMapping();
            builder.SpecifyArticleFavoriteColumnsMapping();
            builder.SpecifyCommentColumnsMapping();
            builder.ConfigureArticleFavoriteRelations();
            builder.ConfigureArticleCommentRelations();

            base.OnModelCreating(builder);
        }
    }
}