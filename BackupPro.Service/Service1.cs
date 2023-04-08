using BackupPro.Repositorio;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace BackupPro.Service
{
    public partial class Service1 : ServiceBase
    {
        //StreamWriter arquivoLog;
        //Thread _ThreadVerificacao;
        Timer timer1;

        private static string EnderecoLog = new Uri("Logs/Log de eventos.txt", UriKind.Relative).ToString();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            /*
            //Instancie a variável criada, que receberá como parâmetro o caminho de meu arquivo de texto,
            //que será o log destes eventos do meu serviço, e o parâmetro encoding com o valor true.
            arquivoLog = new StreamWriter(EnderecoLog, true);

            //Escrevo no arquivo texto no momento que o arquivo for iniciado
            arquivoLog.WriteLine("Serviço iniciado em: " + DateTime.Now);

            //Limpo o buffer com o método Flush
            arquivoLog.Flush();
            
            //criação da thread de verificação e sua execução
            _ThreadVerificacao = new Thread(VerificarHorario);
            _ThreadVerificacao.Start();
            */
            timer1 = new Timer(new TimerCallback(timer1_Tick), null, 15000, 60000);
        }

        //irá verificar se deve ou não executar o método a cada 1 hora
        protected void VerificarHorario()
        {
            while (true)
            {
                if (DateTime.Now.Hour == 10) //Se for 10 horas da manhã
                {
                    RepositorioBackup repositorio = new RepositorioBackup();
                    repositorio.Zipar();
                    
                }
                Thread.Sleep(3600000); //3.600.000 milisegundos equivalem a 1 hora
            }
        }

        

        protected override void OnStop()
        {
            /*
            //Escrevo no arquivo texto no momento exato que o arquivo for encerrado
            arquivoLog.WriteLine("Serviço encerrado em: " + DateTime.Now);

            //Fecho o arquivo com o método Close
            arquivoLog.Close();

            //paramos a thread quando o serviço for parado
            _ThreadVerificacao.Abort();
            */

            StreamWriter vWriter = new StreamWriter(EnderecoLog, true);

            vWriter.WriteLine("Servico Parado: " + DateTime.Now.ToString());
            vWriter.Flush();
            vWriter.Close();
        }

        private void timer1_Tick(object sender)
        {
            StreamWriter vWriter = new StreamWriter(@"c:\testeServico.txt", true);
            vWriter.WriteLine("Servico Rodando: " + DateTime.Now.ToString());
            vWriter.Flush();
            vWriter.Close();
        }
    }
}
