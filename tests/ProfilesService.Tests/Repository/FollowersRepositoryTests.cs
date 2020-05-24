using System;
using System.Threading.Tasks;
using FluentAssertions;
using Marten;
using ProfilesService.Domain.Aggregates;
using ProfilesService.Persistence.Repositories;
using Xunit;

namespace ProfilesService.Tests.Repository
{
    public class FollowersRepositoryTests
    {
        private readonly IDocumentSession _documentSession;
        private readonly FollowersRepository _repository;

        public FollowersRepositoryTests()
        {
            var factory = new EventStoreFactory();
            factory.EnsureEventStoreIsCreated();
            _documentSession = factory.DocumentSession();
            _repository = new FollowersRepository(_documentSession);
        }

        [Fact]
        public async Task PersistNewAsync_Works_Properly()
        {
            // Arrange
            var followerId = Guid.NewGuid().ToString();
            var followingId = Guid.NewGuid().ToString();

            var follower = new Follower
            {
                FollowerId = followerId,
                FollowingId = followingId
            };

            // Act
            await _repository.PersistNewAsync(follower);

            // Assert
            var result = _repository.GetAsync(followerId, followingId);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Remove_Works_Properly()
        {
            // Arrange
            var followerId = Guid.NewGuid().ToString();
            var followingId = Guid.NewGuid().ToString();

            var follower = new Follower
            {
                FollowerId = followerId,
                FollowingId = followingId
            };

            await _repository.PersistNewAsync(follower);
            var aggregate = await _repository.GetAsync(followerId, followingId);

            // Act
            _repository.Remove(aggregate);

            // Assert
            var result = await _repository.GetAsync(followerId, followingId);
            result.Should().BeNull();
        }
    }
}