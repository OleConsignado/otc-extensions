using Microsoft.Extensions.Configuration;
using Otc.ComponentModel.DataAnnotations;
using Otc.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Otc.Extensions.Configuration
{
    public static class OtcConfigurationExtensions
    {
        /// <summary>
        /// Le e valida configuracoes.
        /// <para>
        /// Verifica se existe uma secao do IConfiguration cuja o nome coincide com o nome do tipo T fornecido,
        /// caso a secao exista, le as propriedades da secao e vincula a uma nova instancia do tipo T;
        /// caso a secao nao existe, tentar ler as propriedades da raiz do IConfiguration e vincula a uma nova instancia do tipo T;
        /// </para>
        /// <para>
        /// Posteriormente a instancia T criada eh validada com base em Otc.ComponentModel.Annotation; caso a validacao
        /// falhe, uma excecao do tipo InvalidOperationException sera lancada.
        /// </para>
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser criado e devolvido.</typeparam>
        /// <param name="configuration">O object IConfiguration.</param>
        /// <exception cref="InvalidOperationException" />
        /// <returns>O objeto preenchido com os valores presentes nas configuracoes</returns>
        public static T SafeGet<T>(this IConfiguration configuration)
        {
            var typeName = typeof(T).Name;

            if (configuration.GetChildren().Any(item => item.Key == typeName))
            {
                configuration = configuration.GetSection(typeName);
            }          

            T model = configuration.Get<T>();

            if (model == null)
            {
                throw new InvalidOperationException(
                    $"Item de configuracao nao encontrado para o tipo {typeof(T).FullName}.");
            }

            if (!ModelValidator.TryValidate(model, out IEnumerable<ValidationResult> errors))
            {
                string message = $"A validacao de configuracao para o tipo '{model.GetType().FullName}' falhou: " +
                    string.Join(", ", errors.Select(r => $"\"{r.ErrorMessage}\""));

                throw new InvalidOperationException(message);
            }

            return model;
        }
    }
}
