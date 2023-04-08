using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPro.DAO
{
    public class DAOUtils
    {
        private static string conexaoBanco = "Data Source=Banco.db";
        private static string nomeBanco = "Banco.db";
        
        public static DbConnection GetConexao()
        {           
            if (!File.Exists(nomeBanco))
                SQLiteConnection.CreateFile(nomeBanco);
            
            DbConnection conexao = new SQLiteConnection(conexaoBanco);
            conexao.Open();
            return conexao;

        }

        public static DbCommand GetComando(DbConnection conexao)
        {
            DbCommand comando = conexao.CreateCommand();
            return comando;
        }

        public static DbDataReader GetDatReader(DbCommand comando)
        {
            return comando.ExecuteReader();
        }
    }
}
