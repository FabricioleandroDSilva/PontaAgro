using Ponta.Infra.Repositorios;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.IRepositorios;
using Ponta.Infra.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ponta.Infra.RepositórioBase
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(PostgrSqlContext postgrSqlContext) : base(postgrSqlContext)
        {
        }

        public Usuario Login(string login,string senha)
        {
            return _postgrSqlContext.Set<Usuario>().Where(prop=> prop.Login.Equals(login) && prop.Senha.Equals(senha)).FirstOrDefault();

        }

        
    }
}
