using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPro.Dominio
{
    public class Configuracoes
    {
        public int Id { get; set; }
        public string FTP { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public int Porta { get; set; }
    }
}
