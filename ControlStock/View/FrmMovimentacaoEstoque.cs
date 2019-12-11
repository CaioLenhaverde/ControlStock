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
using Model;

namespace View
{
    public partial class FrmMovimentacaoEstoque : Form
    {
        public FrmMovimentacaoEstoque()
        {
            InitializeComponent();
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            ControllerMovimentacao controllerMovimentacao = new ControllerMovimentacao();
            try
            {
                Produto produto = controllerMovimentacao.CarregarProduto(txtCodigoBarras.Text.Trim());
                if (produto.Codigo <= 0)
                    MessageBox.Show("Nenhum Produto Encontrado com esse código de barras!");
                else
                {
                    if (btnCarregar.Text == "Salvar" && ValidaQuantidade())
                    {

                        Movimentacao movimentacao = new Movimentacao();
                        movimentacao.CodigoProduto = Convert.ToInt32(txtCodigo.Text);
                        movimentacao.CodigoProduto = Convert.ToInt32(txtCodigo.Text);
                        movimentacao.Quantidade = Convert.ToInt32(nudQuantidade.Value);
                        movimentacao.Motivo = rtbMotivo.Text;
                        movimentacao.Acao = cbxTipoMovimentacao.SelectedItem.ToString();
                        movimentacao.Data = DateTime.Today.ToShortDateString();
                        movimentacao.Hora = DateTime.Now.ToShortTimeString();

                        int retorno = controllerMovimentacao.Movimentar(movimentacao);
                        if (retorno > 0)
                        {
                            MessageBox.Show("Movimentação de Estoque registrada com sucesso!");
                            
                        }
                    }
                    else
                    {
                        txtCodigo.Text = produto.Codigo.ToString();
                        txtDescricao.Text = produto.NomeProduto;
                        txtUnidadeMedida.Text = produto.UnidadeMedida;
                        nudQtdeMinima.Value = produto.QtdMinima;
                        nudQtdeMaxima.Value = produto.QtdMaxima;
                        nudQtdeAtual.Value = produto.QtdAtual;

                        gbxDetalhesMovimentacao.Enabled = true;
                        btnCarregar.Text = "Salvar";
                        btnCarregar.Image = Properties.Resources.tick;
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
    
        }

        bool ValidaQuantidade()
        {

            if (nudQuantidade.Value >= nudQtdeAtual.Value && cbxTipoMovimentacao.SelectedItem.ToString() == "Saída")
            {
                MessageBox.Show("Quantidade indisponível para dar saída no estoque. A quantidade atual deste produto é: " + nudQtdeAtual.Value.ToString());
                return false;
            }


            int quantidadeTotal = Convert.ToInt32(nudQuantidade.Value) + Convert.ToInt32(nudQtdeAtual.Value);

            if (quantidadeTotal >= nudQtdeMaxima.Value && cbxTipoMovimentacao.SelectedItem.ToString() == "Entrada")
            {
                MessageBox.Show("Você não pode dar entrada em uma quantidade maior que o limite do estoque deste produto.");
                return false;
            }

            return true;
        }

        //quando max de caracteres do motivo ser atingido muda de cor.
        

        public void LimparCampos(Control controle)
        {

            foreach (Control control in controle.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                if (control is NumericUpDown)
                {
                    ((NumericUpDown)control).Value = 0;
                }
                if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1;
                }

                rtbMotivo.Text = "";
            }
        }

        private void rtbMotivo_TextChanged(object sender, EventArgs e)
        {
            lblCaracteres.Text = "Caracteres: " + rtbMotivo.TextLength + "/200";

            if (rtbMotivo.TextLength == rtbMotivo.MaxLength)
                lblCaracteres.ForeColor = Color.Red;
            else
                lblCaracteres.ForeColor = Color.Black;
        }
    }

}
