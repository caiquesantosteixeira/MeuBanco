using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiService.Util
{

    public class TupleList<T1, T2> : List<Tuple<T1, T2>>
    {
        public void Add(T1 item, T2 item2)
        {
            Add(new Tuple<T1, T2>(item, item2));
        }
    }

    internal class Endpoint
    {
        public Endpoint(Type type, TypeMetodo metodo, string url)
        {
            Metodo = metodo;
            Type = type;
            Url = url;
        }
        public TypeMetodo Metodo { get; set; }
        public Type Type { get; set; }
        public string Url { get; private set; }
    }

    /// <summary>
    ///  Classe reponsável pelas regras de obtenção do endereço para comunicação com a API
    /// </summary>
    public static class Endereco
    {
        private static List<Endpoint> _endpoint;

        static Endereco()
        {
            _endpoint = new List<Endpoint>();
        }

        public static void AdicionarEndpoint(Type type, TupleList<TypeMetodo[], string> endpoints)
        {
            foreach (var url in endpoints)
                foreach (var metodo in url.Item1)
                    _endpoint.Add(new Endpoint(type, metodo, url.Item2));
        }

        public static void InicializarEndereco()
        {
            _endpoint = new List<Endpoint>();
        }


        /// <summary>
        /// Obtém o Endpoint
        /// </summary>
        /// <param name="type"></param>
        /// <param name="metodo"></param>
        /// <returns></returns>
        private static string ObterEndpoint(this Type type, TypeMetodo metodo)
        {
            var query = from ep in _endpoint
                        where ep.Metodo == metodo &&
                              ep.Type == type
                        select ep.Url;
            var listaRetorno = query as IList<string> ?? query.ToList();
            var qtdeRetorno = listaRetorno.Count();
            if (qtdeRetorno == 0)
                throw new Exception(string.Format("Não foi possível obter Endpoint {0}, para a Entidade {1}", metodo.Descricao(), type.Name));
            if (qtdeRetorno > 1)
                throw new Exception("A função ObterEndpoint obteve mais de um resultado!");
            return listaRetorno.FirstOrDefault();
        }


        /// <summary>
        ///  Obtém a URL para uso no ApiService da API
        /// </summary>
        /// <returns></returns>
        public static string ObterEndpointCompleto(this Type type, TypeMetodo metodo, string parametroSubstituicao = "")
        {
            const string barra = "/";

            var url = ObterEndpoint(type, metodo);

            if (!url.StartsWith(barra))
                url = barra + url;

            //if (!url.EndsWith(barra))
            //    url += barra;

            if (!string.IsNullOrWhiteSpace(parametroSubstituicao))
            {
                url = url.ToLower().Replace("parametrosubstituicao", parametroSubstituicao);
            }
            return url;
        }
    }
}
