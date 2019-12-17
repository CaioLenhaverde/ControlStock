using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Controller
{
   public class ControllerVenda
    {
        public bool Registrar(Venda venda, List<Produto> itensVenda)
        {

            

            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {
                string instrucao = "INSERT INTO Venda (data, hora, valortotal, desconto, ativo) VALUES (@data, @hora, @valortotal, @desconto, @ativo); SELECT @@IDENTITY;";
                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                command.Parameters.AddWithValue("@data", DateTime.Today.ToShortDateString());
                command.Parameters.AddWithValue("@hora", DateTime.Now.ToShortTimeString());
                command.Parameters.AddWithValue("@valortotal", venda.ValorTotal);
                command.Parameters.AddWithValue("@desconto", venda.Desconto);
                command.Parameters.AddWithValue("@ativo", "Venda Registrada");


                // Recupera o id atribuido à venda.
                string codigoVenda = command.ExecuteScalar().ToString();


                // Esta etapa é REGISTRAR OS ITENS DA VENDA + ATUALIZAR A QUANTIDADE DISPONÍVEL NO ESTOQUE + Registrar A MOVIMENTAÇÃO NO ESTOQUE.
                foreach (Produto item in itensVenda)
                {
                    string instrucaoItemVenda = "INSERT INTO VendaItem (codigoproduto, codigovenda, quantidade, valor) VALUES (@codigoproduto, @codigovenda, @quantidade, @valor); UPDATE Produto SET qtdatual = qtdatual - @quantidade WHERE codigo = @codigoproduto; INSERT INTO Estoque (quantidade, codigoproduto, data, hora, motivo, acao) VALUES (@quantidade, @codigoproduto, @data, @hora, @motivo, @acao)";

                    SqlCommand command2 = new SqlCommand(instrucaoItemVenda, acessoDados.Conectar());
                    command2.Parameters.AddWithValue("@codigoproduto", item.Codigo);
                    command2.Parameters.AddWithValue("@codigovenda", codigoVenda);
                    command2.Parameters.AddWithValue("@quantidade", item.QtdAtual);
                    command2.Parameters.AddWithValue("@valor", item.PrecoVenda);
                    command2.Parameters.AddWithValue("@data", DateTime.Today.ToShortDateString());
                    command2.Parameters.AddWithValue("@hora", DateTime.Now.ToShortTimeString());
                    command2.Parameters.AddWithValue("@motivo", "Produto Vendido");
                    command2.Parameters.AddWithValue("@acao", "Saída");
                    command2.ExecuteNonQuery();
                }
                return true;
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

        public bool Cancelar(int codigoVenda)
        {
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {
                //MUDA OS STATUS DA VENDA.
                string instrucaoVenda = "UPDATE Venda SET ativo = 'Venda Cancelada' WHERE codigo = " + codigoVenda;
                SqlCommand command = new SqlCommand(instrucaoVenda, acessoDados.Conectar());
                command.ExecuteNonQuery();

                // Recupera os itens da venda cancelada.                
                string instrucaoItemsVenda = "SELECT * FROM VendaItem WHERE codigovenda = " + codigoVenda;
                SqlCommand commandItemsVenda = new SqlCommand(instrucaoItemsVenda, acessoDados.Conectar());
                SqlDataAdapter da = new SqlDataAdapter(commandItemsVenda);
                DataTable itemsVenda = new DataTable();
                da.Fill(itemsVenda);

                // Devolver os itens no estoque e registrar a movimentação.
                foreach (DataRow item in itemsVenda.Rows)
                {
                    string instrucaoItemVenda = "INSERT INTO Estoque (quantidade, codigoproduto, data, hora, motivo, acao) VALUES (@quantidade, @codigoproduto, @data, @hora, @motivo, @acao); UPDATE Produto SET qtdatual = qtdatual + " + item["quantidade"].ToString();

                    SqlCommand command2 = new SqlCommand(instrucaoItemVenda, acessoDados.Conectar());
                    command2.Parameters.AddWithValue("@codigoproduto", item["codigoproduto"].ToString());
                    command2.Parameters.AddWithValue("@quantidade", item["quantidade"].ToString());
                    command2.Parameters.AddWithValue("@data", DateTime.Today.ToShortDateString());
                    command2.Parameters.AddWithValue("@hora", DateTime.Now.ToShortTimeString());
                    command2.Parameters.AddWithValue("@motivo", "Venda Cancelada");
                    command2.Parameters.AddWithValue("@acao", "Entrada");
                    command2.ExecuteNonQuery();
                }

                return true;
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
