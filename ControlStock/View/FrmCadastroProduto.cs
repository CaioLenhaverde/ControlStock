using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class FrmCadastroProduto : Form
    {
        public FrmCadastroProduto()
        {
            InitializeComponent();
        }
        bool ValidarCampos()
        {
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text))
            {
                errorProvider.SetError(txtNomeProduto, "Informe o nome do produto!");
                errorProvider.SetIconPadding(txtNomeProduto, -20);
                txtNomeProduto.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            { 
                errorProvider.SetError(txtCodigoBarras, "Informe o código de barras!");
                errorProvider.SetIconPadding(txtCodigoBarras, -20);
                txtCodigoBarras.Focus();
                return false;
            }
            if (cbxUnidadeMedida.SelectedIndex < 0)
            {
                errorProvider.SetError(cbxUnidadeMedida, " Informe a unidade de medida!");
                errorProvider.SetIconPadding(cbxUnidadeMedida, -40);
                cbxUnidadeMedida.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtValorUnitario.Text))
            {
                errorProvider.SetError(txtValorUnitario, "Informe o valor unitário!");
                errorProvider.SetIconPadding(txtValorUnitario, -20);
                txtValorUnitario.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                errorProvider.SetError(txtPreco, "Informe o preço!");
                errorProvider.SetIconPadding(txtPreco, -20);
                txtPreco.Focus();


                return false;
            }
            if (nudQtdMaxima.Value == 0)
            {
                errorProvider.SetError(nudQtdMaxima, "informe uma quantidade maxima que pode ter em estoque desse produto");
                errorProvider.SetIconPadding(nudQtdMaxima, -20);
                nudQtdMaxima.Focus();
                return false;
            }

            if (nudQtdMinima.Value > nudQtdMaxima.Value)
            {
                errorProvider.SetError(nudQtdMaxima, "Você não pode cadastrar uma quantidade mínima superior a quantidade máxima");
                errorProvider.SetIconPadding(nudQtdMinima, -20);
                nudQtdMaxima.Focus();
                return false;
            }

            return true;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            ValidarCampos();
        }
    }
}
