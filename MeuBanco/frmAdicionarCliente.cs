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
    public partial class frmAdicionarCliente : Form
    {
        public frmAdicionarCliente()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente();

            cliente.Nome = txtNome.Text;
            cliente.Cpf = txtCpf.Text.RemoverPontos();
            cliente.Saldo = 0m;
            cliente.Senha = txtSenha.Text.Trim();

            if (txtSenha.Text.Trim() != txtConfirmarSenha.Text.Trim()) {
                MessageBox.Show("Senha e confirmação de senha divergem.");
            }

            if (validarCliente(cliente))
            {
                if (MeuBancoService.PostCliente(cliente))
                {
                    MessageBox.Show("Cliente Cadastrado com sucesso");
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar cliente");
                }
            }
            else {
                MessageBox.Show("Cliente Inválido.,");
            }
            Close();
        }
        public bool validarCliente(Cliente cliente) 
        {
            if (string.IsNullOrEmpty(cliente.Nome)) 
            {
                return false;
            }

            if (string.IsNullOrEmpty(cliente.Cpf))
            {
                return false;
            }

            if (string.IsNullOrEmpty(cliente.Senha))
            {
                return false;
            }

            return true;
        }
    }
}
