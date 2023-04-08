using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPro.Repositorio
{
    public interface IRepositorio
    {
        string Copiar();
        string Zipar();
        string UploadFTP();
    }
}
