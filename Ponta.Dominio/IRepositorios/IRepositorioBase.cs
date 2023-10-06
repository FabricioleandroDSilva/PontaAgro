using Ponta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.IRepositorios
{
    public interface IRepositorioBase<TEntity> where TEntity : EntidadeBase
    {
        void Inserir(TEntity obj);
        void Atualizar(TEntity obj);
        bool Apagar(int entities);
        IList<TEntity> ListarTodos();
        TEntity SelecionarPorId(int id);
        IList<TEntity> PesquisarCamposTexto(Dictionary<string, string> camposEDados);
    }
}
