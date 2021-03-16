using ApiService;
using ApiService.Util;
using MeuBanco.Servvices;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MeuBanco;
using MonitorCompre.models;
using JGourmet.UTIL;

namespace MeuBanco
{
    public partial class frmLogin : Form
    {
        private ConfiguracaoGeral config { get; set; }

        public frmLogin()
        {
            InitializeComponent();
            var versao = new Version(System.Windows.Forms.Application.ProductVersion);
            lblVersao.Text = "Versão " + versao;
        }

        private void btnLog_Click(object sender, EventArgs e)
        {

            if (config == null)
            {
                config = new ConfiguracaoGeral();
                var diretorio = Application.StartupPath + "\\Arquivos\\";
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);
            }

            config.Host = "https://localhost:44395/api/";
            config.TypeAuthentication = ApiService.Util.TypeAuthentication.BearerToken;
            config.TokenFixed = true;
            config.EndPointGetToken = "v1/Usuarios/Logar-Form";

            if (txtUsuario.Text?.Trim() == "" || txtSenha.Text?.Trim() == "")
            {
                MessageBox.Show("Usuário ou Senha Inválidos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

  
                config.Body = new Body
                {
                    Usuario = txtUsuario.Text?.Trim().RemoverPontos(),
                    Senha = txtSenha.Text.Trim()
                };

                lblLoader.Text = "Aguarde...";
                var resp = MeuBancoService.GetToken(config);
                if (resp.Sucesso)
                {
                    lblLoader.Text = "";
                    lblLoader.Refresh();

                var usuario = resp.Data.Data.UserToken.Usuario;
                var cliente = MeuBancoService.GetCliente(usuario);
      
                    using (var frmPrincipal = new frmPrincipal(cliente))
                    {
                        frmPrincipal.ShowDialog();
                    }
                    txtUsuario.Text = "";
                    txtSenha.Text = "";
                }
                else
                {
                    lblLoader.Refresh();
                    lblLoader.Text = resp.Mensagem;
                    lblLoader.ForeColor = Color.Red;
                }
            }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            ConfiguracaoDTO.GetInstance.ConfSync = new ConfSync();
            ConfiguracaoDTO.GetInstance.ConfSync.Host = "https://localhost:44395/api/";
            ConfiguracaoDTO.GetInstance.ConfSync.TypeAuthentication = ApiService.Util.TypeAuthentication.None;
            ConfiguracaoDTO.GetInstance.ConfSync.TokenFixed = false;

            using (var frmPrincipal = new frmAdicionarCliente())
            {
                frmPrincipal.ShowDialog();
            }
        }
    }
}
