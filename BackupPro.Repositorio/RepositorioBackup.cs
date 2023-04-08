using BackupPro.DAO;
using BackupPro.Dominio;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackupPro.Repositorio
{
    public class RepositorioBackup : IRepositorio
    {
        private static int progresso;
        private static string salvoComo;
        private static string labelEscrevendo;
        private static int arquivosParaZipar;
        private static int arquivosZipados;
        private static double tranferido;
        private static int totalTransferido;
        private static int totalDestinos;
        private static int totalOrigens;
        static Origem origAtualizado;

        public string Copiar()
        {
            try
            {
                FazerBackup(false);
                return "Copiado";
            }
            catch (Exception ex)
            {
                return "Falha ao Copiar, verifique o Log abaixo \n" + ex.Message;
            }
        }

        public string Zipar()
        {
            FazerBackup(false);
            return "Zipado com Sucesso";
        }

        public string UploadFTP()
        {
            FazerBackup(false);
            return "Enviado com Sucesso";
        }

        public void FazerBackup(bool isBackuground)
        {
            DateTime dataHora = DateTime.Now;
            string dataFormatada = dataHora.ToString("dd-MM-yyyy HH:mm:ss");

            OrigemDAO origemDao = new OrigemDAO();
            DbDataReader dro = origemDao.GetOrigens();
            List<Origem> origens = new List<Origem>();
            while (dro.Read())
            {
                origens.Add(new Origem
                {
                    Id = Convert.ToInt32(dro["Id"]),
                    CaminhoArquivo = dro["CaminhoArquivo"].ToString(),
                    NomeTarefa = dro["NomeTarefa"].ToString(),
                    NomeArquivoSemExtensao = dro["NomeArquivoSemExtensao"].ToString(),
                    ExtensaoArquivo = dro["ExtensaoArquivo"].ToString(),
                    HorarioTarefa = Convert.ToDateTime(dro["HorarioTarefa"]),
                    Zipar = Convert.ToBoolean(dro["Zipar"]),
                    Nuvem = Convert.ToBoolean(dro["Nuvem"])
                });
            }

            DestinoDAO destinoDao = new DestinoDAO();
            DbDataReader drd = destinoDao.GetDestinos();
            List<Destino> destinos = new List<Destino>();
            while (drd.Read())
            {
                destinos.Add(new Destino
                {
                    Id = Convert.ToInt32(drd["Id"]),
                    CaminhoDiretorio = drd["CaminhoDiretorio"].ToString()
                });
            }
            if (!drd.Read() && destinos.Count == 0)
                destinos.Add(new Destino { CaminhoDiretorio = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\BackupPro" });

            ConfiguracoesDAO configDao = new ConfiguracoesDAO();
            DbDataReader drc = configDao.GetConfiguracoes();
            List<Configuracoes> configs = new List<Configuracoes>();
            while (drc.Read())
            {
                configs.Add(new Configuracoes
                {
                    Id = Convert.ToInt32(drc["Id"]),
                    FTP = drc["FTP"].ToString(),
                    User = drc["User"].ToString(),
                    Pass = drc["Pass"].ToString(),
                    Porta = Convert.ToInt32(drc["Porta"])
                });
            }

            int hora = 00;
            int minuto = 00;
            int segundo = 00;

            foreach (Origem origem in origens)
            {
                hora = Convert.ToInt32(origem.HorarioTarefa.ToString("HH"));
                minuto = Convert.ToInt32(origem.HorarioTarefa.ToString("mm"));
                segundo = Convert.ToInt32(origem.HorarioTarefa.ToString("ss"));
            }

            if (isBackuground)
                VerificaHorario(hora, minuto, segundo);

            foreach (Destino destino in destinos)
            {
                foreach (Origem arquivo in origens)
                {
                    arquivo.UltimoBackup = Convert.ToDateTime(dataFormatada);
                    if (arquivo.Zipar == true)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                            zip.SaveProgress += SaveProgress;
                            foreach (Origem listaOrigens in origens)
                            {
                                totalOrigens = origens.Count;
                                if (listaOrigens.ExtensaoArquivo == "")
                                {
                                    if (Directory.Exists(listaOrigens.CaminhoArquivo))
                                        zip.AddDirectory(listaOrigens.CaminhoArquivo);
                                }
                                if (listaOrigens.ExtensaoArquivo != "")
                                {
                                    if (File.Exists(listaOrigens.CaminhoArquivo))
                                        zip.AddFile(listaOrigens.CaminhoArquivo);
                                }
                            }
                            foreach (Destino listaDestinos in destinos)
                            {
                                totalDestinos = destinos.Count;
                                CreateDirectory(listaDestinos.CaminhoDiretorio);

                                string SalvarComo = listaDestinos.CaminhoDiretorio + "\\" + arquivo.NomeTarefa + dataHora.ToString(" (ddMMyyyy HHmm)") + ".zip";
                                arquivo.SalvoComo = SalvarComo;

                                zip.Save(SalvarComo);
                                //atualizar data
                                origAtualizado = new Origem();
                                origAtualizado.NomeTarefa = arquivo.NomeTarefa;
                                origAtualizado.UltimoBackup = arquivo.UltimoBackup;
                                origAtualizado.SalvoComo = arquivo.SalvoComo;
                                origAtualizado.Id = arquivo.Id;
                            }
                            progresso = 0;
                        }
                        if (isBackuground)
                        {
                            if (arquivo.Nuvem)
                            {
                                foreach (Configuracoes config in configs)
                                    UploadFile(arquivo.SalvoComo, config.FTP, config.User, config.Pass, config.Porta);
                            }
                        }
                        return;
                    }
                    else if (arquivo.Zipar == false)
                    {
                        string SalvarComo = destino.CaminhoDiretorio + "\\" + arquivo.NomeArquivoSemExtensao + " " + dataHora.ToString("(ddMMyyyy HHmm)") + arquivo.ExtensaoArquivo;
                        CreateDirectory(destino.CaminhoDiretorio);
                        arquivo.SalvoComo = SalvarComo;
                        salvoComo = SalvarComo;

                        if (arquivo.ExtensaoArquivo != "")
                            File.Copy(arquivo.CaminhoArquivo, SalvarComo, true);
                        else
                            DirectoryCopy(arquivo.CaminhoArquivo, SalvarComo, true);

                        //atualizar data
                        origAtualizado = new Origem();
                        origAtualizado.NomeTarefa = arquivo.NomeTarefa;
                        origAtualizado.UltimoBackup = arquivo.UltimoBackup;
                        origAtualizado.SalvoComo = arquivo.SalvoComo;
                        origAtualizado.Id = arquivo.Id;

                        if (isBackuground)
                        {
                            if (arquivo.Nuvem)
                            {
                                foreach (Configuracoes config in configs)
                                    UploadFile(arquivo.SalvoComo, config.FTP, config.User, config.Pass, config.Porta);
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_Started)
            {
                salvoComo = e.ArchiveName;
            }
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                labelEscrevendo = "Compactando: " + e.CurrentEntry.FileName + " (" + (e.EntriesSaved + 1) + "/" + e.EntriesTotal + ")";
                arquivosParaZipar = e.EntriesTotal;
                arquivosZipados = e.EntriesSaved + 1;
                totalTransferido = progresso + 1;
            }
            else if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                tranferido = ((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
                progresso = (int)((tranferido / totalOrigens) / totalDestinos) + totalTransferido;
            }
        }

        public static int GetProgresso()
        {
            return progresso;
        }

        public static string GetSalvoComo()
        {
            return salvoComo;
        }

        public static string GetLabelEscrevendo()
        {
            return labelEscrevendo;
        }

        private static void VerificaHorario(int hora, int minuto, int segundo)
        {
            bool iniciar = false;
            while (iniciar == false)
            {
                if (DateTime.Now.Hour == hora)
                {
                    if (DateTime.Now.Minute == minuto)
                    {
                        iniciar = true;
                        return;
                    }
                    Thread.Sleep(60000);
                }
                else if (DateTime.Now.Hour == hora - 1)
                    Thread.Sleep(60000);

                else
                    Thread.Sleep(3600000);
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the file contents of the directory to copy.
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                // Create the path to the new copy of the file.
                var temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, true);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (!copySubDirs) return;

            foreach (var subdir in dirs)
            {
                // Create the subdirectory.
                var temppath = Path.Combine(destDirName, subdir.Name);

                // Copy the subdirectories.
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }

        private static void CreateDirectory(string destDirName)
        {
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
        }

        #region UploadFTP
        //private static void Upload(string localFile, string host, string user, string pass, int port)
        //{
        //    FtpWebRequest ftpRequest = null;
        //    Stream ftpStream = null;
        //    int bufferSize = 2048;

        //    try
        //    {
        //        /* Create an FTP Request */
        //        ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + Path.GetFileName(localFile));
        //        /* Log in to the FTP Server with the User Name and Password Provided */
        //        ftpRequest.Credentials = new NetworkCredential(user, pass);
        //        /* When in doubt, use these options */
        //        ftpRequest.UseBinary = true;
        //        ftpRequest.UsePassive = true;
        //        ftpRequest.KeepAlive = true;
        //        /* Specify the Type of FTP Request */
        //        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
        //        /* Establish Return Communication with the FTP Server */
        //        ftpStream = ftpRequest.GetRequestStream();
        //        /* Open a File Stream to Read the File for UploadFTP */
        //        FileStream localFileStream = new FileStream(localFile, FileMode.Create);
        //        /* Buffer for the Downloaded Data */
        //        byte[] byteBuffer = new byte[bufferSize];
        //        int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
        //        /* UploadFTP the File by Sending the Buffered Data Until the Transfer is Complete */
        //        try
        //        {
        //            while (bytesSent != 0)
        //            {
        //                ftpStream.Write(byteBuffer, 0, bytesSent);
        //                bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
        //            }
        //        }
        //        catch (Exception ex) { ex.Message.ToString(); }
        //        /* Resource Cleanup */
        //        localFileStream.Close();
        //        ftpStream.Close();
        //        ftpRequest = null;
        //    }
        //    catch (Exception ex) { ex.Message.ToString(); }
        //    return;
        //}

        public void UploadFile(string localFile, string ftp, string user, string pass, int port)
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
        }
        #endregion

        public static bool VerificaDestino()
        {
            DestinoDAO destinoDao = new DestinoDAO();
            DbDataReader dr = destinoDao.GetDestinos();
            if (dr.Read())
            {
                return true;
            }
            return false;
        }

        public static bool VerificaConfigFTP()
        {
            ConfiguracoesDAO configDao = new ConfiguracoesDAO();
            DbDataReader dr = configDao.GetConfiguracoes();
            if (dr.Read())
            {
                return true;
            }
            return false;
        }

        public void AtualizarDUBackup()
        {
            OrigemDAO origDao = new OrigemDAO();
            origDao.AtualizarDataBackup(origAtualizado);
        }
    }
}
