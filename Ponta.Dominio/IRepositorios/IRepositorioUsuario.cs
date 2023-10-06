using Ponta.Dominio.IRepositorios;
using Ponta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.IRepositorios
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Usuario Login(string username, string password);

        
    }
}
