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
    public class ControllerMovimentacao
    {
        public Produto CarregarProduto(string codigoBarras)
        {
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {

                string instrucao = "SELECT * FROM Produto WHERE codigobarras = '" + codigoBarras + "'";

                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Produto produto = new Produto();
                foreach (DataRow item in dt.Rows)
                {
                    produto.Codigo = Convert.ToInt32(item["codigo"].ToString());
                    produto.NomeProduto = item["nomeproduto"].ToString();
                    produto.UnidadeMedida = item["unidademedida"].ToString();
                    produto.QtdMinima = Convert.ToInt32(item["qtdminima"].ToString());
                    produto.QtdMaxima = Convert.ToInt32(item["qtdmaxima"].ToString());
                    produto.QtdAtual = Convert.ToInt32(item["qtdatual"].ToString());
                    produto.PrecoVenda = Convert.ToDouble(item["precovenda"].ToString());
                }

                return produto;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                acessoDados.FecharConexao();
            }
        }

        public int Movimentar(Movimentacao movimentacao)
        {
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();

            try
            {
                string instrucaoEstoque = "INSERT INTO Estoque (quantidade, codigoproduto, data, hora, motivo, acao) VALUES (@quantidade, @codigoproduto, @data, @hora, @motivo, @acao); ";

                string instrucaoProduto = "";

                if (movimentacao.Acao == "Entrada")
                    instrucaoProduto = "UPDATE Produto Set qtdatual = qtdatual + @quantidade WHERE codigo = @codigoproduto";
                else
                    instrucaoProduto = "UPDATE Produto SET qtdatual = qtdatual - @quantidade WHERE codigo = @codigoproduto";

                SqlCommand command = new SqlCommand(instrucaoEstoque + instrucaoProduto, acessoDados.Conectar());
                command.Parameters.AddWithValue("@quantidade", movimentacao.Quantidade);
                command.Parameters.AddWithValue("@codigoproduto", movimentacao.CodigoProduto);
                command.Parameters.AddWithValue("@data", movimentacao.Data);
                command.Parameters.AddWithValue("@hora", movimentacao.Hora);
                command.Parameters.AddWithValue("@motivo", movimentacao.Motivo);
                command.Parameters.AddWithValue("@acao", movimentacao.Acao);

                return Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public DataTable Lista (string nomeProduto, string acao)
        {
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {
                if (acao == "Todas")
                    acao = "";

                string instrucao = "SELECT Estoque.data, Estoque.hora, Estoque.motivo, Estoque.acao, Estoque.quantidade, Produto.nomeproduto as produto, Produto.unidademedida, Produto.qtdatual FROM Estoque INNER JOIN Produto ON(Estoque.codigoproduto = Produto.codigo) WHERE Produto.nomeproduto  LIKE '%" + nomeProduto + "%' AND Estoque.acao LIKE '%" + acao + "%'";

                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch ( Exception erro)
            {
                throw erro;
            }
            finally
            {
                acessoDados.FecharConexao();
            }
        }

        public int QtdeProdutoEstoqueMaximo()
        {
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {
                string instrucao = "SELECT COUNT(codigo) FROM Produto WHERE qtdatual > qtdmaxima";

                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                int retorno = Convert.ToInt32(command.ExecuteScalar());
                return retorno;
            }
            catch (Exception erro)
            {
                throw erro;
            }
            finally
            {
                acessoDados.FecharConexao();
            }
        }

        public int QtdeProdutoEstoqueMinimo()
        {
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {
                string instrucao = "SELECT COUNT(codigo) FROM Produto WHERE qtdatual < qtdminima";

                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                int retorno = Convert.ToInt32(command.ExecuteScalar());
                return retorno;
            }
            catch (Exception erro)
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
