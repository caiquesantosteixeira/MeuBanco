using JGourmet.UTIL;
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
        public frmTransferencia()
        {
            InitializeComponent();
            txtValor.ToMonetario();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCpf.Text.RemoverPontos();
        }
    }
}
