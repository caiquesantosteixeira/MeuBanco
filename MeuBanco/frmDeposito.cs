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
        private Cliente _cliente;
        public frmDeposito(Cliente cliente)
        {
            _cliente = cliente;
            InitializeComponent();
            txtSaque.ToMonetario();
        }

        private void btnDepositar_Click(object sender, EventArgs e)
        {
            
            var deposito = new Deposito();
            deposito.IdCliente = _cliente.Id;
            deposito.Valor = txtSaque.Text.ToString().ToDecimal();

            var enviado = MeuBancoService.PostDeposito(deposito);

            if (enviado)
            {
                _cliente = MeuBancoService.GetCliente(_cliente.Cpf);
                _cliente.Saldo += deposito.Valor;
                MeuBancoService.PutCliente(_cliente);
                MessageBox.Show("Deposito efetuado com sucesso!");
            }
            else 
            {
                MessageBox.Show("Erro ao efetuar depósito!");
            }
            Close();
        }
    }
}
