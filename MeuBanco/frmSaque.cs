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
        private Cliente _cliente;
        public frmSaque(Cliente cliente)
        {
            _cliente = cliente;
            InitializeComponent();
            txtSaque.ToMonetario();
        }

        private void btnSacar_Click(object sender, EventArgs e)
        {
            if (_cliente.Saldo < txtSaque.Text.ToString().ToDecimal())
            {
                MessageBox.Show("Saldo insuficiente para saque.");
                return;
            }
            var Saque = new Saque();
            Saque.IdCliente = _cliente.Id;
            Saque.Valor = txtSaque.Text.ToString().ToDecimal();

            var enviado = MeuBancoService.PostSaque(Saque);

            if (enviado)
            {
                _cliente.Saldo -= Saque.Valor;
                MeuBancoService.PutCliente(_cliente);

                MessageBox.Show("Saque efetuado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao efetuar depósito!");
            }
            Close();
        }
    }
}
