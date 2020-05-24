using System;

namespace ProfilesService.Domain.Aggregates
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Bio { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}