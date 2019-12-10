using Model;
using Controller;
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

            //carrega o filtro TODOS por padrão.
            cbxFiltro.SelectedItem = "Todos";
            CarregarProdutos();
            
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

        void SalvarDadosProduto()
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

        void CarregarProdutos()
        {
            txtPesquisa.Focus();
            try
            {
                ControllerProduto controllerProduto = new ControllerProduto();
                DataTable dt = new DataTable();
                dt = controllerProduto.Carregar(txtPesquisa.Text, cbxFiltro.SelectedItem.ToString());
                dgvDados.DataSource = dt;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            txtPesquisa.Focus();
        }

        public void LimparCampos(Control controle)
        {
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.Enabled = true;
            btnEditar.Enabled = true;
            btnEditar.Text = "Atualizar";
            btnEditar.Enabled = true;
            btnCancelar.Enabled = false;
            gbxProdutosCadastrador.Enabled = true;
            gbxInformacoesProduto.Enabled = false;

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
                ckbAtivo.Checked = false;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            LimparCampos(gbxInformacoesProduto);
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
            else
            {
                
                if (ValidarCampos())
                {
                    SalvarDadosProduto();

                    //Enviando para controller salvar no banco de daos.
                    ControllerProduto controllerProduto = new ControllerProduto();

                    //recebe o codigo gerado para o produto no banco de dandos com o auto incremento.
                    int codigo = controllerProduto.Persistir(produto, "cadastrar");

                    
                    if(codigo > 0)
                    {
                        MessageBox.Show("PRODUTO CADASTRADO COM SUCESSO!");
                        LimparCampos(gbxInformacoesProduto);

                        //carrega novamente para carregar o novo produto cadastrado.
                        CarregarProdutos();
                    }
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos(this.gbxInformacoesProduto);
            
            
        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        //esse comando passa os dados do datagraviw para cada dominio   caso queira editar alguma informação.
        private void dgvDados_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvDados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Você precisa selecionar um produto da lista!");
            }
            else
            {
                txtCodigo.Text = dgvDados.CurrentRow.Cells["codigo"].Value.ToString();
                txtCodigoBarras.Text = dgvDados.CurrentRow.Cells["codigobarras"].Value.ToString();
                txtNomeProduto.Text = dgvDados.CurrentRow.Cells["nomeproduto"].Value.ToString();
                cbxUnidadeMedida.SelectedItem = dgvDados.CurrentRow.Cells["unidademedida"].Value.ToString();
                nudQtdMinima.Value = (int)dgvDados.CurrentRow.Cells["qtdminima"].Value;
                nudQtdAtual.Value = (int)dgvDados.CurrentRow.Cells["qtdatual"].Value;
                nudQtdMaxima.Value = (int)dgvDados.CurrentRow.Cells["qtdmaxima"].Value;
                txtValorUnitario.Text = dgvDados.CurrentRow.Cells["valorunitario"].Value.ToString();
                txtPercentualLucro.Text = dgvDados.CurrentRow.Cells["percentuallucro"].Value.ToString();
                txtPreco.Text = dgvDados.CurrentRow.Cells["precovenda"].Value.ToString();

                if (dgvDados.CurrentRow.Cells["ativo"].Value.ToString() == "Sim")
                    ckbAtivo.Checked = true;
                else
                    ckbAtivo.Checked = false;

                btnEditar.Enabled = true;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Você precisa selecionar um produto da lista!");
            }
            else
            {
                if (btnEditar.Text == "Atualizar")
                {
                    gbxInformacoesProduto.Enabled = true;
                    btnCancelar.Enabled = true;
                    gbxProdutosCadastrador.Enabled = false;
                    btnEditar.Text = "Salvar";

                    txtCodigoBarras.Focus();
                    btnCadastrar.Enabled = false;
                    nudQtdAtual.Enabled = false;
                    lblLembrete.Visible = true;
                }
                else
                {
                    if (ValidarCampos())
                    {
                        SalvarDadosProduto();

                        // *UPDATE*  atualiza produtos cadastrados.
                        ControllerProduto controllerProduto = new ControllerProduto();
                        int codigo = controllerProduto.Persistir(produto, "atualizar");

                        if (codigo > 0)
                        {
                            MessageBox.Show("Atualizado com sucesso!");
                            LimparCampos(gbxInformacoesProduto);
                            CarregarProdutos();
                        }
                    }
                }
            }
        }
    }
}
