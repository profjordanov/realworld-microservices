using System.Collections.Generic;

namespace Articles.Api.Infrastructure.Options
{
    public class FailingOptions
    {
        public string ConfigPath = "/Failing";

        public List<string> EndpointPaths { get; set; } = new List<string>();

        public List<string> NotFilteredPaths { get; set; } = new List<string>();
    }
}