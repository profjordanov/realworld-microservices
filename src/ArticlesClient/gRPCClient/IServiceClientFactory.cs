using ArticlesService.Protos;

namespace ArticlesClient.gRPCClient
{
    public interface IServiceClientFactory
    {
        ArticleService.ArticleServiceClient Create();
    }
}