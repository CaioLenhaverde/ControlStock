using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Data;

namespace Controller
{
    public class ControllerProduto
    {
        //cadatrar dados no banco
        public int Persistir(Produto produto, string tipo)
        {
            //conexão com o banco de dados.
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {
                string instrucao = "";
                if (tipo == "cadastrar")
                {
                    instrucao = "INSERT INTO Produto (codigobarras, nomeproduto, unidademedida, qtdminima, qtdmaxima, qtdatual, custounitario, percentuallucro, precovenda, ativo) VALUES (@codigobarras, @nomeproduto, @unidademedida, @qtdminima, @qtdmaxima, @qtdatual, @custounitario, @precovenda, @percentuallucro, @ativo); SELECT SCOPE_IDENTITY();";
                }
                else if (tipo == "atualizar")
                {
                    instrucao = "UPDATE Produto SET codigobarras=@codigobarras, nomeproduto=@nomeproduto, unidademedida=@unidademedida, qtdminima=@qtdminima, qtdmaxima=@qtdmaxima, qtdatual=@qtdatual, custounitario=@custounitario, percentuallucro=@percentuallucro, precovenda=@precovenda, ativo=@ativo WHERE codigo=@codigo";
                }

                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());

                if (tipo == "atualizar")
                {
                    command.Parameters.AddWithValue("@codigo", produto.Codigo);
                }
                command.Parameters.AddWithValue("@codigobarras", produto.CodigoBarras);
                command.Parameters.AddWithValue("@nomeproduto", produto.NomeProduto);
                command.Parameters.AddWithValue("@unidademedida", produto.UnidadeMedida);
                command.Parameters.AddWithValue("@qtdminima", produto.QtdMinima);
                command.Parameters.AddWithValue("@qtdmaxima", produto.QtdMaxima);
                command.Parameters.AddWithValue("@qtdatual", produto.QtdAtual);
                command.Parameters.AddWithValue("@custounitario", produto.CustoUnitario);
                command.Parameters.AddWithValue("@percentuallucro", produto.PercentualLucro);
                command.Parameters.AddWithValue("@precovenda", produto.PrecoVenda);
                command.Parameters.AddWithValue("@ativo", produto.Ativo);

                //retorna o codigo gerado para o produto.
                return Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch 
            {
                throw new Exception();
            }
            finally
            {
                acessoDados.FecharConexao();
            }
            
        }

        //chamando dados do banco
        public DataTable Carregar(string descricao, string ativo)
        {
            string instrucao;
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();

            if (ativo == "Todos")
                instrucao = "SELECT * FROM Produto WHERE nomeproduto LIKE '%" + descricao + "%'";
            else
                instrucao = "SELECT * FROM Produto WHERE nomeproduto LIKE '%" + descricao + "%' AND ativo = '" + ativo + "'";

            try
            {
                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
            catch(Exception erro)
            {
                throw erro;
            }
            finally
            {
                acessoDados.FecharConexao();
            }
        }
    }
}
