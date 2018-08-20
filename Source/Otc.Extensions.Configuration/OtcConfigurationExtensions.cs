using Microsoft.Extensions.Configuration;
using Otc.Validations;
using System;
using System.Collections.Generic;
using Otc.ComponentModel.DataAnnotations;
using System.Linq;

namespace Otc.Extensions.Configuration
{
    public static class OtcConfigurationExtensions
    {
        /// <summary>
        /// Obtem uma secao de configuracao em um objeto do tipo T e realiza uma validacao baseada 
        /// em atributos de System.ComponentModel.DataAnnotations. Caso a validacao falhe, uma <see cref="InvalidOperationException"/>
        /// sera lancada.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser criado e devolvido.</typeparam>
        /// <param name="configuration">A secao de configuracao a ser lida.</param>
        /// <exception cref="InvalidOperationException">Caso o objeto nao seja aceito pela validacao baseada 
        /// em atributos de System.ComponentModel.DataAnnotations</exception>
        /// <returns>O objeto preenchido com os valores presentes nas configuracoes</returns>
        public static T SafeGet<T>(this IConfiguration configuration)
        {
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
