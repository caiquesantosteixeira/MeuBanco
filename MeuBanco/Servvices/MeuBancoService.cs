using ApiService;
using ApiService.Util;
using MeuBanco.Models;
using MonitorCompre.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuBanco.Servvices
{
    public class MeuBancoService
    {

        public static RetornoApi<RetornoLogin> GetToken(ConfiguracaoGeral config)
        {

            var result = TokenUtil.GetToken<RetornoLogin>(config);
            if (result.Sucesso)
            {
                config.Token = result.Data.Data.AccesToken;
                ConfiguracaoDTO.GetInstance.ConfSync = config;
                if (!TokenUtil.IsTokenValido(config.Token))
                {
                    result.Sucesso = false;
                    result.StatusCode = 401;
                    result.Mensagem = "Token Inválido.";
                }
            }
            return result;

        }

        public static bool PostCliente(Cliente cliente)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Cliente, RetornoApi<Cliente>>(cliente, ref ret, $"v1/Clientes");
            if (retorno != null && retorno.Data != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool PostDeposito(Deposito deposito)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Deposito, RetornoApi<Deposito>>(deposito, ref ret, $"v1/Deposito");
            if (retorno != null && retorno.Data != null)
            {
                return true;
            }
            else {
                return false;
            }
            
        }

        public static bool PostSaque(Saque saque)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Saque, RetornoApi<Deposito>>(saque, ref ret, $"v1/Saque");
            if (retorno != null && retorno.Data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool PostTransferencia(Transferencia transferencia)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Transferencia, RetornoApi<Transferencia>>(transferencia, ref ret, $"v1/transferencia");
            if (retorno != null && retorno.Data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Cliente GetCliente(string cpf)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.GET<RetornoApi<Cliente>>($"v1/Clientes/CpfCnpj/{cpf}");
            if (retorno != null && retorno.Data != null)
            {
                return retorno.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
