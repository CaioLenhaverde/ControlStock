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
    public partial class FrmVenda : Form
    {
        public FrmVenda()
        {
            InitializeComponent();
        }
        double ValorTotal = 0;

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            ControllerMovimentacao controllerMovimentacao = new ControllerMovimentacao();

            try
            {
                Produto produto = controllerMovimentacao.CarregarProduto(txtCodigoBarras.Text.Trim());

                if (produto.Codigo <= 0)
                    MessageBox.Show("Nenhum produto encontra com esse codigo de barras!");

                txtCodigo.Text = produto.Codigo.ToString();
                txtCodigoBarras.Text = produto.CodigoBarras;
                txtDescricao.Text = produto.NomeProduto;
                txtUnidadeMedida.Text = produto.UnidadeMedida;
                txtQtdAtual.Text = produto.QtdAtual.ToString();
                txtValorUnitario.Text = produto.PrecoVenda.ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        void ValidaDinheiro(TextBox textbox)
        {
            // Recebe o controle Textbox inteiro para manipular.
            string n = string.Empty;
            double valor = 0;
            try
            {
                // Remove os pontos e virgula da string do Textbox.
                n = textbox.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                    n = "";
                //Retorna uma nova string no qual o início é preenchido com espaços.
                n = n.PadLeft(3, '0');

                //Validando zeros a esquerda.
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);

                //Tentativa de converter o valor para double.
                valor = Convert.ToDouble(n) / 100;
                textbox.Text = string.Format("{0:N}", valor);

                //Sempre mantém o cursor no início do Textbox.
                textbox.SelectionStart = textbox.Text.Length;
            }
            catch { }
        }

        void AtualizaValores()
        {
            txtValorTotal.Text.Replace(",", "").Replace("R", "").Replace("$", "");
            txtSubTotal.Text.Replace(",", "").Replace("R", "").Replace("$", "");

            txtValorTotal.Text = this.ValorTotal.ToString("C");
            txtSubTotal.Text = (this.ValorTotal - Convert.ToDouble(txtDesconto.Text)).ToString("C");
        }



        private void ckbDesconto_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDesconto.Checked == false)
            {
                txtDesconto.Enabled = false;
                txtDesconto.Text = "";
            }
            else
                txtDesconto.Enabled = true;
        }

        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            if (btnNovaVenda.Text == "Nova")
            {
                gbxDetalhesVenda.Enabled = true;
                btnNovaVenda.Text = "Finalizar";
                btnNovaVenda.Image = Properties.Resources.tick;
            }
            else
            {
                ControllerVenda controllerVenda = new ControllerVenda();
                Venda venda = new Venda();
                venda.ValorTotal = this.ValorTotal;
                venda.Desconto = Convert.ToDouble(txtDesconto.Text);

                List<Produto> listaProdutos = new List<Produto>();



                foreach (DataGridViewRow item in dgvDados.Rows)
                {
                    Produto produto = new Produto();
                    produto.Codigo = Convert.ToInt32(item.Cells["codigoproduto"].Value);
                    produto.QtdAtual = Convert.ToInt32(item.Cells["quantidade"].Value);
                    produto.PrecoVenda = Convert.ToDouble(item.Cells["valortotal"].Value);
                    listaProdutos.Add(produto);
                }

                bool retorno = controllerVenda.Registrar(venda, listaProdutos);
                if (retorno == true)
                {
                    MessageBox.Show("Venda Finalizada!");
                    LimparCampos(this.gbxDetalhesVenda);


                }
            }
        }
        void LimparCampos(Control controle)
        {
            foreach (Control control in controle.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                nudQuantidade.Value = 1;
                ckbDesconto.Checked = false;
                gbxDetalhesVenda.Enabled = false;
                btnNovaVenda.Text = "Nova";
                txtValorTotal.Text = "0";
                txtSubTotal.Text = "0";
                dgvDados.Rows.Clear();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Você precisa carregar um produto para adicionar na venda!");
            }
            else if (Convert.ToInt32(txtQtdAtual.Text) < nudQuantidade.Value)
            {
                MessageBox.Show("Quantidade indisponível no estoque!");
            }
            else
            {
                double valorTotal = Convert.ToDouble(txtValorUnitario.Text) * Convert.ToInt32(nudQuantidade.Value);
                dgvDados.Rows.Add(txtCodigo.Text, txtDescricao.Text, txtValorUnitario.Text, nudQuantidade.Value.ToString(), txtUnidadeMedida.Text, valorTotal.ToString());

                this.ValorTotal += valorTotal;
                AtualizaValores();
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dgvDados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Você precisa selecionar um produto da venda para remover");
            }
            else
            {
                string valorTotal = dgvDados.CurrentRow.Cells["valortotal"].Value.ToString();

                dgvDados.Rows.Remove(dgvDados.CurrentRow);
                this.ValorTotal -= Convert.ToDouble(valorTotal);
                AtualizaValores();
            }
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para permitir somente números.
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            ValidaDinheiro(txtDesconto);
            if (this.ValorTotal == Convert.ToDouble(txtDesconto.Text))
            {
                MessageBox.Show("Não é possível aplicar desconto maior que o valor da venda!");
            }
            else
            {
                AtualizaValores();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos(this.gbxDetalhesVenda);
        }
    }
}
