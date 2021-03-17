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
    public partial class frmExtrato : Form
    {
        public Cliente _cliente;

        public List<Deposito> Depositos;

        public List<Saque> Saques;

        public List<Transferencia> Transferencias;


        public List<Transferencia> TransferenciasRecebidas;
        public frmExtrato(Cliente cliente)
        {
            _cliente = cliente;
            InitializeComponent();
            Depositos = MeuBancoService.GetDepositos(cliente.Id);
            Saques = MeuBancoService.GetSaques(cliente.Id);
            Transferencias = MeuBancoService.GetTransferencias(cliente.Id);
            TransferenciasRecebidas = MeuBancoService.GetTransferenciasRecebidas(cliente.Id);
            PreencherGridSaques();
            PreencherGridSTransf();
            PreencherGridDep();
        }


        public void PreencherGridSaques()
        {
            gridSaques.Rows.Clear();
            foreach (var item in Saques)
            {
                var row = gridSaques.Rows.Add();
                gridSaques[0, row].Value = item.Data;
                gridSaques[1, row].Value = item.Valor;
            }
        }

        public void PreencherGridSTransf()
        {
            grisTransferencias.Rows.Clear();
            foreach (var item in Transferencias)
            {
                var row = grisTransferencias.Rows.Add();
                grisTransferencias[0, row].Value = item.Data;
                grisTransferencias[1, row].Value = item.Valor;
            }
        }

        public void PreencherGridDep()
        {
            gridDepositos.Rows.Clear();
            foreach (var item in Depositos)
            {
                var row = gridDepositos.Rows.Add();
                gridDepositos[0, row].Value = item.Data;
                gridDepositos[1, row].Value = item.Valor;
            }
        }

        public void PreencherGridRecebidas()
        {
            gridRecebidas.Rows.Clear();
            foreach (var item in TransferenciasRecebidas)
            {
                var row = gridRecebidas.Rows.Add();
                gridRecebidas[0, row].Value = item.Data;
                gridRecebidas[1, row].Value = item.Valor;
            }
        }
    }
}
