using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Persistence.Repositories
{
    public class ProfilesRepository
    {
        private static readonly List<KeyValuePair<Guid, Profile>> KeyValuePairs = new List<KeyValuePair<Guid, Profile>>
        {
            new KeyValuePair<Guid, Profile>(Guid.Parse("408F8D79-7A20-4B08-8866-5E4CA1CE5864"), new Profile
            {
                Id = Guid.Parse("408F8D79-7A20-4B08-8866-5E4CA1CE5864")
            })
        };

        private readonly ConcurrentDictionary<Guid, Profile> _dictionary =
            new ConcurrentDictionary<Guid, Profile>(KeyValuePairs);

        public IEnumerable<Profile> All => _dictionary.Values;

        public Profile GetByIdOrDefault(Guid profileId)
        {
            var hasValue = _dictionary.TryGetValue(profileId, out var profile);
            return hasValue ? profile : default;
        }

        public Profile AddOrDefault(Profile profile)
        {
            var added = _dictionary.TryAdd(profile.Id, profile);
            return added ? profile : default;
        }

        public Profile UpdateOrDefault(Profile profile)
        {
            var hasValue = _dictionary.TryGetValue(profile.Id, out _);

            if (!hasValue)
            {
                return default;
            }

            _dictionary[profile.Id] = profile;
            return profile;
        }
    }
}