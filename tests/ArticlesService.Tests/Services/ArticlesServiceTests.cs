using System;
using System.Threading.Tasks;
using ArticlesService.Core;
using ArticlesService.Domain.Repositories;
using ArticlesService.Persistence.Repositories;
using ArticlesService.Protos;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace ArticlesService.Tests.Services
{
    public class ArticlesServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IArticlesRepository _repository;

        private readonly ArticlesService.Services.ArticlesService _service;

        public ArticlesServiceTests()
        {
            var profile = new MappingProfile();
            var mapperConfiguration = new MapperConfiguration(expression => expression.AddProfile(profile));
            _mapper = new Mapper(mapperConfiguration);

            _repository = new ArticlesRepository(DbContextProvider.GetInMemory());

            _service = new ArticlesService.Services.ArticlesService(_mapper, _repository);
        }

        [Fact]
        public async Task Publish_Works_Properly()
        {
            // Arrange
            var publishCmd = new PublishArticle
            {
                AuthorId = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            };

            // Act
            var result = await _service.Publish(publishCmd, null);

            // Assert
            result.Should().NotBeNull();
        }
    }
}