using ApiService;
using ApiService.Util;
using MeuBanco.Models;
using MonitorCompre.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (retorno != null && retorno.Sucesso)
            {
                return true;
            }
            else
            {
                MessageBox.Show(retorno != null ? retorno.Mensagem:"Erro");
                return false;
            }

        }

        public static Usuario PostUsuario(Usuario usuario)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Usuario, RetornoApi<Usuario>>(usuario, ref ret, $"v1/Usuarios");
            if (retorno.Sucesso)
            {
                return retorno.Data;
            }
            else
            {
                MessageBox.Show(retorno != null ? retorno.Mensagem : "Erro");
                return null;
            }

        }

        public static bool PutCliente(Cliente cliente)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.PUT<Cliente, RetornoApi<Cliente>>(cliente, ref ret, $"v1/Clientes");
            if (retorno != null && retorno.Sucesso)
            {
                return true;
            }
            else
            {
                MessageBox.Show(retorno != null ? retorno.Mensagem : "Erro");
                return false;
            }

        }

        public static bool PostDeposito(Deposito deposito)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Deposito, RetornoApi<Deposito>>(deposito, ref ret, $"v1/Deposito");
            if (retorno != null && retorno.Sucesso)
            {
                return true;
            }
            else {
                MessageBox.Show(retorno != null ? retorno.Mensagem : "Erro");
                return false;
            }
            
        }

        public static bool PostSaque(Saque saque)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Saque, RetornoApi<Deposito>>(saque, ref ret, $"v1/Saque");
            if (retorno != null && retorno.Sucesso)
            {
                return true;
            }
            else
            {
                MessageBox.Show(retorno != null ? retorno.Mensagem : "Erro");
                return false;
            }
        }

        public static bool PostTransferencia(Transferencia transferencia)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.POST<Transferencia, RetornoApi<Transferencia>>(transferencia, ref ret, $"v1/transferencia");
            if (retorno != null && retorno.Sucesso)
            {
                return true;
            }
            else
            {
                MessageBox.Show(retorno != null ? retorno.Mensagem : "Erro");
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

        public static List<Deposito> GetDepositos(int idCliente)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.GETALL<RetornoApi<List<Deposito>>>($"v1/Deposito?idCliente={idCliente}");
            if (retorno != null )
            {
                return retorno.Data;
            }
            else
            {
                return null;
            }
        }

        public static List<Saque> GetSaques(int idCliente)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.GETALL<RetornoApi<List<Saque>>>($"v1/Saque?idCliente={idCliente}");
            if (retorno != null )
            {
                return  retorno.Data; ;
            }
            else
            {
                return null;
            }
        }

        public static List<Transferencia> GetTransferencias(int idCliente)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.GETALL<RetornoApi<List<Transferencia>>>($"v1/Transferencia?idCliente={idCliente}");
            if (retorno != null)
            {
                return  retorno.Data; ;
            }
            else
            {
                return null;
            }
        }

        public static List<Transferencia> GetTransferenciasRecebidas(int idCliente)
        {
            bool ret = false;
            var retorno = ApiService.Http.Metodo.GETALL<RetornoApi<List<Transferencia>>>($"v1/Transferencia/GetAllRecebidas?idCliente={idCliente}");
            if (retorno != null)
            {
                return retorno.Data; ;
            }
            else
            {
                return null;
            }
        }
    }
}
