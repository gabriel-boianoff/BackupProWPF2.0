using BackupPro.Dominio;
using BackupPro.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupPro
{
    public partial class frmDestino : Form
    {
        public frmDestino()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            fbdDestino.ShowDialog();
            Destino destino = new Destino
            {
                CaminhoDiretorio = fbdDestino.SelectedPath
            };
            lbxDestinos.Items.Add(destino.CaminhoDiretorio);
            List<Destino> listaDestino = new List<Destino>();
            listaDestino.Add(destino);
            RepositorioBackupBlocoNotas.EscreverDestino(listaDestino);
            CarregarListaDestinos();
        }

        private void frmDestino_Shown(object sender, EventArgs e)
        {
            CarregarListaDestinos();
        }
        private void CarregarListaDestinos()
        {
            lbxDestinos.Items.Clear();
            lbxDestinos.Items.AddRange(RepositorioBackupBlocoNotas.LerDestino().ToArray());
        }
    }
}
