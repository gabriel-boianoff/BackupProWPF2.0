using BackupPro.Dominio;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPro.Repositorio
{
    public class RepositorioBackupBlocoNotas : IRepositorio
    {
        private static string EnderecoOrigem = AppDomain.CurrentDomain.BaseDirectory + "ArquivosBackup.txt";
        private static string EnderecoDestino = AppDomain.CurrentDomain.BaseDirectory + "DestinoBackup.txt";
        private static int progresso;

        public string Copiar()
        {
            string acao = "COPIAR";
            FazerBackup(acao);
            return "Copiado com Sucesso";
        }

        public string Zipar()
        {
            string acao = "ZIPAR";
            FazerBackup(acao);
            return "Zipado com Sucesso";
        }

        public string UploadFTP()
        {
            throw new NotImplementedException();
        }

        public static List<Origem> LerOrigem()
        {
            List<Origem> listaArquivos = new List<Origem>();
            if (File.Exists(EnderecoOrigem))
            {
                using (StreamReader sr = File.OpenText(EnderecoOrigem))
                {
                    while (sr.Peek() >= 0) //verifica se existe algo pra ler, quando nao existir o peek retorna -1
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';');
                        if (linhaComSplit.Count() == 6)
                        {
                            Origem origem = new Origem();
                            origem.CaminhoArquivo = linhaComSplit[0];
                            origem.NomeArquivo = linhaComSplit[1];
                            origem.DiretorioArquivo = linhaComSplit[2];
                            origem.NomeArquivoSemExtensao = linhaComSplit[3];
                            origem.ExtensaoArquivo = linhaComSplit[4];
                            origem.TamanhoArquivo = Convert.ToDouble(linhaComSplit[5]);
                            listaArquivos.Add(origem);
                        }
                    }
                }
            }
            return listaArquivos;
        }

        public static void EscreverOrigem(List<Origem> listaArquivos)
        {
            using (StreamWriter sw = new StreamWriter(EnderecoOrigem, true))
            {
                foreach (Origem origem in listaArquivos)
                {
                    string linha = string.Format("{0};{1};{2};{3};{4};{5}", origem.CaminhoArquivo, origem.NomeArquivo, origem.DiretorioArquivo,
                        origem.NomeArquivoSemExtensao, origem.ExtensaoArquivo, origem.TamanhoArquivo);
                    sw.WriteLine(linha);
                }
                sw.Flush();
            }
        }

        public static void EscreverDestino(List<Destino> listaDestinos)
        {
            using (StreamWriter sw = new StreamWriter(EnderecoDestino, true))
            {
                foreach (Destino destino in listaDestinos)
                {
                    string linha = string.Format("{0};{1};{2};{3};{4}", destino.CaminhoDiretorio, destino.NomeDiretorio, destino.Unidade,
                        destino.EspacoDisponivel, destino.EspacoTotal);
                    sw.WriteLine(linha);
                }
                sw.Flush();
            }
        }

        public static List<Destino> LerDestino()
        {
            List<Destino> listaDestinos = new List<Destino>();
            if (File.Exists(EnderecoDestino))
            {
                using (StreamReader sr = File.OpenText(EnderecoDestino))
                {
                    while (sr.Peek() >= 0) //verifica se existe algo pra ler, quando nao existir o peek retorna -1
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';');
                        if (linhaComSplit.Count() == 5)
                        {
                            Destino destino = new Destino();
                            destino.CaminhoDiretorio = linhaComSplit[0];
                            destino.NomeDiretorio = linhaComSplit[1];
                            destino.Unidade = linhaComSplit[2];
                            destino.EspacoDisponivel = Convert.ToDouble(linhaComSplit[3]);
                            destino.EspacoTotal = Convert.ToDouble(linhaComSplit[4]);
                            listaDestinos.Add(destino);
                        }
                    }
                }
            }
            return listaDestinos;
        }

        public void FazerBackup(string acao)
        {
            DateTime dataHora = DateTime.Now;
            string DataFormatada = dataHora.ToString("ddMMyyyy HHmm");
            Origem origem = new Origem();
            Destino destino = new Destino();
            using (StreamReader sr = File.OpenText(EnderecoOrigem))
            {
                while (sr.Peek() >= 0)
                {
                    string linha = sr.ReadLine();
                    string[] linhaComSplit = linha.Split(';');
                    if (linhaComSplit.Count() == 6)
                    {
                        origem.CaminhoArquivo = linhaComSplit[0];
                        origem.NomeArquivo = linhaComSplit[1];
                        origem.DiretorioArquivo = linhaComSplit[2];
                        origem.NomeArquivoSemExtensao = linhaComSplit[3];
                        origem.ExtensaoArquivo = linhaComSplit[4];
                        origem.TamanhoArquivo = Convert.ToDouble(linhaComSplit[5]);
                    }
                }
            }
            using (StreamReader sr = File.OpenText(EnderecoDestino))
            {
                while (sr.Peek() >= 0)
                {
                    string linha = sr.ReadLine();
                    destino.CaminhoDiretorio = linha;
                }
            }
            string CaminhoDestino = destino.CaminhoDiretorio + "\\" + origem.NomeArquivo;
            {
                if (acao == "COPIAR")
                {
                    File.Copy(origem.CaminhoArquivo, CaminhoDestino, true); ;
                }
                else if (acao == "ZIPAR")
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        CaminhoDestino = destino.CaminhoDiretorio + "\\" + origem.NomeArquivoSemExtensao + " " + DataFormatada + ".zip";
                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                        zip.SaveProgress += SaveProgress;
                        zip.AddFile(origem.CaminhoArquivo, "");
                        zip.Save(CaminhoDestino);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                progresso = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
            }
        }

        public static int GetProgresso()
        {
            return progresso;
        }
               
    }
}
