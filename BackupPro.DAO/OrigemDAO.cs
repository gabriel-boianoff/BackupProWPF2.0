using BackupPro.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupPro.DAO
{
    public class OrigemDAO
    {
        DbDataReader reader;
        public DbDataReader GetOrigens()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM ORIGENS";
            reader = DAOUtils.GetDatReader(comando);
            //DataTable dataTable = new DataTable();
            //dataTable.Load(reader);
            return reader;
        }

        public void Excluir(int id)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE FROM ORIGENS WHERE ID = @id";
            comando.Parameters.Add(new SQLiteParameter("@id", id));
            comando.ExecuteNonQuery();
        }

        public void Inserir(Origem origem)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO ORIGENS (CAMINHOARQUIVO, NOMEARQUIVO, DIRETORIOARQUIVO, NOMEARQUIVOSEMEXTENSAO, EXTENSAOARQUIVO, " +
                 "TAMANHOARQUIVO, NOMETAREFA, TIPOTAREFA, HORARIOTAREFA, ZIPAR, NUVEM, ULTIMOBACKUP) " +
                 "VALUES (@caminhoArquivo, @nomeArquivo, @diretorioArquivo, @nomeArquivoSemExtensao, @extensaoArquivo, @tamanhoArquivo, @nomeTarefa, " +
                 "@tipoTarefa, @horarioTarefa, @zipar, @nuvem, @ultimoBackup)";
            comando.Parameters.Add(new SQLiteParameter("@caminhoArquivo", origem.CaminhoArquivo));
            comando.Parameters.Add(new SQLiteParameter("@nomeArquivo", origem.NomeArquivo));
            comando.Parameters.Add(new SQLiteParameter("@diretorioArquivo", origem.DiretorioArquivo));
            comando.Parameters.Add(new SQLiteParameter("@nomeArquivoSemExtensao", origem.NomeArquivoSemExtensao));
            comando.Parameters.Add(new SQLiteParameter("@extensaoArquivo", origem.ExtensaoArquivo));
            comando.Parameters.Add(new SQLiteParameter("@tamanhoArquivo", origem.TamanhoArquivo));
            comando.Parameters.Add(new SQLiteParameter("@nomeTarefa", origem.NomeTarefa));
            comando.Parameters.Add(new SQLiteParameter("@tipoTarefa", origem.TipoTarefa));
            comando.Parameters.Add(new SQLiteParameter("@horarioTarefa", origem.HorarioTarefa));
            comando.Parameters.Add(new SQLiteParameter("@zipar", origem.Zipar));
            comando.Parameters.Add(new SQLiteParameter("@nuvem", origem.Nuvem));
            comando.Parameters.Add(new SQLiteParameter("@ultimoBackup", origem.UltimoBackup));
            comando.ExecuteNonQuery();
        }

        public void AtualizarDataBackup(Origem origem)
        {            
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE ORIGENS SET NOMETAREFA = @nomeTarefa, ULTIMOBACKUP = @ultimoBackup, SALVOCOMO = @SalvoComo WHERE ID = @id";
            comando.Parameters.Add(new SQLiteParameter("@nomeTarefa", origem.NomeTarefa));
            comando.Parameters.Add(new SQLiteParameter("@ultimoBackup", origem.UltimoBackup));
            comando.Parameters.Add(new SQLiteParameter("@salvoComo", origem.SalvoComo));
            comando.Parameters.Add(new SQLiteParameter("@id", origem.Id));
            comando.ExecuteNonQuery();
        }

        public DbDataReader GetUltimoBackup()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM ORIGENS ORDER BY ULTIMOBACKUP";
            DbDataReader reader = DAOUtils.GetDatReader(comando);
            return reader;
        }

        public void Atualizar(Origem origem)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE ORIGENS SET NOMETAREFA = @nomeTarefa, TIPOTAREFA = @tipoTarefa, HORARIOTAREFA = @horarioTarefa, ZIPAR = @zipar, NUVEM = @nuvem";
            comando.Parameters.Add(new SQLiteParameter("@nomeTarefa", origem.NomeTarefa));
            comando.Parameters.Add(new SQLiteParameter("@tipoTarefa", origem.TipoTarefa));
            comando.Parameters.Add(new SQLiteParameter("@horarioTarefa", origem.HorarioTarefa));
            comando.Parameters.Add(new SQLiteParameter("@zipar", origem.Zipar));
            comando.Parameters.Add(new SQLiteParameter("@nuvem", origem.Nuvem));
            //comando.Parameters.Add(new SQLiteParameter("@id", origem.Id));
            comando.ExecuteNonQuery();
        }

        public void CriarTabelaOrigens()
        {
            using (DbConnection conexao = DAOUtils.GetConexao())
            {
                using (DbCommand comando = DAOUtils.GetComando(conexao))
                {
                    comando.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("CREATE TABLE IF NOT EXISTS ORIGENS ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, ");
                    sql.AppendLine("[CaminhoArquivo] VARCHAR(1000),");
                    sql.AppendLine("[NomeArquivo] VARCHAR(1000),");
                    sql.AppendLine("[DiretorioArquivo] VARCHAR(1000),");
                    sql.AppendLine("[NomeArquivoSemExtensao] VARCHAR(1000),");
                    sql.AppendLine("[ExtensaoArquivo] VARCHAR(100),");
                    sql.AppendLine("[TamanhoArquivo] DOUBLE,");
                    sql.AppendLine("[NomeTarefa] VARCHAR(1000),");
                    sql.AppendLine("[TipoTarefa] VARCHAR(100),");
                    sql.AppendLine("[HorarioTarefa] TIME,");
                    sql.AppendLine("[Zipar] BOOL,");
                    sql.AppendLine("[Nuvem] BOOL,");
                    sql.AppendLine("[UltimoBackup] DATETIME,");
                    sql.AppendLine("[SalvoComo] VARCHAR(1000));");
                    comando.CommandText = sql.ToString();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}

