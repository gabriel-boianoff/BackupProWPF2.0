using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPro.Dominio
{
    public class Destino
    {
        public int Id { get; set; }
        public string CaminhoDiretorio { get; set; }
        public string NomeDiretorio { get; set; }
        public string Unidade { get; set; }
        public double EspacoDisponivel { get; set; }
        public double EspacoTotal { get; set; }
    }
}
