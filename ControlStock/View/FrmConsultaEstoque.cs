using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;

namespace View
{
    public partial class FrmConsultaEstoque : Form
    {
        public FrmConsultaEstoque()
        {
            InitializeComponent();
            cbxFiltro.SelectedItem = "Todas";
            Carregar();
        }

        void Carregar()
        {
            ControllerMovimentacao controllerMovimentacao = new ControllerMovimentacao();
            DataTable dt = controllerMovimentacao.Lista(txtPesquisa.Text, cbxFiltro.SelectedItem.ToString());

            dgvDados.DataSource = dt;

        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            Carregar();
        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carregar();
        }
    }
}
