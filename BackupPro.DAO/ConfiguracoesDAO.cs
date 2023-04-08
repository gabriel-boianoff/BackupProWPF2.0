using BackupPro.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPro.DAO
{
    public class ConfiguracoesDAO
    {
        public DbDataReader GetConfiguracoes()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM CONFIGURACOES";
            DbDataReader reader = DAOUtils.GetDatReader(comando);
            return reader;
        }

        public void Inserir(Configuracoes configuracoes)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO CONFIGURACOES (FTP, USER, PASS, PORTA) VALUES (@ftp, @user, @pass, @porta)";
            comando.Parameters.Add(new SQLiteParameter("@ftp", configuracoes.FTP));
            comando.Parameters.Add(new SQLiteParameter("@user", configuracoes.User));
            comando.Parameters.Add(new SQLiteParameter("@pass", configuracoes.Pass));
            comando.Parameters.Add(new SQLiteParameter("@porta", configuracoes.Porta));
            comando.ExecuteNonQuery();
        }

        public void Atualizar(Configuracoes configuracoes)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE CONFIGURACOES SET FTP = @ftp, USER = @user, PASS = @pass, PORTA = @porta";
            comando.Parameters.Add(new SQLiteParameter("@ftp", configuracoes.FTP));
            comando.Parameters.Add(new SQLiteParameter("@user", configuracoes.User));
            comando.Parameters.Add(new SQLiteParameter("@pass", configuracoes.Pass));
            comando.Parameters.Add(new SQLiteParameter("@porta", configuracoes.Porta));
            comando.ExecuteNonQuery();
        }

        public void CriarTabelaConfiguracoes()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE IF NOT EXISTS CONFIGURACOES ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, ");
            sql.AppendLine("[FTP] VARCHAR(100),");
            sql.AppendLine("[User] VARCHAR(100),");
            sql.AppendLine("[Pass] VARCHAR(100),");
            sql.AppendLine("[Porta] VARCHAR(100));");
            comando.CommandText = sql.ToString();
            comando.ExecuteNonQuery();
        }
    }
}
