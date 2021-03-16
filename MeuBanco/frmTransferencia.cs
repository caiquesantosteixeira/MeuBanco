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
        public Cliente _cliente;
        public Cliente _clienteDestinatario;
        public frmTransferencia(Cliente cliente)
        {
            _cliente = cliente;
            InitializeComponent();
            txtValor.ToMonetario();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _clienteDestinatario = MeuBancoService.GetCliente(txtCpf.Text.RemoverPontos());

            if (_clienteDestinatario != null)
            {
                lblClienteDest.Text = "Cliente Destinatário: " + _clienteDestinatario.Nome ;
                lblClienteDest.Refresh();
            }
            else {
                MessageBox.Show("Cliente inexistente");
            }
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            if (_clienteDestinatario == null) {
                MessageBox.Show("Cliente destinatário não adicionado.");
                return;
            }
            if (_cliente.Saldo < txtValor.Text.ToString().ToDecimal())
            {
                MessageBox.Show("Saldo insuficiente para transferência.");
                return;
            }

            var transferencia = new Transferencia();
            transferencia.IdClienteRemetente = _cliente.Id;
            transferencia.IdClienteDestinatario = _clienteDestinatario.Id;
            transferencia.Valor = txtValor.Text.ToString().ToDecimal();

            var enviado = MeuBancoService.PostTransferencia(transferencia);

            if (enviado)
            {
                _cliente.Saldo -= transferencia.Valor;
                MeuBancoService.PutCliente(_cliente);

                _clienteDestinatario.Saldo += transferencia.Valor;
                MeuBancoService.PutCliente(_clienteDestinatario);

                MessageBox.Show("Transferencia efetuada com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao efetuar Transferencia!");
            }
            Close();
        }
    }
}
