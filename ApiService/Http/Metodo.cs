using ApiService.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiService.Http
{
    public class Metodo
    {
        public static T GET<T>(string url, string id = "", int versao = 0)
        {
            try
            {
                T obj = default(T);
                var client = InstanciarHttpClient(versao);
                var response = client.GetAsync(url + id).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(result);
                }
                else
                {
                    ApiLog.Log.FazLog(
                       Mensagem: $"URL [{url}] Response: [{result}]",
                       Versao: ConfiguracaoDTO.GetInstance.Version);
                }
                return obj;
            }
            catch (Exception ex)
            {
                ApiLog.Log.FazLog(
              Mensagem: "GET BY ID " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"),
                    Versao: ConfiguracaoDTO.GetInstance.Version,
                    Exception: ex);

                Console.Write(ex.Message);
            }
            return default(T);
        }

        public static T GETALL<T>(string url, int versao = 0)
        {
            T obj = default;
            try
            {

                var client = InstanciarHttpClient(versao);
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(result);
                }
                //else
                //{
                //    ApiLog.Log.FazLog(
                //        Mensagem: $"URL [{url}] Response: [{result}]",
                //        Versao: ConfiguracaoDTO.GetInstance.Version);
                //}
            }
            catch (Exception ex)
            {
                ApiLog.Log.FazLog(
                     Mensagem: "GET ALL " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"),
                     Versao: ConfiguracaoDTO.GetInstance.Version,
                     Exception: ex);

                Console.Write(ex.Message);
            }
            return obj;
        }

        public static HttpStatusCode DELETE(string url, string id, ref string result, int versao = 0)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(id))
                //{
                //throw new Exception("Obrigatório informar o id para métodos do tipo DELETE.");
                //}
                var client = InstanciarHttpClient(versao);
                var response = client.DeleteAsync(url + id).Result;
                result = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    ApiLog.Log.FazLog(
                             Mensagem: (url + id) + " " + result,
                            Versao: ConfiguracaoDTO.GetInstance.Version);
                }
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                ApiLog.Log.FazLog(
                 Mensagem: "DELETE " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"),
                Versao: ConfiguracaoDTO.GetInstance.Version,
                Exception: ex);

                throw new InvalidOperationException($"Falha na Operação", ex);
            }
        }

        public static T POST<T>(T _objeto, ref bool isSuccessStatusCode, string _url, int versao = 0)
            => Send<T, T>(_objeto, ref isSuccessStatusCode, _url, false, versao: versao);

        public static T PUT<T>(T _objeto, ref bool isSuccessStatusCode, string _url, string id = "", int versao = 0)
            => Send<T, T>(_objeto, ref isSuccessStatusCode, _url, true, id, versao);

        public static R POST<T, R>(T _objeto, ref bool isSuccessStatusCode, string _url, int versao = 0)
            => Send<T, R>(_objeto, ref isSuccessStatusCode, _url, false, versao: versao);

        public static R PUT<T, R>(T _objeto, ref bool isSuccessStatusCode, string _url, string id = "")
            => Send<T, R>(_objeto, ref isSuccessStatusCode, _url, true, id);

        private static R Send<T, R>(T _objeto, ref bool isSuccessStatusCode, string _url, bool _put, string id = "", int versao = 0)
        {
            try
            {
                R obj = default;
                var client = InstanciarHttpClient(versao);
                var json = JsonConvert.SerializeObject(_objeto);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = _put
                   ? client.PutAsync(_url + id, stringContent).Result
                   : client.PostAsync(_url, stringContent).Result;

                var result = response.Content.ReadAsStringAsync().Result;
                isSuccessStatusCode = response.IsSuccessStatusCode;

                if (isSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<R>(result);
                }
                else
                {
                    obj = JsonConvert.DeserializeObject<R>(result);
                    ApiLog.Log.FazLog(
                        Mensagem: $"REQUEST [{_url}] :{json}",
                        Versao: ConfiguracaoDTO.GetInstance.Version);

                    ApiLog.Log.FazLog(
                    Mensagem: $"RESPONSE :STATUSCODE: {(int)response.StatusCode} | {result}",
                    Versao: ConfiguracaoDTO.GetInstance.Version);
                }

                return obj;
            }
            catch (Exception ex)
            {
                if (!isSuccessStatusCode)
                {
                    ApiLog.Log.FazLog(Mensagem: "POST OR PUT " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"),
                    Versao: ConfiguracaoDTO.GetInstance.Version,
                    Exception: ex);
                }
                return default;
            }
        }

        public static async Task PATCH<T>(T _objeto, string _url, int versao)
        {
            try
            {
                var client = InstanciarHttpClient(versao);
                var json = JsonConvert.SerializeObject(_objeto);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PatchAsync(_url, stringContent).Result;
                var result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                ApiLog.Log.FazLog(Mensagem: "PATCH " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"),
                    Versao: ConfiguracaoDTO.GetInstance.Version,
                    Exception: ex);
            }
        }

        private static HttpClient InstanciarHttpClient(int versaoHost = 0)
        {
            if (string.IsNullOrWhiteSpace(ConfiguracaoDTO.GetInstance.ConfSync.Host))
                throw new InvalidOperationException("Host Inválido");
            HttpClient client;
            if (ConfiguracaoDTO.GetInstance.ConfSync.TypeAuthentication == Util.TypeAuthentication.None)
                client = ClientHelper.GetClient();
            else if (ConfiguracaoDTO.GetInstance.ConfSync.TypeAuthentication == Util.TypeAuthentication.BasicAuth)
            {
                client = ClientHelper.GetClient(ConfiguracaoDTO.GetInstance.ConfSync.Username, ConfiguracaoDTO.GetInstance.ConfSync.Password);
            }
            else
            {

                if (!ConfiguracaoDTO.GetInstance.ConfSync.TokenFixed)
                {
                    if (string.IsNullOrWhiteSpace(ConfiguracaoDTO.GetInstance.ConfSync.EndPointGetToken))
                        throw new InvalidOperationException("End Point Get Token Não Definido");

                    if (!TokenUtil.IsTokenValido(ConfiguracaoDTO.GetInstance.ConfSync.Token))
                    {
                        var result = TokenUtil.GetToken<Token>(ConfiguracaoDTO.GetInstance.ConfSync);
                        if (!result.Sucesso)
                            throw new InvalidOperationException("Falha na Recuperação do Token");

                        if (result.Data.Data != null && result.Data.Data.AccesToken != null)
                        {
                            ConfiguracaoDTO.GetInstance.ConfSync.Token = result.Data.Data.AccesToken;
                        }
                        else 
                        {
                            ConfiguracaoDTO.GetInstance.ConfSync.Token = result.Data?.AccessToken;
                        }
                        
                    }
                }

                client = ClientHelper.GetClient(ConfiguracaoDTO.GetInstance.ConfSync.Token, ConfiguracaoDTO.GetInstance.ConfSync.TypeAuthentication);
            }
            client.Timeout = new TimeSpan(1, 0, 0);

            if (versaoHost == 1)
            {
                client.BaseAddress = new Uri(ConfiguracaoDTO.GetInstance.ConfSync.HostV1);
            }
            else if (versaoHost == 2)
            {
                client.BaseAddress = new Uri(ConfiguracaoDTO.GetInstance.ConfSync.HostV2);
            }
            else if (versaoHost == 3)
            {
                client.BaseAddress = new Uri(ConfiguracaoDTO.GetInstance.ConfSync.HostV3);
            }
            else
            {
                client.BaseAddress = new Uri(ConfiguracaoDTO.GetInstance.ConfSync.Host);
            }

            ClientHelper.AddVersionHeaders(client);
            return client;
        }
    }

    public static class ClientHelper
    {
        public static void AddVersionHeaders(HttpClient client)
        {
            if (ConfiguracaoDTO.GetInstance.ConfSync.Headers != null)
            {
                foreach (var header in ConfiguracaoDTO.GetInstance.ConfSync.Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Name, header.Value);
                }
            }
        }

        // Basic auth
        public static HttpClient GetClient(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidOperationException($"Username Inválido");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException($"Password Inválido");
            }
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            return client;
        }

        // Auth with bearer token
        public static HttpClient GetClient(string token, Util.TypeAuthentication type)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException($"Token Inválido");
            }
            var authValue = new AuthenticationHeaderValue(type.Descricao(), token);
            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
                //Set some other client defaults like timeout / BaseAddress
            };

            return client;
        }

        // Auth with none authentication
        public static HttpClient GetClient()
        {
            var client = new HttpClient();
            return client;
        }
    }

    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string _url, StringContent iContent)
        {
            var requestUri = new Uri(client.BaseAddress + _url);
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };
            return await client.SendAsync(request);
        }
    }
}
