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
            if (txtSenha.Text.Trim() != txtConfirmarSenha.Text.Trim())
            {
                MessageBox.Show("Senha e confirmação de senha divergem.");
                return;
            }

            if (string.IsNullOrEmpty(txtConfirmarSenha.Text) || string.IsNullOrEmpty(txtSenha.Text) || string.IsNullOrEmpty(txtCpf.Text.Trim().RemoverPontos()) || string.IsNullOrEmpty(txtSenha.Text.Trim())) {
                MessageBox.Show("Dados inválidos");
                return;
            }

            if (txtCpf.Text.Trim().RemoverPontos().Length != 11) {
                MessageBox.Show("cpf inválido");
                return;
            }

            var usuario = new Usuario { 
                UserName = txtCpf.Text.Trim().RemoverPontos(),
                Senha = txtSenha.Text.Trim(),
                Email = ""
            };

            var usuarioRetorno = MeuBancoService.PostUsuario(usuario);
            var cliente = new Cliente();
            if (usuarioRetorno != null)
            {
                cliente.Nome = txtNome.Text;
                cliente.Cpf = txtCpf.Text.RemoverPontos();
                cliente.Saldo = 0m;
                cliente.Senha = txtSenha.Text.Trim();
                cliente.IdUsuario = usuarioRetorno.Id;
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
