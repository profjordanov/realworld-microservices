using ArticlesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesService.Persistence.EntityFramework
{
    public static class OnModelCreatingConfiguration
    {
        internal static void ConfigureGuidPrimaryKeys(this ModelBuilder builder)
        {
            builder
                .Entity<Article>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Entity<Comment>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Entity<ArticleFavorite>(typeBuilder =>
                {
                    typeBuilder.HasKey(af => new { af.ArticleId, af.UserId });
                });

            builder.Entity<ArticleFavorite>(b =>
            {
                b.HasKey(af => new { af.ArticleId, af.UserId });

                b.HasOne(af => af.Article)
                    .WithMany(a => a.Favorites)
                    .HasForeignKey(af => af.ArticleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        internal static void SpecifyTablesNames(this ModelBuilder builder)
        {
            builder.Entity<Article>().ToTable("articles");
            builder.Entity<Comment>().ToTable("comments");
            builder.Entity<ArticleFavorite>().ToTable("article_favorites");
        }

        internal static void SpecifyArticleColumnsMapping(this ModelBuilder builder)
        {
            builder
                .Entity<Article>()
                .Property(a => a.Id)
                .HasColumnName("id");

            builder
                .Entity<Article>()
                .Property(a => a.Slug)
                .HasColumnName("slug");

            builder
                .Entity<Article>()
                .Property(a => a.Title)
                .HasColumnName("title");
            
            builder
                .Entity<Article>()
                .Property(a => a.Description)
                .HasColumnName("description");

            builder
                .Entity<Article>()
                .Property(a => a.AuthorId)
                .HasColumnName("author_id");

            builder
                .Entity<Article>()
                .Property(a => a.CreatedAtUtc)
                .HasColumnName("created_at_utc");

            builder
                .Entity<Article>()
                .Property(a => a.UpdatedAtUtc)
                .HasColumnName("updated_at_utc");

            builder
                .Entity<Article>()
                .Property(a => a.FavoritesCount)
                .HasColumnName("updated_at_utc");

            builder
                .Entity<Article>()
                .Ignore(article => article.FavoritesCount);
        }

        internal static void SpecifyArticleFavoriteColumnsMapping(this ModelBuilder builder)
        {
            builder
                .Entity<ArticleFavorite>()
                .Property(af => af.ArticleId)
                .HasColumnName("article_id");

            builder
                .Entity<ArticleFavorite>()
                .Property(af => af.UserId)
                .HasColumnName("user_id");
        }

        internal static void SpecifyCommentColumnsMapping(this ModelBuilder builder)
        {
            builder
                .Entity<Comment>()
                .Property(c => c.Id)
                .HasColumnName("id");

            builder
                .Entity<Comment>()
                .Property(c => c.Body)
                .HasColumnName("body");

            builder
                .Entity<Comment>()
                .Property(c => c.ArticleId)
                .HasColumnName("article_id");

            builder
                .Entity<Comment>()
                .Property(c => c.AuthorId)
                .HasColumnName("author_id");

            builder
                .Entity<Comment>()
                .Property(c => c.CreatedAtUtc)
                .HasColumnName("created_at_utc");

            builder
                .Entity<Comment>()
                .Property(c => c.UpdatedAtUtc)
                .HasColumnName("updated_at_utc");
        }

        internal static void ConfigureArticleFavoriteRelations(this ModelBuilder builder)
        {
            builder
                .Entity<ArticleFavorite>()
                .HasOne(af => af.Article)
                .WithMany(article => article.Favorites)
                .HasForeignKey(af => af.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        internal static void ConfigureArticleCommentRelations(this ModelBuilder builder)
        {
            builder
                .Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(article => article.Comments)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}