using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Ponta.Infra.Contexto;
using Ponta.Dominio.IRepositorios;
using Ponta.Dominio.Entidades;
using System;
using Ponta.Dominio.Enumeradores;

namespace Ponta.Infra.Repositorios
{
    public abstract class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : EntidadeBase
    {
        protected readonly PostgrSqlContext _postgrSqlContext;

        public RepositorioBase(PostgrSqlContext postgrSqlContext)
        {
            _postgrSqlContext = postgrSqlContext;
        }

        public virtual void Inserir(TEntity obj)
        {
            _postgrSqlContext.Set<TEntity>().Add(obj);
            _postgrSqlContext.SaveChanges();
        }

        public virtual void Atualizar(TEntity obj)
        {
            _postgrSqlContext.Entry(obj).State = EntityState.Modified;
            _postgrSqlContext.SaveChanges();
        }

        public virtual bool Apagar(int id)
        {
            var obj = SelecionarPorId(id);
            if (obj == null)
                throw new Exception("Registro não encontrado na base de dados.");
            _postgrSqlContext.Set<TEntity>().Remove(obj);
            _postgrSqlContext.SaveChanges();
            return true;
        }

        public virtual IList<TEntity> ListarTodos()
        {

            return _postgrSqlContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public virtual TEntity SelecionarPorId(int id)
        {
            return _postgrSqlContext.Set<TEntity>().AsNoTracking().FirstOrDefault(prop => prop.Id == id);
        }
        public virtual IList<TEntity> PesquisarCamposTexto(Dictionary<string, string> camposEDados)
        {
            IQueryable<TEntity> query = _postgrSqlContext.Set<TEntity>().AsNoTracking();

            foreach (var item in camposEDados)
            {
                var propriedade = typeof(TEntity).GetProperty(item.Key);
                if (propriedade != null)
                {
                    if (propriedade.Name == "Status" && Enum.TryParse(item.Value, out Status parsedStatus))
                        query = query.Where(prop => EF.Property<Status>(prop, propriedade.Name) == parsedStatus);
                    else
                        query = query.Where(prop => EF.Property<string>(prop, propriedade.Name).ToLower().Contains(item.Value.ToLower()));
                }
            }

            return query.ToList();
        }
    }
}
