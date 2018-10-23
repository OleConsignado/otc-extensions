using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Otc.Extensions.Configuration
{
    internal class ObjectConfigurationProvider : JsonConfigurationProvider
    {
        private readonly object configurationObject;

        public ObjectConfigurationProvider(JsonConfigurationSource source, object configurationObject) : base(source)
        {
            this.configurationObject = configurationObject ?? throw new ArgumentNullException(nameof(configurationObject));
        }

        public override void Load()
        {
            var serializedObject = JsonConvert.SerializeObject(configurationObject);
            var byteArray = Encoding.ASCII.GetBytes(serializedObject);
            var memoryStream = new MemoryStream(byteArray);

            Load(memoryStream);
        }
    }
}