using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace BackupPro
{
    public partial class TesteSQLite : Form
    {
        public class Pessoas
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }

        private static string conexao = "Data Source=Banco.db";
        private static string nomeBanco = "Banco.db";
        private static int IdRegistro;

        public TesteSQLite()
        {
            InitializeComponent();
        }

        private void TesteSQLite_Load(object sender, EventArgs e)
        {
            if (!File.Exists(nomeBanco))
            {
                SQLiteConnection.CreateFile(nomeBanco);
                SQLiteConnection conn = new SQLiteConnection(conexao);
                conn.Open();

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("CREATE TABLE IF NOT EXISTS PESSOAS ([ID] INTEGER PRIMARY KEY AUTOINCREMENT, ");
                sql.AppendLine("[NOME] VARCHAR(100))");

                SQLiteCommand cmd = new SQLiteCommand(sql.ToString(), conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("erro ao criar banco!" + ex.Message);
                }
            }
            CarregarGrid();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Pessoas pessoa = new Pessoas();
            pessoa.Nome = txtNome.Text;

            SQLiteConnection conn = new SQLiteConnection(conexao);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO PESSOAS (NOME) VALUES (@NOME)", conn);
            cmd.Parameters.AddWithValue("NOME", pessoa.Nome);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro salvo");
                txtNome.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro  ao gravar registro", ex.Message);
            }
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            SQLiteConnection conn = new SQLiteConnection(conexao);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM PESSOAS", conn);
            SQLiteDataReader dr = cmd.ExecuteReader();
            List<Pessoas> lista = new List<Pessoas>();
            while (dr.Read())
            {
                lista.Add(new Pessoas
                {
                    Id = Convert.ToInt32(dr["ID"]),
                    Nome = dr["NOME"].ToString()
                });
            }
            dataGridView1.DataSource = lista;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(conexao);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM PESSOAS WHERE ID = @ID", conn);
            cmd.Parameters.AddWithValue("ID", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro excluido");
                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro  ao excluir registro ", ex.Message);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (IdRegistro > 0)
            {
                SQLiteConnection conn = new SQLiteConnection(conexao);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SQLiteCommand cmd = new SQLiteCommand("UPDATE PESSOAS SET NOME =@NOME WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("NOME", txtNome.Text);
                cmd.Parameters.AddWithValue("ID", IdRegistro);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro Atualizado");
                    CarregarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro  ao Atualizar registro ", ex.Message);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            IdRegistro = 0;
            IdRegistro = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            txtNome.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
