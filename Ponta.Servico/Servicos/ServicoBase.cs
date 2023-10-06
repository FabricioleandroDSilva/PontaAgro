using AutoMapper;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.IRepositorios;
using FluentValidation;
using Ponta.Dominio.IServicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Servico.Servicos
{
    public abstract class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : EntidadeBase
    {
        private readonly IRepositorioBase<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public ServicoBase(IRepositorioBase<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual TOutputModel Inserir<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class

        {

            TEntity entity = _mapper.Map<TEntity>(obj);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Inserir(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }
        public virtual TOutputModel Atualizar<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {

            TEntity entity = _mapper.Map<TEntity>(obj);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Atualizar(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }

        public virtual bool Apagar(int id)
        {

            _baseRepository.Apagar(id);
            return true;

        }
        public TOutputModel SelecionarPorId<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = _baseRepository.SelecionarPorId(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public virtual IEnumerable<TOutputModel> ListarTodos<TOutputModel>() where TOutputModel : class
        {
            var entities = _baseRepository.ListarTodos();

            var outputModel = entities.Select(entity => _mapper.Map<TOutputModel>(entity));

            return outputModel;
        }

        public static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registro não detectados.");

            validator.ValidateAndThrow(obj);
        }
    }
}
