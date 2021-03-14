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
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnSaque_Click(object sender, EventArgs e)
        {
            using (var frmSaque = new frmSaque()) {
                frmSaque.ShowDialog();
            }
        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {
            using (var frmDeposito = new frmDeposito())
            {
                frmDeposito.ShowDialog();
            }
        }

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            using (var frmTransferencia = new frmTransferencia())
            {
                frmTransferencia.ShowDialog();
            }
        }
    }
}
