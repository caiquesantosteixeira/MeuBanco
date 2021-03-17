using MeuBanco.Models;
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
    public partial class frmPrincipal : Form
    {
        private Cliente _cliente;
        public frmPrincipal(Cliente cliente)
        {
            _cliente = cliente;
            InitializeComponent();
        }

        private void btnSaque_Click(object sender, EventArgs e)
        {
            using (var frmSaque = new frmSaque(_cliente)) {
                frmSaque.ShowDialog();
            }
        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {
            using (var frmDeposito = new frmDeposito(_cliente))
            {
                frmDeposito.ShowDialog();
            }
        }

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            using (var frmTransferencia = new frmTransferencia(_cliente))
            {
                frmTransferencia.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var frmExtrato = new frmExtrato(_cliente))
            {
                frmExtrato.ShowDialog();
            }
        }
    }
}
