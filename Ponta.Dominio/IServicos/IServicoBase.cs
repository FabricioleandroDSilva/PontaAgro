using Ponta.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.IServicos
{
    public interface IServicoBase<TEntity> where TEntity : EntidadeBase
    {
        TOutputModel Inserir<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        TOutputModel Atualizar<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        bool Apagar(int id);
        IEnumerable<TOutputModel> ListarTodos<TOutputModel>() where TOutputModel : class;
        TOutputModel SelecionarPorId<TOutputModel>(int id) where TOutputModel : class;
        
    }
}
