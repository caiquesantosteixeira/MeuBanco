using ApiService.Util;
using System;
using System.Collections.Generic;

namespace ApiService
{
    public class ConfiguracaoDTO
    {
        public ConfSync ConfSync { get; set; }
        public string Version { get; set; }


        private static ConfiguracaoDTO instance;
        public static ConfiguracaoDTO GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfiguracaoDTO();
                }
                return instance;
            }
        }
    }

    [Serializable]

    public class ConfSync
    {
        public ConfSync()
        {
            TypeAuthentication = TypeAuthentication.BasicAuth;
            Headers = new List<Header>();
        }
        public string Host { get; set; }
        public string HostV1 { get; set; }
        public string HostV2 { get; set; }
        public string HostV3 { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string EndPointGetToken { get; set; }
        public bool TokenFixed { get; set; }
        public TypeAuthentication TypeAuthentication { get; set; }
        public Body Body { get; set; }

        [field: NonSerialized]
        public List<Header> Headers { get; set; } 
    }

    public class Body
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Usuario { get; set; }
        public string Senha { get; set; }
    }

    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
