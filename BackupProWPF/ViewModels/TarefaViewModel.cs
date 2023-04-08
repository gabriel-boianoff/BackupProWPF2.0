using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupProWPF.ViewModels
{
    class TarefaViewModel
    {
        public DateTime HorarioTarefa { get; set; }
        public string NomeTarefa { get; set; }
        public bool Nuvem { get; set; }
        public string TipoTarefa { get; set; }
        public bool Zipar { get; set; }
    }
}
