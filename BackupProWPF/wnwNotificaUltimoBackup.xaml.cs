using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BackupProWPF
{
    /// <summary>
    /// Interaction logic for wnwNotificaUltimoBackup.xaml
    /// </summary>
    public partial class wnwNotificaUltimoBackup : Window
    {
        public wnwNotificaUltimoBackup()
        {
            InitializeComponent();
        }

        private void wnwNUB_Loaded(object sender, RoutedEventArgs e)
        {
            txbStatus.Focus();
        }

        private void txbAbrirHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            wnwNUB.Close();
            MainWindow.OpenWindow(typeof(MainWindow));
        }
    }
}
