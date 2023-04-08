using BackupPro.Dominio;
using BackupPro.Repositorio;
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
    public partial class frmOrigem : Form
    {
        public frmOrigem()
        {
            InitializeComponent();
        }              

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ofdLocalizaArquivo.ShowDialog();
            
            Origem origem = new Origem()
            {
                CaminhoArquivo = ofdLocalizaArquivo.FileName,
                NomeArquivo = ofdLocalizaArquivo.SafeFileName,
                DiretorioArquivo = ofdLocalizaArquivo.FileName.Replace(ofdLocalizaArquivo.SafeFileName, ""),
                NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(ofdLocalizaArquivo.FileName),
                ExtensaoArquivo = Path.GetExtension(ofdLocalizaArquivo.SafeFileName),
                ListaNomeArquivos = new List<string>(ofdLocalizaArquivo.SafeFileNames.ToList())
            };
            FileInfo fileInfo = new FileInfo(origem.CaminhoArquivo);
            origem.TamanhoArquivo = (int)fileInfo.Length;
            for (int i = 0; i < origem.ListaNomeArquivos.Count; i++)
            {
                lbxArquivos.Items.Add(origem.ListaNomeArquivos[i]);                
            }            
            List<Origem> listaArquivos = new List<Origem>();           
            listaArquivos.Add(origem);
            RepositorioBackupBlocoNotas.EscreverOrigem(listaArquivos);
            CarregarListaArquivos();
        }

        private void CarregarListaArquivos()
        {
            lbxArquivos.Items.Clear();
            lbxArquivos.Items.AddRange(RepositorioBackupBlocoNotas.LerOrigem().ToArray());
        }

        private void frmOrigem_Shown(object sender, EventArgs e)
        {
            CarregarListaArquivos();
            
        }
    }
}
