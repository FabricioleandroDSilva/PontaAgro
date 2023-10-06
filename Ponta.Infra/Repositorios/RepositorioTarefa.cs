using Microsoft.EntityFrameworkCore;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.IRepositorios;
using Ponta.Infra.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Infra.Repositorios
{
    public class RepositorioTarefa : RepositorioBase<Tarefas>
    {
        public RepositorioTarefa(PostgrSqlContext postgrSqlContext) : base(postgrSqlContext)
        {
        }

        public override Tarefas SelecionarPorId(int id)
        {
            return _postgrSqlContext.Set<Tarefas>()
                 .Include(prop => prop.Usuario).AsNoTracking().FirstOrDefault(prop => prop.Id == id);
        }
    }
    
}
