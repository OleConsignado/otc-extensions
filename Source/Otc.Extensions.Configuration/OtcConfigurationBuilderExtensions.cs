using Otc.Extensions.Configuration;

namespace Microsoft.Extensions.Configuration
{
    public static class OtcConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adiciona um provedor de configuracao baseado em um objeto ao configuration builder.
        /// </summary>
        /// <typeparam name="T">Classe do objeto de configuracao.</typeparam>
        /// <param name="builder">Configuration Builder.</param>
        /// <param name="configurationObject">Objeto portador das configuracoes.</param>
        /// <returns>Proprio configuration builder com o provedor adicionado.</returns>
        public static IConfigurationBuilder AddObject<T>(this IConfigurationBuilder builder, T configurationObject)
            where T : class
        {
            return builder.Add(new ObjectConfigurationSource(configurationObject));
        }
    }
}