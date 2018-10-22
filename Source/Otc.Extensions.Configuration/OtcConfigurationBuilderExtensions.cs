using Otc.Extensions.Configuration;

namespace Microsoft.Extensions.Configuration
{
    public static class OtcConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adiciona um provedor de configuração baseado em um objeto ao configuration builder.
        /// </summary>
        /// <typeparam name="T">Classe do objeto de configuração.</typeparam>
        /// <param name="builder">Configuration Builder.</param>
        /// <param name="configurationObject">Objeto portador das configurações.</param>
        /// <returns>Próprio configuration builder com o provedor adicionado.</returns>
        public static IConfigurationBuilder AddJsonObject<T>(this IConfigurationBuilder builder, T configurationObject)
            where T : class
        {
            return builder.Add(new JsonObjectConfigurationSource(configurationObject));
        }
    }
}