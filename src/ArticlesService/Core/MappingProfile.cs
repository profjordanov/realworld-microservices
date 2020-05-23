using System;
using ArticlesService.Domain.Entities;
using ArticlesService.Protos;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace ArticlesService.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PublishArticle, Article>(MemberList.None)
                .ForMember(
                    dest => dest.Slug,
                    opts => opts.MapFrom(
                        src => MakeSlug(src.Title)))
                .ForMember(dest => dest.CreatedAtUtc,
                    opts => opts.MapFrom(
                        _ => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.UpdatedAtUtc,
                    opts => opts.MapFrom(
                        _ => DateTimeOffset.UtcNow));

            CreateMap<Article, ArticleView>(MemberList.Source)
                .ForMember(
                    dest => dest.Id, 
                    opts => opts.MapFrom(
                        src => src.Id.ToString()))
                .ForMember(
                    dest => dest.CreatedAtUtc,
                    opts => opts.MapFrom(
                        src => src.CreatedAtUtc.ToTimestamp()))
                .ForMember(
                    dest => dest.UpdatedAtUtc,
                    opts => opts.MapFrom(
                        src => src.UpdatedAtUtc.HasValue ? src.UpdatedAtUtc.Value.ToTimestamp() : null));

        }

        private static string MakeSlug(string title) =>
            title.ToLower().Replace(' ', '-');
    }
}