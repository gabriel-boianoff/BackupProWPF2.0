using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupProWPF.ViewModels
{
    class DestinoViewModel
    {
        public int Id { get; set; }
        public string CaminhoDiretorio { get; set; }
        public string Unidade { get; set; }
        public string EspacoDisponivel { get; set; }
    }
}
