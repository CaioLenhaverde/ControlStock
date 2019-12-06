using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public class ControllerConfiguracaoSql
    {
        public bool Salvar(ConfiguracaoSql configuracaoSql)
        {
            try
            {
                // Armazena configurações de conexão com o banco no arquivo de config do sistema.
                Properties.Settings.Default.ServidorBanco = configuracaoSql.ServidorBanco;
                Properties.Settings.Default.NomeBanco = configuracaoSql.NomeBanco;
                Properties.Settings.Default.UsuarioBanco = configuracaoSql.UsuarioBanco;
                Properties.Settings.Default.SenhaBanco = configuracaoSql.SenhaBanco;

                Properties.Settings.Default.Save();

                return true;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
        public ConfiguracaoSql Carregar()
        {
            ConfiguracaoSql configuracaoSql = new ConfiguracaoSql();
            configuracaoSql.ServidorBanco = Properties.Settings.Default.ServidorBanco;
            configuracaoSql.NomeBanco = Properties.Settings.Default.NomeBanco;
            configuracaoSql.UsuarioBanco = Properties.Settings.Default.UsuarioBanco;
           configuracaoSql.SenhaBanco = Properties.Settings.Default.SenhaBanco;

            return configuracaoSql;
        }
    }
}
