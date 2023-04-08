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
    public class DestinoDAO
    {
        public DbDataReader GetDestinos()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM DESTINOS";
            DbDataReader reader = DAOUtils.GetDatReader(comando);
            //DataTable dataTable = new DataTable();
            //dataTable.Load(reader);
            return reader;
        }

        public void Excluir(int id)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE FROM DESTINOS WHERE ID = @ID";
            comando.Parameters.Add(new SQLiteParameter("@id", id));
            comando.ExecuteNonQuery();
        }

        public void Inserir(Destino destino)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO DESTINOS (CAMINHODIRETORIO, NOMEDIRETORIO, UNIDADE, ESPACODISPONIVEL, ESPACOTOTAL) " +
                 "VALUES (@CaminhoDiretorio, @NomeDiretorio, @Unidade, @EspacoDisponivel, @EspacoTotal)";
            comando.Parameters.Add(new SQLiteParameter("@CaminhoDiretorio", destino.CaminhoDiretorio));
            comando.Parameters.Add(new SQLiteParameter("@NomeDiretorio", destino.NomeDiretorio));
            comando.Parameters.Add(new SQLiteParameter("@Unidade", destino.Unidade));
            comando.Parameters.Add(new SQLiteParameter("@EspacoDisponivel", destino.EspacoDisponivel));
            comando.Parameters.Add(new SQLiteParameter("@EspacoTotal", destino.EspacoTotal));
            comando.ExecuteNonQuery();
        }

        public void CriarTabelaDestinos()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("CREATE TABLE IF NOT EXISTS DESTINOS ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, ");
            sql.AppendLine("[CaminhoDiretorio] VARCHAR(1000),");
            sql.AppendLine("[NomeDiretorio] VARCHAR(1000),");
            sql.AppendLine("[Unidade] VARCHAR(100),");
            sql.AppendLine("[EspacoDisponivel] DOUBLE,");
            sql.AppendLine("[EspacoTotal] DOUBLE);");            
            comando.CommandText = sql.ToString();
            comando.ExecuteNonQuery();
        }
    }
}
