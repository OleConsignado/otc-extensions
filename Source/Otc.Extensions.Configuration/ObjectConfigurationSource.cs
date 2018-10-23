using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace Otc.Extensions.Configuration
{
    internal class ObjectConfigurationSource : JsonConfigurationSource
    {
        private readonly object configurationObject;

        public ObjectConfigurationSource(object configurationObject)
        {
            this.configurationObject = configurationObject ?? throw new ArgumentNullException(nameof(configurationObject));
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new ObjectConfigurationProvider(this, configurationObject);
        }
    }
}