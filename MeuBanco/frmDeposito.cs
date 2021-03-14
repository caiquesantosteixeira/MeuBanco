using ApiService;
using JGourmet.UTIL;
using MeuBanco.Models;
using MeuBanco.Servvices;
using MonitorCompre.models;
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
    public partial class frmDeposito : Form
    {
        public frmDeposito()
        {
            InitializeComponent();
            txtSaque.ToMonetario();
        }

        private void btnDepositar_Click(object sender, EventArgs e)
        {
            ConfiguracaoDTO.GetInstance.ConfSync = new ConfSync();
            ConfiguracaoDTO.GetInstance.ConfSync.Host = "https://localhost:44395/api/";
            ConfiguracaoDTO.GetInstance.ConfSync.TypeAuthentication = ApiService.Util.TypeAuthentication.None;
   
            var deposito = new Deposito();
            deposito.IdCliente = 1;
            deposito.Valor = txtSaque.Text.ToString().ToDecimal();

            var enviado = MeuBancoService.PostDeposito(deposito);

            if (enviado)
            {
                MessageBox.Show("Deposito efetuado com sucesso!");
            }
            else 
            {
                MessageBox.Show("Erro ao efetuar depósito!");
            }
        }
    }
}
