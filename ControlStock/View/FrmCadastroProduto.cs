using Model;
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
        Produto produto = new Produto();

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

        void CarregarDadosProduto()
        {
            //Montado o objeto Produto com as informações para enviar para a controller
            if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
                produto.Codigo = Convert.ToInt32(txtCodigo.Text);

            produto.NomeProduto = txtNomeProduto.Text;
            produto.CodigoBarras = txtCodigoBarras.Text;
            produto.UnidadeMedida = cbxUnidadeMedida.SelectedItem.ToString();
            produto.CustoUnitario = Convert.ToDouble(txtValorUnitario.Text);
            produto.PrecoVenda = Convert.ToDouble(txtPreco.Text);
            produto.PercentualLucro = Convert.ToDouble(txtPercentualLucro.Text);
            produto.QtdAtual = (int)nudQtdAtual.Value;
            produto.QtdMaxima = (int)nudQtdMaxima.Value;
            produto.QtdMinima = (int)nudQtdMinima.Value;

            //if ternario
            produto.Ativo = ckbAtivo.Checked ? "Sim" : "Não";

        }
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (btnCadastrar.Text == "Cadastrar")
            {
                btnCadastrar.Text = "Salvar";
                btnCadastrar.Focus();
                btnCancelar.Enabled = true;
                btnEditar.Enabled = false;
                gbxInformacoesProduto.Enabled = true;
                gbxProdutosCadastrador.Enabled = false;

                //Qtdatual só é habilitada no casdastro. para evitar fraude.
                //para alteração da quantidade em estoque, podera ser feita na tela de movimentação de estoque.
                nudQtdAtual.Enabled = true;
            }
            else if (btnCadastrar.Text == "Salvar")
            {
                if (ValidarCampos())
                {
                    CarregarDadosProduto();
                }
            }
        }
    }
}
