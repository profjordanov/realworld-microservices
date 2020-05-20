using System;
using ArticlesService.Domain.Entities;
using ArticlesService.Protos;
using AutoMapper;

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

            CreateMap<Article, ArticleView>(MemberList.None)
                .ForMember(
                    dest => dest.Id, 
                    opts => opts.MapFrom(
                        src => src.Id.ToString()));

        }

        private static string MakeSlug(string title) =>
            title.ToLower().Replace(' ', '-');
    }
}