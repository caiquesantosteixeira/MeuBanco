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
    public partial class frmSaque : Form
    {
        public frmSaque()
        {
            InitializeComponent();
            txtSaque.ToMonetario();
        }

        private void btnSacar_Click(object sender, EventArgs e)
        {
            ConfiguracaoDTO.GetInstance.ConfSync = new ConfSync();
            ConfiguracaoDTO.GetInstance.ConfSync.Host = "https://localhost:44395/api/";
            ConfiguracaoDTO.GetInstance.ConfSync.TypeAuthentication = ApiService.Util.TypeAuthentication.None;

            var cliente = MeuBancoService.GetCliente("05890299590");

            if (cliente.Saldo < txtSaque.Text.ToString().ToDecimal())
            {
                MessageBox.Show("Saldo insuficiente para saque.");
                return;
            }
            var Saque = new Saque();
            Saque.IdCliente = cliente.Id;
            Saque.Valor = txtSaque.Text.ToString().ToDecimal();

            var enviado = MeuBancoService.PostSaque(Saque);

            if (enviado)
            {
                MessageBox.Show("Saque efetuado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao efetuar depósito!");
            }
        }
    }
}
