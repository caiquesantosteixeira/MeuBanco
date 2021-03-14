using ApiService;
using JGourmet.UTIL;
using MeuBanco.Models;
using MeuBanco.Servvices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeuBanco
{
    public partial class frmTransferencia : Form
    {
        public frmTransferencia()
        {
            InitializeComponent();
            txtValor.ToMonetario();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCpf.Text.RemoverPontos();
            var cliente = MeuBancoService.GetCliente("05890299590");
            if (cliente != null)
            {
                if (cliente.Saldo < txtValor.Text.ToString().ToDecimal())
                {
                    MessageBox.Show("Saldo insuficiente para transferência.");
                    return;
                }

                ConfiguracaoDTO.GetInstance.ConfSync = new ConfSync();
                ConfiguracaoDTO.GetInstance.ConfSync.Host = "https://localhost:44395/api/";
                ConfiguracaoDTO.GetInstance.ConfSync.TypeAuthentication = ApiService.Util.TypeAuthentication.None;

                var transferencia = new Transferencia();
                transferencia.IdClienteRemetente = cliente.Id;
                transferencia.IdClienteDestinatario = 1;
                transferencia.Valor = txtValor.Text.ToString().ToDecimal();

                var enviado = MeuBancoService.PostTransferencia(transferencia);

                if (enviado)
                {
                    MessageBox.Show("Transferencia efetuada com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao efetuar Transferencia!");
                }
            }
            else {
                MessageBox.Show("Cliente inexistente");
            }
        }
    }
}
