using BackupPro.DAO;
using BackupPro.Dominio;
using BackupPro.Repositorio;
using BackupProWPF.ViewModels;
using OpenDotNetFtpLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BackupProWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int progresso;
        private static int indiceSelecionado;
        private static string VerificarCaminho = string.Empty;
        TarefaViewModel tarefaInicial;
        ConfiguracoesViewModel configInicial;
        Thread _ThreadSegundoPlano;
        NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            //MinimizarParaBandeja();            
        }

        private void wdwBackupPro_Loaded(object sender, RoutedEventArgs e)
        {
            OrigemDAO origemDao = new OrigemDAO();
            origemDao.CriarTabelaOrigens();
            CarregarGridViewOrigem();
            DestinoDAO destinoDao = new DestinoDAO();
            destinoDao.CriarTabelaDestinos();
            CarregarGridViewDestino();
            ConfiguracoesDAO configDao = new ConfiguracoesDAO();
            configDao.CriarTabelaConfiguracoes();
            CarregarConfiguracoes();

            _ThreadSegundoPlano = new Thread(SegundoPlano);
            _ThreadSegundoPlano.Start();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void imgFechar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            wdwBackupPro.Hide();
        }

        private void imgMinimizar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            wdwBackupPro.WindowState = WindowState.Minimized;
        }

        private void grdHome_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarTelaInicial();
            CarregarGridViewOrigem();
        }

        private void CarregarTelaInicial()
        {
            btnHome.IsEnabled = true;
            imgStatus.Visibility = Visibility.Visible;
            progressBar1.Visibility = Visibility.Hidden;
            progressBar1.Value = 0;
            stackPanelProgress.Visibility = Visibility.Hidden;

            Origem origem = new Origem();
            string DUBackup = string.Empty;
            OrigemDAO origemDao = new OrigemDAO();
            DbDataReader dr = origemDao.GetUltimoBackup();
            while (dr.Read())
            {
                if (dr["UltimoBackup"].ToString() != string.Empty)
                    origem.UltimoBackup = Convert.ToDateTime(dr["UltimoBackup"]);
                DUBackup = origem.UltimoBackup.ToString("dd/MM/yyyy AS HH:mm").ToLower();
            }

            if (DUBackup != string.Empty)
            {
                if (DUBackup == "01/01/0001 as 00:00")
                {
                    BitmapImage img = new BitmapImage(new Uri("Resources/Atencao.png", UriKind.Relative));
                    imgStatus.Source = img;
                    lblStatus.Foreground = System.Windows.Media.Brushes.Orange;
                    lblStatus.Text = "Atenção, o backup nao esta sendo realizado a muito tempo!";
                    btnHome.Content = "Fazer Backup Agora";
                }
                else
                {
                    BitmapImage img = new BitmapImage(new Uri("Resources/Sucesso.png", UriKind.Relative));
                    imgStatus.Source = img;
                    lblStatus.Foreground = System.Windows.Media.Brushes.DarkGreen;
                    lblStatus.Text = "Muito Bem, o backup foi realizado pela ultima vez em " + DUBackup;
                    btnHome.Content = "Fazer Backup Agora";
                }
            }
            else
            {
                BitmapImage img = new BitmapImage(new Uri("Resources/Falha.png", UriKind.Relative));
                imgStatus.Source = img;
                lblStatus.Foreground = System.Windows.Media.Brushes.DarkRed;
                lblStatus.Text = "Ops! Parece que o Backup não esta configurado.";
                btnHome.Content = "Configurar Backup Agora";
            }
        }

        private void grdArquivos_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarGridViewOrigem();
            HabilitarDesabilitarCamposArquivos();
        }

        private void grdDestinos_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarGridViewDestino();
        }

        private void grdConfiguracoes_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarConfiguracoes();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (btnHome.Content.ToString() == "Configurar Backup Agora")
            {
                tabArquivos.IsSelected = true;
            }
            else {
                if (RepositorioBackup.VerificaDestino())
                {
                    if (tarefaInicial.Nuvem)
                    {
                        if (RepositorioBackup.VerificaConfigFTP())
                            ExecutarBackup();
                        else {
                            var result = System.Windows.MessageBox.Show("Configure o FTP antes de continuar. \nClique em OK para abrir as configurações.", "Atenção", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                            if (result == MessageBoxResult.OK)
                                tabConfiguracoes.IsSelected = true;
                            else
                                return;
                        }
                    }
                    else
                        ExecutarBackup();
                }
                else {
                    string destPadrao = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\BackupPro";
                    var result = System.Windows.MessageBox.Show("Nenhum destino informado, o backup será salvo na pasta padrão em \n"
                        + destPadrao, "Atenção", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        InserirDestino(destPadrao);
                        ExecutarBackup();
                    }
                    else if (result == MessageBoxResult.No)
                        tabDestinos.IsSelected = true;
                    else
                        return;
                }
            }
        }

        private void ExecutarBackup()
        {
            btnHome.IsEnabled = false;
            progressBar1.Visibility = Visibility.Visible;
            imgStatus.Visibility = Visibility.Hidden;
            lblStatus.Foreground = System.Windows.Media.Brushes.DarkGreen;

            // Faz o backup
            Task taskBkp = Task.Run(() =>
            {
                IRepositorio repositorio = new RepositorioBackup();
                if (tarefaInicial.Zipar)
                    repositorio.Zipar();
                else {
                    lblStatus.Dispatcher.Invoke(() => lblStatus.Text = "Copiando..   ", DispatcherPriority.Normal);
                    var result = repositorio.Copiar();
                    if (result == "Copiado")
                        lblStatus.Dispatcher.Invoke(() => lblStatus.Text = "Copiado com Sucesso", DispatcherPriority.Normal);
                    else
                        lblStatus.Dispatcher.Invoke(() => lblStatus.Text = result, DispatcherPriority.Normal);
                }
            });

            //Carrega a Barra de progresso
            if (tarefaInicial.Zipar == true)
            {
                Task.Run(() =>
                {
                    progresso = 0;
                    while (progresso < 100)
                    {
                        progresso = RepositorioBackup.GetProgresso();
                        if (tarefaInicial.Nuvem)
                            progresso /= 2;
                        progressBar1.Dispatcher.Invoke(() => progressBar1.Value = progresso, DispatcherPriority.Normal);
                        lblStatus.Dispatcher.Invoke(() => lblStatus.Text = "Zipando..   " + progresso + "%", DispatcherPriority.Normal);
                        if (tarefaInicial.Nuvem)
                            progresso *= 2;
                    }
                    lblStatus.Dispatcher.Invoke(() => lblStatus.Text = "Zipado com sucesso", DispatcherPriority.Normal);
                });
            }

            //Faz o Upload pro Ftp, somente quando o backup Concluir
            Task taskFtp = taskBkp.ContinueWith((task) =>
            {
                if (tarefaInicial.Nuvem)
                {
                    if (RepositorioBackup.VerificaConfigFTP())
                    {
                        ParametrosFtp(configInicial.FTP, configInicial.User, configInicial.Pass, configInicial.Porta, true);
                        if (AtivarUploadFtp())
                        {
                            progressBar1.Dispatcher.Invoke(() => progressBar1.Visibility = Visibility.Visible, DispatcherPriority.Normal);
                            stackPanelProgress.Dispatcher.Invoke(() => stackPanelProgress.Visibility = Visibility.Visible, DispatcherPriority.Normal);

                            Upload();
                        }
                        else {
                            System.Windows.MessageBox.Show("Problemas na configuração do ftp, verifique");
                        }
                    }
                }
            });
            TaskAwaiter awaiter = taskFtp.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                RepositorioBackup repBkp = new RepositorioBackup();
                repBkp.AtualizarDUBackup();
                CarregarTelaInicial();
            });
        }

        private void btnSelecionarArquivo_Click(object sender, RoutedEventArgs e)
        {
            OrigemDAO origemDao = new OrigemDAO();
            OpenFileDialog ofdSelecionarArquivo = new OpenFileDialog();
            var resultado = ofdSelecionarArquivo.ShowDialog();
            if (resultado == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo arquivoSelecionado = new FileInfo(ofdSelecionarArquivo.FileName);
                if (VerificarCaminho != arquivoSelecionado.FullName)
                {
                    VerificarCaminho = arquivoSelecionado.FullName;
                    double tamanhoArquivo = arquivoSelecionado.Length;
                    Origem novaOrigemArquivo = new Origem()
                    {
                        CaminhoArquivo = arquivoSelecionado.FullName,
                        NomeArquivo = arquivoSelecionado.Name,
                        DiretorioArquivo = arquivoSelecionado.DirectoryName,
                        NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(arquivoSelecionado.FullName),
                        ExtensaoArquivo = arquivoSelecionado.Extension,
                        TamanhoArquivo = Math.Round(tamanhoArquivo / 1024 / 1024, 2),
                        //salva tarefa
                        NomeTarefa = txtNome.Text,
                        TipoTarefa = cmbTipo.Text,
                        HorarioTarefa = Convert.ToDateTime(txtHorario.Text),
                        Zipar = Convert.ToBoolean(cmbZipar.SelectedIndex),
                        Nuvem = Convert.ToBoolean(cmbNuvem.SelectedIndex)
                    };
                    origemDao.Inserir(novaOrigemArquivo);   //salva no banco de dados
                    CarregarGridViewOrigem();
                    AtualizarTarefa();
                    HabilitarDesabilitarCamposArquivos();
                    //List<Origem> listaArquivos = new List<Origem>();
                    //listaArquivos.Add(novaOrigemArquivo);
                    //RepositorioBackupBlocoNotas.EscreverOrigem(listaArquivos); //salva no bloco de notas
                }
            }
        }

        private void CarregarGridViewOrigem()
        {
            //List<Origem> origens = RepositorioBackup.LerOrigem().ToList(); // Lê o bloco de notas
            //Convertendo o DataReader Para List
            OrigemDAO origemDao = new OrigemDAO();
            DbDataReader dr = origemDao.GetOrigens();
            List<Origem> origens = new List<Origem>();
            while (dr.Read())
            {
                origens.Add(new Origem
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    CaminhoArquivo = dr["CaminhoArquivo"].ToString(),
                    NomeArquivoSemExtensao = dr["NomeArquivoSemExtensao"].ToString(),
                    ExtensaoArquivo = dr["ExtensaoArquivo"].ToString(),
                    TamanhoArquivo = Convert.ToDouble(dr["TamanhoArquivo"]),
                    NomeTarefa = dr["NomeTarefa"].ToString(),
                    HorarioTarefa = Convert.ToDateTime(dr["HorarioTarefa"]),
                    TipoTarefa = dr["TipoTarefa"].ToString(),
                    Zipar = Convert.ToBoolean(dr["Zipar"]),
                    Nuvem = Convert.ToBoolean(dr["Nuvem"])
                });
            }
            //fim da conversao
            List<OrigemViewModel> origemViewModels = new List<OrigemViewModel>();
            foreach (Origem origem in origens)
            {
                string tipo = " MB";
                if (origem.TamanhoArquivo > 1024) { origem.TamanhoArquivo /= 1024; tipo = " GB"; }
                else if (origem.TamanhoArquivo < 1) { origem.TamanhoArquivo *= 1024; tipo = " KB"; }
                OrigemViewModel viewModel = new OrigemViewModel
                {
                    Id = origem.Id,
                    Caminho = origem.CaminhoArquivo,
                    Tamanho = Math.Round(origem.TamanhoArquivo, 2) + tipo,
                    Nome = origem.NomeArquivoSemExtensao,
                    Extensao = origem.ExtensaoArquivo
                };
                origemViewModels.Add(viewModel);
            }
            lvwArquivos.ItemsSource = origemViewModels;


            foreach (Origem tarefa in origens)
            {
                tarefaInicial = new TarefaViewModel
                {
                    NomeTarefa = tarefa.NomeTarefa,
                    TipoTarefa = tarefa.TipoTarefa,
                    HorarioTarefa = tarefa.HorarioTarefa,
                    Zipar = tarefa.Zipar,
                    Nuvem = tarefa.Nuvem
                };
                txtNome.Text = tarefaInicial.NomeTarefa;
                cmbTipo.Text = tarefaInicial.TipoTarefa;
                txtHorario.Text = tarefaInicial.HorarioTarefa.ToString("HH:mm:ss");
                cmbZipar.SelectedIndex = Convert.ToInt32(tarefaInicial.Zipar);
                cmbNuvem.SelectedIndex = Convert.ToInt32(tarefaInicial.Nuvem);
            }

        }

        private void btnSelecionarPastaDestino_Click(object sender, RoutedEventArgs e)
        {

            FolderBrowserDialog fbdSelecionarDestino = new FolderBrowserDialog();
            var resultado = fbdSelecionarDestino.ShowDialog();
            if (resultado == System.Windows.Forms.DialogResult.OK)
            {
                InserirDestino(fbdSelecionarDestino.SelectedPath);
            }
        }

        private void InserirDestino(string destino)
        {
            DestinoDAO destinoDao = new DestinoDAO();
            DirectoryInfo destinoSelecionado = new DirectoryInfo(destino);
            if (VerificarCaminho != destinoSelecionado.FullName)
            {
                VerificarCaminho = destinoSelecionado.FullName;
                DriveInfo unidadeSelecionada = new DriveInfo(destinoSelecionado.Root.Name);
                double espacoDisponivel = unidadeSelecionada.TotalFreeSpace;
                double espacoTotal = unidadeSelecionada.TotalSize;
                Destino novoDestino = new Destino
                {
                    CaminhoDiretorio = destinoSelecionado.FullName,
                    NomeDiretorio = destinoSelecionado.Name,
                    Unidade = destinoSelecionado.Root.Name,
                    EspacoDisponivel = Math.Round(espacoDisponivel / 1024 / 1024 / 1024, 2),
                    EspacoTotal = Math.Round(espacoTotal / 1024 / 1024 / 1024, 2)
                };
                destinoDao.Inserir(novoDestino);
                CarregarGridViewDestino();
            }
        }

        private void CarregarGridViewDestino()
        {
            //List<Destino> destinos = RepositorioBackup.LerDestino();
            //Convertendo o DataReader Para List
            DestinoDAO destinoDao = new DestinoDAO();
            DbDataReader dr = destinoDao.GetDestinos();
            List<Destino> destinos = new List<Destino>();
            while (dr.Read())
            {
                destinos.Add(new Destino
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    CaminhoDiretorio = dr["CaminhoDiretorio"].ToString(),
                    Unidade = dr["Unidade"].ToString(),
                    EspacoDisponivel = Convert.ToDouble(dr["EspacoDisponivel"]),
                    EspacoTotal = Convert.ToDouble(dr["EspacoTotal"])
                });
            }
            //fim da conversao
            List<DestinoViewModel> destinoViewModels = new List<DestinoViewModel>();
            foreach (Destino destino in destinos)
            {
                DestinoViewModel viewModel = new DestinoViewModel
                {
                    Id = destino.Id,
                    CaminhoDiretorio = destino.CaminhoDiretorio,
                    Unidade = destino.Unidade,
                    EspacoDisponivel = destino.EspacoDisponivel + " de " + destino.EspacoTotal + " GB"
                };
                destinoViewModels.Add(viewModel);
            }
            lvwDestinos.ItemsSource = destinoViewModels;
        }

        private void btnSelecionarPasta_Click(object sender, RoutedEventArgs e)
        {
            OrigemDAO origemDao = new OrigemDAO();
            FolderBrowserDialog fbdSelecionarPasta = new FolderBrowserDialog();
            var resultado = fbdSelecionarPasta.ShowDialog();
            if (resultado == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo pastaSelecionada = new DirectoryInfo(fbdSelecionarPasta.SelectedPath);
                if (VerificarCaminho != pastaSelecionada.FullName)
                {
                    VerificarCaminho = pastaSelecionada.FullName;
                    double tamanhoPasta = Size(pastaSelecionada);
                    Origem novaOrigemPasta = new Origem()
                    {
                        CaminhoArquivo = pastaSelecionada.FullName,
                        NomeArquivo = "",
                        DiretorioArquivo = "",
                        NomeArquivoSemExtensao = pastaSelecionada.Name,
                        ExtensaoArquivo = "",
                        TamanhoArquivo = Math.Round(tamanhoPasta / 1024 / 1024, 2),
                        NomeTarefa = txtNome.Text,
                        TipoTarefa = cmbTipo.Text,
                        HorarioTarefa = Convert.ToDateTime(txtHorario.Text),
                        Zipar = Convert.ToBoolean(cmbZipar.SelectedIndex),
                        Nuvem = Convert.ToBoolean(cmbNuvem.SelectedIndex)
                    };
                    origemDao.Inserir(novaOrigemPasta);
                    CarregarGridViewOrigem();
                    AtualizarTarefa();
                    HabilitarDesabilitarCamposArquivos();
                }
            }
        }

        public static long Size(DirectoryInfo dirInfo)
        {
            long total = 0;
            //Obtem o tamanho total dos arquivos no diretório
            foreach (FileInfo file in dirInfo.GetFiles())
                total += file.Length;

            //Obtem o tamanho total dos sub-diretórios da pasta 
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                total += Size(dir);

            return total;
        }

        private void lvwArquivos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DateTime dataHora = DateTime.Now;
            //string DataFormatada = dataHora.ToString(" ddMMyyyy HHmm");
            //indiceSelecionado = lvwArquivos.SelectedIndex;

            //if (indiceSelecionado >= 0)
            //{
            //    OrigemViewModel origem = (OrigemViewModel)lvwArquivos.Items[indiceSelecionado];
            //    if (cmbZipar.SelectedIndex == 1)
            //        txtNome.Text = origem.Nome + DataFormatada + ".zip";
            //    else
            //        txtNome.Text = origem.Nome + DataFormatada + origem.Extensao;
            //    AtualizarOrigem();
            //}

            HabilitarDesabilitarCamposArquivos();
        }

        private void lvwArquivos_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            indiceSelecionado = lvwArquivos.SelectedIndex;
            if (e.Key == Key.Delete && indiceSelecionado >= 0)
            {
                OrigemViewModel origem = (OrigemViewModel)lvwArquivos.Items[indiceSelecionado];
                OrigemDAO origemDao = new OrigemDAO();
                origemDao.Excluir(origem.Id);
                CarregarGridViewOrigem();
            }
        }

        private void AtualizarTarefa()
        {
            if (tarefaInicial != null)
            {
                if (txtNome.Text != tarefaInicial.NomeTarefa || cmbTipo.Text != tarefaInicial.TipoTarefa || Convert.ToDateTime(txtHorario.Text) != tarefaInicial.HorarioTarefa
                    || Convert.ToBoolean(cmbZipar.SelectedIndex) != tarefaInicial.Zipar || Convert.ToBoolean(cmbNuvem.SelectedIndex) != tarefaInicial.Nuvem)
                {
                    OrigemDAO origemDao = new OrigemDAO();
                    Origem origemAtualizar = new Origem
                    {
                        NomeTarefa = txtNome.Text,
                        TipoTarefa = cmbTipo.Text,
                        HorarioTarefa = Convert.ToDateTime(txtHorario.Text),
                        Zipar = Convert.ToBoolean(cmbZipar.SelectedIndex),
                        Nuvem = Convert.ToBoolean(cmbNuvem.SelectedIndex)
                    };
                    origemDao.Atualizar(origemAtualizar);

                    _ThreadSegundoPlano.Abort();
                    _ThreadSegundoPlano = new Thread(SegundoPlano);
                    _ThreadSegundoPlano.Start();
                }
            }
        }

        private void lvwDestinos_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            indiceSelecionado = lvwDestinos.SelectedIndex;
            if (e.Key == Key.Delete && indiceSelecionado >= 0)
            {
                DestinoViewModel destino = (DestinoViewModel)lvwDestinos.Items[indiceSelecionado];
                DestinoDAO destinoDao = new DestinoDAO();
                destinoDao.Excluir(destino.Id);
                CarregarGridViewDestino();
                HabilitarDesabilitarCamposArquivos();
            }
        }

        private void txtHorario_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int foco;
            txtHorario.MaxLength = (8);
            e.Handled = true;
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;

            if (txtHorario.Text.Length == 2)
            {
                foco = txtHorario.SelectionStart;
                txtHorario.Text = txtHorario.Text.Insert(2, ":");
                txtHorario.SelectionStart = foco + 1;
            }
            if (txtHorario.Text.Length == 5)
            {
                foco = txtHorario.SelectionStart;
                txtHorario.Text = txtHorario.Text.Insert(5, ":");
                txtHorario.SelectionStart = foco + 1;
            }
        }

        private void tabArquivos_LostFocus(object sender, RoutedEventArgs e)
        {
            AtualizarTarefa();
        }

        private void HabilitarDesabilitarCamposArquivos()
        {
            if (lvwArquivos.Items.Count > 0)
            {
                txtNome.IsEnabled = true;
                cmbTipo.IsEnabled = true;
                txtHorario.IsEnabled = true;
                cmbZipar.IsEnabled = true;
                cmbNuvem.IsEnabled = true;
            }
            else
            {
                txtNome.IsEnabled = false;
                cmbTipo.IsEnabled = false;
                txtHorario.IsEnabled = false;
                cmbZipar.IsEnabled = false;
                cmbNuvem.IsEnabled = false;
            }
        }

        private void tabConfiguracoes_LostFocus(object sender, RoutedEventArgs e)
        {
            AtualizarConfiguracoes();
        }

        private void CarregarConfiguracoes()
        {
            ConfiguracoesDAO configDao = new ConfiguracoesDAO();
            DbDataReader dr = configDao.GetConfiguracoes();
            List<Configuracoes> configs = new List<Configuracoes>();
            while (dr.Read())
            {
                configs.Add(new Configuracoes
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FTP = dr["FTP"].ToString(),
                    User = dr["User"].ToString(),
                    Pass = dr["Pass"].ToString(),
                    Porta = Convert.ToInt32(dr["Porta"])
                });
            }

            foreach (Configuracoes config in configs)
            {
                configInicial = new ConfiguracoesViewModel
                {
                    FTP = config.FTP,
                    User = config.User,
                    Pass = config.Pass,
                    Porta = config.Porta
                };
                txtServidor.Text = configInicial.FTP;
                txtUsuario.Text = configInicial.User;
                txtSenha.Text = configInicial.Pass;
                txtPorta.Text = configInicial.Porta.ToString();
            }
            if (configs.Count == 0)
            {
                if (txtPorta.Text == "" || txtPorta.Text == "0")
                    txtPorta.Text = "21";
                Configuracoes config = new Configuracoes();
                config.FTP = txtNome.Text;
                config.User = txtUsuario.Text;
                config.Pass = txtSenha.Text;
                config.Porta = Convert.ToInt32(txtPorta.Text);
                configDao.Inserir(config);
            }
        }

        private void AtualizarConfiguracoes()
        {
            if (txtPorta.Text == "" || txtPorta.Text == "0")
                txtPorta.Text = "21";
            if (txtServidor.Text != configInicial.FTP || txtUsuario.Text != configInicial.User || txtSenha.Text != configInicial.Pass
                || Convert.ToInt32(txtPorta.Text) != configInicial.Porta)
            {
                ConfiguracoesDAO configDao = new ConfiguracoesDAO();
                Configuracoes configAtualizar = new Configuracoes
                {
                    FTP = txtServidor.Text,
                    User = txtUsuario.Text,
                    Pass = txtSenha.Text,
                    Porta = Convert.ToInt32(txtPorta.Text)
                };
                configDao.Atualizar(configAtualizar);
            }
        }

        private void SegundoPlano()
        {
            bool isBackground = true;
            RepositorioBackup repositorio = new RepositorioBackup();
            repositorio.FazerBackup(isBackground);
            repositorio.AtualizarDUBackup();
        }

        #region Bandeja
        private void MinimizarParaBandeja()
        {

            if (notifyIcon == null)
            {
                notifyIcon = new NotifyIcon();
                notifyIcon.Icon = new Icon("Icone.ico");
                notifyIcon.Visible = true;

                notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);
                notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseDoubleClick);
                notifyIcon.ShowBalloonTip(500, "Titulo", "Texto", ToolTipIcon.Info);

                System.Windows.Forms.ContextMenu notifyIconContextMenu = new System.Windows.Forms.ContextMenu();
                notifyIconContextMenu.MenuItems.Add("Exibir BackupPro", new EventHandler(Exibir));
                notifyIconContextMenu.MenuItems.Add("Sair", new EventHandler(Sair));

                notifyIcon.ContextMenu = notifyIconContextMenu;
            }
        }

        private void Sair(object sender, EventArgs e)
        {
            _ThreadSegundoPlano.Abort();
            CloseAllWindows();
        }

        private void Exibir(object sender, EventArgs e)
        {
            OpenWindow(typeof(MainWindow));
        }

        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            OpenWindow(typeof(wnwNotificaUltimoBackup));
        }

        private void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            OpenWindow(typeof(MainWindow));
        }

        public static void OpenWindow(Type wdwType)
        {
            bool bolCtl = false;
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType().Equals(wdwType))
                {
                    window.Show();
                    window.WindowState = WindowState.Normal;
                    bolCtl = true;
                    break;
                }
            }
            if (!bolCtl)
            {
                Window wdw = (Window)Activator.CreateInstance(wdwType);
                wdw.Show();
            }
        }

        private void CloseAllWindows()
        {
            for (int intCounter = System.Windows.Application.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                System.Windows.Application.Current.Windows[intCounter].Close();
        }

        private void MenuItemAbrir_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(MainWindow));
        }

        private void MenuItemSair_Click(object sender, RoutedEventArgs e)
        {
            _ThreadSegundoPlano.Abort();
            NotifyIconHarcodet.Dispose();
            CloseAllWindows();
        }

        #endregion

        #region Upload FTP

        string Ftp;
        string User;
        string Pass;
        int Port;
        bool UsePassive;
        string Arquivo;
        string DiretorioFtp;

        private FtpClient _ftpClient = null;
        private bool _ftpUploadStopped = false;

        public void ParametrosFtp(string FTP, string Usuario, string Senha, int Porta, bool ConexaoPassiva)
        {
            Ftp = FTP;
            User = Usuario;
            Pass = Senha;
            Port = Porta;
            UsePassive = ConexaoPassiva;
            DiretorioFtp = "/Arquivos/";
        }

        private bool AtivarUploadFtp()
        {
            if (!string.IsNullOrEmpty(Ftp) && !string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Pass))
                return true;
            else
                return false;
        }

        public void Upload()
        {
            _ftpUploadStopped = false;
            var user = string.Empty;
            var password = string.Empty;
            var localDirectory = string.Empty;
            var localFileName = string.Empty;
            var remoteDirectory = string.Empty;
            var remoteFileName = string.Empty;

            GetFtpParameters(out user, out password, out localDirectory, out localFileName, out remoteDirectory, out remoteFileName);

            if (_ftpClient != null) _ftpClient.Abort();
            _ftpClient = new FtpClient(Ftp, user, Pass, Port, UsePassive);
            _ftpClient.UploadProgressChanged += new EventHandler<UploadProgressChangedLibArgs>(ftpClient_UploadProgressChanged);
            _ftpClient.UploadFileCompleted += new EventHandler<UploadFileCompletedEventLibArgs>(ftpClient_UploadFileCompleted);
            _ftpClient.Upload(localDirectory, localFileName, remoteDirectory, remoteFileName);
        }

        private void ftpClient_UploadProgressChanged(object sender, UploadProgressChangedLibArgs e)
        {
            var statusMessage = string.Empty;
            switch (e.TransmissionState)
            {
                case TransmissionState.Uploading:
                    statusMessage = "Uploading..";
                    break;
                case TransmissionState.CreatingDir:
                    statusMessage = "Criando (sub)diretório..";
                    break;
                case TransmissionState.ProofingDirExits:
                    statusMessage = "Verificando se o diretório existe..";
                    break;
                default:
                    statusMessage = "Upload falhou";
                    break;
            }//switch

            Dispatcher.BeginInvoke((Action)delegate
            {

                if (tarefaInicial.Zipar)
                    progressBar1.Value = (e.Procent / 2) + 50;
                else
                    progressBar1.Value = e.Procent;
                lblBytesSend.Content = e.BytesSent;
                lblBytesTotal.Content = e.TotalBytesToSend;
                SetLabelStatus(statusMessage);
            });
        }

        private void ftpClient_UploadFileCompleted(object sender, UploadFileCompletedEventLibArgs e)
        {
            var statusMessage = string.Empty;
            switch (e.TransmissionState)
            {
                case TransmissionState.Success:
                    statusMessage = "Upload concluído com sucesso";
                    break;
                default:
                    statusMessage = "Upload falhou";
                    break;
            }//switch
            SetLabelStatus(statusMessage);
        }

        private void GetFtpParameters(out string user, out string password, out string localDirectory, out string localFileName,
                                      out string remoteDirectory, out string remoteFileName)
        {
            user = User;
            password = Pass;
            Arquivo = RepositorioBackup.GetSalvoComo();
            var tipo = Path.GetExtension(Arquivo);
            if (tipo != "")
            {
                var fileInfo = new FileInfo(Arquivo);
                localDirectory = fileInfo.DirectoryName.Replace("\\", "/");
                localFileName = fileInfo.Name;
                remoteDirectory = DiretorioFtp.Trim().Replace("\\", "/");
                remoteFileName = fileInfo.Name;
            }
            else
            {
                var directoryInfo = new DirectoryInfo(Arquivo);
                localDirectory = directoryInfo.FullName.Replace("\\", "/");
                localFileName = directoryInfo.Name;
                remoteDirectory = DiretorioFtp.Trim().Replace("\\", "/") + localFileName + "/";
                remoteFileName = directoryInfo.GetFiles().ToString();
            }
        }

        private void SetLabelStatus(string statusMessage)
        {
            if (!_ftpUploadStopped)
            {
                Dispatcher.BeginInvoke((Action)delegate
                {
                    lblStatus.Text = statusMessage;
                    if (statusMessage == "Upload falhou" || statusMessage == "Upload concluído com sucesso")
                        stackPanelProgress.Visibility = Visibility.Hidden;
                });
            }
        }
        #endregion
    }
}
