using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.Enumeradores;
using Ponta.Dominio.IRepositorios;
using Ponta.Dominio.IServicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Servico.Servicos
{
    public class ServicoTarefa : ServicoBase<Tarefas>, IServicoTarefa
    {
        private readonly IRepositorioBase<Tarefas> _repositorioBaseTarefas;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositorioUsuario _repositorioBaseUsuario;
        public ServicoTarefa(IRepositorioBase<Tarefas> repositorioBaseTarefas, IMapper mapper, IHttpContextAccessor httpContextAccessor, IRepositorioUsuario repositorioBaseUsuario) : base(repositorioBaseTarefas, mapper)
        {
            _repositorioBaseTarefas = repositorioBaseTarefas;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _repositorioBaseUsuario = repositorioBaseUsuario;
        }
        public override TOutputModel Inserir<TInputModel, TOutputModel, TValidator>(TInputModel obj)
        {
            Tarefas entity = _mapper.Map<Tarefas>(obj);

            Validate(entity, Activator.CreateInstance<TValidator>());

            Dictionary<string, string> camposvalor = new()
            {
                { "Login", _httpContextAccessor.HttpContext.User.Identity.Name}
            };
            var usuarioLogado = _repositorioBaseUsuario.PesquisarCamposTexto(camposvalor).FirstOrDefault();

            entity.IdUsuario = usuarioLogado.Id;
            Validate(entity, Activator.CreateInstance<TValidator>());
            _repositorioBaseTarefas.Inserir(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }
        public override TOutputModel Atualizar<TInputModel, TOutputModel, TValidator>(TInputModel obj)
        {
            Tarefas entity = _mapper.Map<Tarefas>(obj);

            Tuple<int, string> usuarioBanco = RetornarUsuarioLogado(entity.Id);

            if (!usuarioBanco.Item2.ToUpper().Equals(_httpContextAccessor.HttpContext.User.Identity.Name.ToUpper()))
                throw new Exception("Usuário não tem permissão para alterar esta tarefa.");

            entity.IdUsuario = usuarioBanco.Item1;
            Validate(entity, Activator.CreateInstance<TValidator>());
            _repositorioBaseTarefas.Atualizar(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }
        public virtual IEnumerable<TOutputModel> SelecionarPorStatus<TOutputModel>(int? status) where TOutputModel : class
        {
            if (status == null)
                throw new Exception("Por Favor preencha o campo status.");

            if (status != 0 && status != 1 && status != 2)
                throw new Exception("Status incorreto. Utilize \n 0 - Pendente \n 1 - Em Conclusão \n 2 - Concluido.");

            Dictionary<string, string> camposvalor = new ()
            {
                { "Status", status.ToString()}
            };

            var entities = _repositorioBaseTarefas.PesquisarCamposTexto(camposvalor);
            var outputModel = entities.Select(entity => _mapper.Map<TOutputModel>(entity));

            return outputModel;
        }
        public override bool Apagar(int id)
        {

            Tuple<int, string> usuarioBanco = RetornarUsuarioLogado(id);
            if (!usuarioBanco.Item2.ToUpper().Equals(_httpContextAccessor.HttpContext.User.Identity.Name.ToUpper()))
                throw new Exception("Usuário não tem permissão para Excluir esta tarefa.");

            _repositorioBaseTarefas.Apagar(id);
            return true;    

        }

        private Tuple<int, string> RetornarUsuarioLogado(int id)
        {
            Tarefas tarefas = _repositorioBaseTarefas.SelecionarPorId(id);
            if (tarefas == null)
                throw new Exception("Registro não encontrado.");

            return new Tuple<int, string>(tarefas.Usuario.Id, tarefas.Usuario.Login);
        }

    }
}
