using ApiService;
using ApiService.Util;
using MeuBanco.Servvices;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MeuBanco;
using MonitorCompre.models;

namespace MonitorCompre
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

            config.Host = "https://localhost:44395/api/v1/";
            config.TypeAuthentication = ApiService.Util.TypeAuthentication.None;
            config.TokenFixed = false;
            //config.EndPointGetToken = "v1/Usuarios/Logar-Form";
            bool erros = false;

            if (txtUsuario.Text?.Trim() == "" || txtSenha.Text?.Trim() == "")
            {
                erros = true;
                MessageBox.Show("Usuário ou Senha Inválidos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

  
                config.Body = new Body
                {
                    Usuario = txtUsuario.Text?.Trim(),
                    Senha = CriptografiaRijndael.Encryptar(txtSenha.Text?.Trim())
                };

                lblLoader.Text = "Aguarde...";
                var resp = MeuBancoService.GetToken(config);
                if (resp.Sucesso)
                {
                    lblLoader.Text = "";
                    lblLoader.Refresh();


                    // salva novas config
      
                    using (var frmPrincipal = new frmPrincipal())
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
        }
}
