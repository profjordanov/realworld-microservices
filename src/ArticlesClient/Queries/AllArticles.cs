using System.Collections.Generic;
using ArticlesClient.Views;
using MediatR;

namespace ArticlesClient.Queries
{
    public class AllArticles : IRequest<IEnumerable<ArticleProjection>>
    {
        
    }
}