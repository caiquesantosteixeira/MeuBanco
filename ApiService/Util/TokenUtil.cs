using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ApiService.Util
{
    public class TokenUtil
    {
        public static bool IsTokenValido(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            TokenOption tokenOption = new TokenOption();
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
            {
                return false;
            }
            JwtSecurityToken parsedJwt = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var exp = parsedJwt.Payload.Exp ?? 0;
            var segundos = DateTimeOffset.Now.ToUnixTimeSeconds();
            return exp > segundos;
        }

        public static RetornoApi<R> GetToken<R>(ConfSync confSync)
        {
            try
            {
                HttpClient client;

                if (string.IsNullOrWhiteSpace(confSync.Username) || string.IsNullOrWhiteSpace(confSync.Username))
                {
                    client = new HttpClient
                    {
                        BaseAddress = new Uri(confSync.Host),
                    };
                }
                else
                {
                    var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{confSync.Username}:{confSync.Password}")));
                    client = new HttpClient
                    {
                        BaseAddress = new Uri(confSync.Host),
                        DefaultRequestHeaders = { Authorization = authValue }
                    };
                }


                client.DefaultRequestHeaders.Accept.Clear();

                var body = new Dictionary<string, string>();

                if (!string.IsNullOrWhiteSpace(confSync.Body.ClientId))
                {
                    body.Add("client_id", confSync.Body.ClientId);
                }
                if (!string.IsNullOrWhiteSpace(confSync.Body.ClientSecret))
                {
                    body.Add("client_secret", confSync.Body.ClientSecret);
                }

                body.Add("grant_type", confSync.Body.GrantType);

                if (!string.IsNullOrWhiteSpace(confSync.Body.Username))
                {
                    body.Add("username", confSync.Body.Username);
                }
                if (!string.IsNullOrWhiteSpace(confSync.Body.Password))
                {
                    body.Add("password", confSync.Body.Password);
                }

                if (!string.IsNullOrWhiteSpace(confSync.Body.Usuario))
                {
                    body.Add("Usuario", confSync.Body.Usuario);
                }
                if (!string.IsNullOrWhiteSpace(confSync.Body.Senha))
                {
                    body.Add("Senha", confSync.Body.Senha);
                }

                var response = client.PostAsync(confSync.EndPointGetToken, new FormUrlEncodedContent(body))?.Result;         
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var retorno = JsonConvert.DeserializeObject<R>(result);
                    return new RetornoApi<R>
                    {
                        Sucesso = true,
                        StatusCode = 200,
                        Data = retorno
                    };
                   
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new RetornoApi<R>
                    {
                        Sucesso = false,
                        StatusCode = 401,
                        Mensagem = "Login ou senha incorretos."
                    };
                }else if(response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return new RetornoApi<R>
                    {
                        Sucesso = false,
                        StatusCode = 403,
                        Mensagem = "Não Autorizado."
                    };
                }else if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new RetornoApi<R>
                    {
                        Sucesso = false,
                        StatusCode = 404,
                        Mensagem = "Url não encontrada."
                    };
                }else if(response.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    return new RetornoApi<R>
                    {
                        Sucesso = false,
                        StatusCode = 405,
                        Mensagem = "Método não encontrado/Parâmetros inválidos."
                    };
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    ApiLog.Log.FazLog(Mensagem: $"Não catalogado! {result}");                   

                    return new RetornoApi<R>
                    {
                        Sucesso = false,
                        StatusCode = 0,
                        Mensagem = "Erro não catalogado. Consulte o log para saber mais."
                    };
                }
            }
            catch (Exception ex)
            {
                ApiLog.Log.FazLog(Mensagem: "GetToken " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"), Exception: ex);
                return new RetornoApi<R>
                {
                    Sucesso = false,
                    StatusCode = 500,
                    Mensagem = "Ocorreu um erro de servidor. Consulte o log para saber mais."
                };
            }
        }

        public static Token GetToken(ConfSync confSync, string json = "")
        {
            Token token = null;
            try
            {
                HttpClient client;


                if (string.IsNullOrWhiteSpace(confSync.Username) || string.IsNullOrWhiteSpace(confSync.Username))
                {
                    client = new HttpClient
                    {
                        BaseAddress = new Uri(confSync.Host),
                    };
                }
                else
                {
                    var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{confSync.Username}:{confSync.Password}")));
                    client = new HttpClient
                    {
                        BaseAddress = new Uri(confSync.Host),
                        DefaultRequestHeaders = { Authorization = authValue }
                    };
                }


                client.DefaultRequestHeaders.Accept.Clear();

                var body = new Dictionary<string, string>();

                if (!string.IsNullOrWhiteSpace(confSync.Body.ClientId))
                {
                    body.Add("client_id", confSync.Body.ClientId);
                }
                if (!string.IsNullOrWhiteSpace(confSync.Body.ClientSecret))
                {
                    body.Add("client_secret", confSync.Body.ClientSecret);
                }

                body.Add("grant_type", confSync.Body.GrantType);

                if (!string.IsNullOrWhiteSpace(confSync.Body.Username))
                {
                    body.Add("username", confSync.Body.Username);
                }
                if (!string.IsNullOrWhiteSpace(confSync.Body.Password))
                {
                    body.Add("password", confSync.Body.Password);
                }

                if (!string.IsNullOrWhiteSpace(confSync.Body.Usuario))
                {
                    body.Add("Usuario", confSync.Body.Usuario);
                }
                if (!string.IsNullOrWhiteSpace(confSync.Body.Senha))
                {
                    body.Add("Senha", confSync.Body.Senha);
                }
                var response = new HttpResponseMessage();
                if (string.IsNullOrEmpty(json))
                {
                    response = client.PostAsync(confSync.EndPointGetToken, new FormUrlEncodedContent(body))?.Result;
                }
                else
                {
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = client.PostAsync(confSync.EndPointGetToken, stringContent).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrEmpty(json))
                    {
                        token = JsonConvert.DeserializeObject<Token>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        var ret = JsonConvert.DeserializeObject<RetornoLogin>(response.Content.ReadAsStringAsync().Result);
                        token = new Token();
                        token.AccessToken = ret.Data.AccesToken;
                        token.ExpiresIn = (int)ret.Data.ExpiresIn;
                        token.TokenType = ret.Data.TokenType;
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    throw new InvalidOperationException($"Usuário e senha invalido para sincronização\n " + result, new Exception());
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    throw new InvalidOperationException(result);
                }
            }
            catch (Exception ex)
            {
                token = null;
                ApiLog.Log.FazLog(Mensagem: "GetToken " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"));
            }
            return token;
        }


        //public static R GetToken<R>(string json, ConfSync confSync)
        //{
        //    Token token = null;
        //    R obj = default;
        //    try
        //    {
        //        HttpClient client;


        //        if (string.IsNullOrWhiteSpace(confSync.Username) || string.IsNullOrWhiteSpace(confSync.Username))
        //        {
        //            client = new HttpClient
        //            {
        //                BaseAddress = new Uri(confSync.Host),
        //            };
        //        }
        //        else
        //        {
        //            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{confSync.Username}:{confSync.Password}")));
        //            client = new HttpClient
        //            {
        //                BaseAddress = new Uri(confSync.Host),
        //                DefaultRequestHeaders = { Authorization = authValue }
        //            };
        //        }


        //        client.DefaultRequestHeaders.Accept.Clear();

        //        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = client.PostAsync(confSync.EndPointGetToken, stringContent).Result;

        //        var result = response.Content.ReadAsStringAsync().Result;

        //        var isSuccessStatusCode = response.IsSuccessStatusCode;

        //        if (isSuccessStatusCode)
        //        {
        //            obj = JsonConvert.DeserializeObject<R>(result);
        //        }
        //        else
        //        {
        //            ApiLog.Log.FazLog(
        //                Mensagem: $"REQUEST [{confSync.EndPointGetToken}] :{json}",
        //                Versao: ConfiguracaoDTO.GetInstance.Version);

        //            ApiLog.Log.FazLog(
        //            Mensagem: $"RESPONSE :STATUSCODE: {(int)response.StatusCode} | {result}",
        //            Versao: ConfiguracaoDTO.GetInstance.Version);
        //        }

        //        //var response = client.PostAsync(confSync.EndPointGetToken, new FormUrlEncodedContent(body))?.Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            token = JsonConvert.DeserializeObject<Token>(response.Content.ReadAsStringAsync().Result);
        //        }
        //        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        //        {
        //            result = response.Content.ReadAsStringAsync().Result;
        //            throw new InvalidOperationException($"Usuário e senha invalido para sincronização\n " + result, new Exception());
        //        }
        //        else
        //        {
        //            result = response.Content.ReadAsStringAsync().Result;
        //            throw new InvalidOperationException(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        token = null;
        //        ApiLog.Log.FazLog(Mensagem: "GetToken " + ex.StackTrace + (ex.Message ?? "Message NULL ") + (ex.InnerException?.Message ?? "InnerException NULL"));
        //    }
        //    return obj;
        //}
    }
}
