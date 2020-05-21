using TagsService.Protos;

namespace TagsClient.gRPCClient
{
    public interface IServiceClientFactory
    {
        TagService.TagServiceClient Create();
    }
}