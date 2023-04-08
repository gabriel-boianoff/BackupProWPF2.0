using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPro.Dominio
{
    public class Origem
    {
        public int Id { get; set; }
        public string CaminhoArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string DiretorioArquivo { get; set; }
        public string NomeArquivoSemExtensao { get; set; }
        public string ExtensaoArquivo { get; set; }
        public double TamanhoArquivo { get; set; }
        public string NomeTarefa { get; set; }
        public string TipoTarefa { get; set; }
        public DateTime HorarioTarefa { get; set; }
        public bool Zipar { get; set; }
        public bool Nuvem { get; set; }
        public DateTime UltimoBackup { get; set; }
        public string SalvoComo { get; set; }
        public List<string> ListaNomeArquivos { get; set; }

        //public override string ToString()
        //{
        //    return string.Format("{0}", this.CaminhoArquivo);
        //}
    }
}
