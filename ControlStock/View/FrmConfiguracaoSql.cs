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
    public partial class FrmConfiguracaoSql : Form
    {
        public FrmConfiguracaoSql()
        {
            InitializeComponent();
            carregar();
        }
        bool validarDados()
        {
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtServidor.Text))
            {
                errorProvider.SetError(txtServidor, " Informe o Servidor do banco de dados!");
                errorProvider.SetIconAlignment(txtServidor, ErrorIconAlignment.MiddleLeft);
                txtServidor.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtBanco.Text))
            {
                errorProvider.SetError(txtBanco, " Informe o Nome do banco de dados!");
                errorProvider.SetIconAlignment(txtBanco, ErrorIconAlignment.MiddleLeft);
                txtBanco.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                errorProvider.SetError(txtUsuario, " Informe o Usuário com acesso ao banco de dados!");
                errorProvider.SetIconAlignment(txtUsuario, ErrorIconAlignment.MiddleLeft);
                txtUsuario.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                errorProvider.SetError(txtSenha, " Informe a Senha do banco de dados!");
                errorProvider.SetIconAlignment(txtSenha, ErrorIconAlignment.MiddleLeft);
                txtSenha.Focus();
                return false;
            }
            return true;
        }

        void carregar()
        {
            ControllerConfiguracaoSql controllerConfiguracaoSql = new ControllerConfiguracaoSql();
            ConfiguracaoSql configuracaoSql = new ConfiguracaoSql();
            configuracaoSql = controllerConfiguracaoSql.Carregar();

            txtServidor.Text = configuracaoSql.ServidorBanco;
            txtBanco.Text = configuracaoSql.NomeBanco;
            txtUsuario.Text = configuracaoSql.UsuarioBanco;
            txtSenha.Text = configuracaoSql.SenhaBanco;
        }

        void limpar()
        {
            txtBanco.Text = "";
            txtServidor.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";
            btnCancelar.Enabled = false;
            pnlDados.Enabled = false;
            carregar();
            btnEditar.Text = "Editar";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            pnlDados.Enabled = true;
            btnCancelar.Enabled = true;

            validarDados();

            if (btnEditar.Text == "Editar")
            {
                btnEditar.Text = "Salvar";
                btnEditar.Image = Properties.Resources.tick;
                pnlDados.Enabled = true;
            }
            else
            {
                if (validarDados())
                {
                    ConfiguracaoSql configuracaoSql = new ConfiguracaoSql();
                    configuracaoSql.ServidorBanco = txtServidor.Text.Trim();
                    configuracaoSql.NomeBanco = txtBanco.Text.Trim();
                    configuracaoSql.UsuarioBanco = txtUsuario.Text.Trim();
                    configuracaoSql.SenhaBanco = txtSenha.Text.Trim();

                    ControllerConfiguracaoSql controllerConfiguracaoSql = new ControllerConfiguracaoSql();

                    try
                    {
                        bool retorno = controllerConfiguracaoSql.Salvar(configuracaoSql);

                        if (retorno == true)
                        {
                            MessageBox.Show("Configuração salva com sucesso!");
                            limpar();
                        }
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Não foi possível salvar as configurações. Detalhes: " + erro.Message);
                    }
                }
            }
        }
        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
