using Ponta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.IServicos
{
    public interface IServicoLogin: IServicoBase<Usuario>
    {
        string Login(string login, string senha);

    }
}
