using BackupPro.Dominio;
using BackupPro.Repositorio;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupPro
{
    public partial class frmPrincipal : Form
    {
        IRepositorio repositorio = new RepositorioBackupBlocoNotas();

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnOrigem_Click(object sender, EventArgs e)
        {
            frmOrigem frmOrigem = new frmOrigem();
            frmOrigem.ShowDialog();
        }

        private void btnDestino_Click(object sender, EventArgs e)
        {
            frmDestino frmDestino = new frmDestino();
            frmDestino.ShowDialog();
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            try
            {
                repositorio.Copiar();
                MessageBox.Show("Copiado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZipar_Click(object sender, EventArgs e)
        {
            try
            {
                int progresso = 0;
                pbrProgresso.Maximum = 100;
                ZiparArquivo();
                //Progresso();
                pbxLoad.Visible = true;
                while (progresso < 100)
                {                    
                    progresso = RepositorioBackupBlocoNotas.GetProgresso();
                    pbrProgresso.Value = progresso;
                }
                
                pbxLoad.Enabled = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void Progresso()
        {
            await Task.Run(() =>
            {
                int progresso = 0;
                while (progresso < 100)
                {
                    progresso = RepositorioBackupBlocoNotas.GetProgresso();
                }
                pbrProgresso.Invoke((MethodInvoker)delegate
                {
                    pbrProgresso.Maximum = 100;
                    pbrProgresso.Value = progresso;
                });
            });
        }

        public void ZiparArquivo()
        {
            Task.Run(() =>
            {
                repositorio.Zipar();
                MessageBox.Show("Zipado");
            });
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            TesteSQLite teste = new TesteSQLite();
            teste.ShowDialog();
        }
    }
}
