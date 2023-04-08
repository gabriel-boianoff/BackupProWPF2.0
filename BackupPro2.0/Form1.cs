using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BackupPro2._0
{
    public delegate void Atualiza(bool b);

    public partial class frmHome : Form
    {
        //Thread Tarefa;

        public frmHome()
        {
            InitializeComponent();

        }


        String ArquivoOrigem;
        String PastaOrigem;
        String CaminhoOrigem;
        String PastaDestino;
        String CaminhoDestino;
        String ArqSemExt;
        String ExtArq;

        String Ftp;
        String User;
        String Pass;
        int SizeOrig;
        //int SizeDest;


        private void btnOrigem_Click(object sender, EventArgs e)
        {

            BuscaArquivo.ShowDialog();
            CaminhoOrigem = BuscaArquivo.FileName;
            ArquivoOrigem = BuscaArquivo.SafeFileName;
            PastaOrigem = CaminhoOrigem.Replace(BuscaArquivo.SafeFileName, "");
            txtOrigem.Text = CaminhoOrigem;
            ArqSemExt = Path.GetFileNameWithoutExtension(CaminhoOrigem);
            ExtArq = Path.GetExtension(ArquivoOrigem);
        }

        private void btnDestino_Click(object sender, EventArgs e)
        {

            BuscaDiretorio.ShowDialog();
            PastaDestino = BuscaDiretorio.SelectedPath;
            txtDestino.Text = PastaDestino;

        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (File.Exists(CaminhoOrigem))
            {
                if (txtDestino.Text != "")
                {
                    AtivaBackup();

                }
                else
                {
                    MessageBox.Show("Destino Invalido Verifique!!");
                }
            }
            else
            {
                MessageBox.Show("Arquivo Selecionado nao encontrado. \nVerfique!!");
            }
        }



        public void AtivaBackup()
        {
            DateTime dataHora = DateTime.Now;
            String DataFormatada = dataHora.ToString("ddMMyyyy HHmm");
            PastaDestino = txtDestino.Text;
            CaminhoOrigem = txtOrigem.Text;

            if (!Directory.Exists(PastaDestino))
            {
                try
                {
                    Directory.CreateDirectory(PastaDestino);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nao foi possivel selecionar/criar a pasta de destino.\nVerifique!! (" + ex.Message + ")");
                    return;
                }
            }
            if (File.Exists(CaminhoOrigem))
            {
                CaminhoDestino = Path.Combine(PastaDestino, ArqSemExt + " " + DataFormatada + ExtArq);
                if (chkNormal.Checked == true)
                {
                    try
                    {
                        FileInfo fileinfo = new FileInfo(CaminhoOrigem);
                        SizeOrig = (int)fileinfo.Length;

                        File.Copy(CaminhoOrigem, CaminhoDestino, true);
                        MessageBox.Show("Backup Realizado com Sucesso!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Falha ao realizar backup, verifique o caminho/destino e o Log abaixo!\n\n" + ex.Message);
                        return;
                    }

                }
                else
                {
                    try
                    {
                        using (ZipFile zip = new ZipFile())
                        {

                            //Parallel.Invoke(() =>
                            //{

                            //});
                            CaminhoDestino = PastaDestino + "\\" + ArqSemExt + " " + DataFormatada + ".zip";

                            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                            zip.SaveProgress += SaveProgress;

                            zip.StatusMessageTextWriter = System.Console.Out;
                            zip.AddFile(CaminhoOrigem, "");
                            zip.Save(CaminhoDestino);

                            MessageBox.Show("Backup Realizado com Sucesso!!");


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Falha ao compactar ao compactar arquivo.\nVerifique o Log abaixo.\n\n" + ex.Message);
                        return;
                    }

                }

            }
            else
            {
                MessageBox.Show("Arquivo Selecionado nao encontrado!! \nVerfique");
            }


        }

        public void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                int progresso = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);

                pbrProgresso.Maximum = 100;
                pbrProgresso.Value = progresso;
                lblLoad.Text = progresso.ToString();

            }

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            tabConfig.Visible = false;
            tabConfig.SendToBack();
        }

        private void lblConfig_Click(object sender, EventArgs e)
        {
            tabConfig.Visible = true;
            //guiConfig.Size = new Size(265, 180);
            tabConfig.Size = new Size(265, 195);
            tabConfig.BringToFront();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {


            //Ftp = txtFtp.Text;
            //User = txtUser.Text;
            //Pass = txtPass.Text;
            //Ftp = "ftp://201.24.155.2/Amazonia/Tecnicos/Gabriel_PVA/Docs/";
            //User = "suporte";
            //Pass = "dog#@";
            Ftp = "ftp://backuppro.brickftp.com/Arquivos/";
            User = "gabrielboiano@hotmail.com";
            Pass = "gabriel9";

            txtFtp.Text = Ftp;
            txtUser.Text = User;
            txtPass.Text = Pass;
        }

        private void btnFTP_Click(object sender, EventArgs e)
        {
            if (File.Exists(CaminhoDestino))
            {
                Upload();
            }
            else
            {
                MessageBox.Show("Primeiro faça o Backup!");
            }

        }

        public void Upload()
        {
            pbxLoad.Visible = true;
            UploadFile(Ftp, CaminhoDestino, User, Pass);
            timer1.Interval = 10;


        }

        public void UploadFile(string ftp, string localFile, string user, string pass)
        {
            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(ftp + "/" + Path.GetFileName(localFile));
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential(user, pass);
            req.UsePassive = true;
            req.UseBinary = true;
            req.KeepAlive = false;
            FileStream stream = File.OpenRead(localFile);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            Stream reqstrm = req.GetRequestStream();
            reqstrm.Write(buffer, 0, buffer.Length);
            reqstrm.Close();
            MessageBox.Show("Enviado com Sucesso!!");

        }
        int inicio = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            inicio++;
            if (inicio >= 100)
            {
                timer1.Stop();
                pbxLoad.Visible = false;
            }
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            // Tarefa = new Thread(new ThreadStart(xxx));
        }






        private void pbxLoad_Click(object sender, EventArgs e)
        {

        }
    }
}
