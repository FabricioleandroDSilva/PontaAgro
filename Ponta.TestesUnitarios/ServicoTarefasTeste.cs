using NUnit.Framework;
using Moq;
using Ponta.Infra.Repositorios;
using Ponta.Dominio.IRepositorios;
using Ponta.Dominio.Entidades;
using Ponta.Servico.Servicos;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Ponta.Aplicacao.Modelos;
using System;
using Ponta.Dominio.IServicos;
using Ponta.Servico.Validadores;
using System.Collections.Generic;
using Ponta.Dominio.Enumeradores;

namespace Ponta.TestesUnitarios
{
    public class ServicoTarefasTeste
    {
      

        [SetUp]
        public void Setup()
        {
            
        }

        [Test, Order(1)]
        public void Inserir()
        {
            var baseTarefasRepositorioMock = new Mock<IRepositorioBase<Tarefas>>();
            var baseUsuariosRepostorioMock = new Mock<IRepositorioUsuario>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var mapperMock = new Mock<IMapper>();

            var usuarioLogado = new Usuario { Id = 1, Nome = "fabricio teste", Login = "fabricio.silva",Senha= "1234" };

            var Tarefas  = new ModeloEntradaTarefa
            {
                Titulo = "Tarefa 1",
                Descricao = "Teste descrição Tarefa",
                Data = DateTime.UtcNow,
                Status =  1
            };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(usuarioLogado.Login);


            baseUsuariosRepostorioMock.Setup(x => x.PesquisarCamposTexto(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { usuarioLogado });

            var servicoTarefa = new ServicoTarefa(baseTarefasRepositorioMock.Object, mapperMock.Object, httpContextAccessorMock.Object, baseUsuariosRepostorioMock.Object);

            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<Tarefas>()))
                .Returns((Tarefas input) => new Tarefas{ Titulo = "Tarefa 1", Descricao = "Teste descrição Tarefa", Data = DateTime.UtcNow, Status = Status.Pendente, IdUsuario =1,Id =1});

            mapperMock.Setup(x => x.Map<Tarefas>(It.IsAny<ModeloEntradaTarefa>()))
                      .Returns(new Tarefas { IdUsuario = usuarioLogado.Id, Titulo = Tarefas.Titulo,Descricao = Tarefas.Descricao,Data=Tarefas.Data,Status=Status.Pendente });

            mapperMock.Setup(x => x.Map<Tarefas>(It.IsAny<ModeloEntradaTarefa>()))
                     .Returns(new Tarefas { IdUsuario = usuarioLogado.Id, Titulo = Tarefas.Titulo, Descricao = Tarefas.Descricao, Data = Tarefas.Data, Status = Status.Pendente });

            var modelo = new ModeloEntradaTarefa
            {
                Titulo = "Tarefa 1",
                Descricao = "Teste descrição Tarefa",
                Data = DateTime.UtcNow,
                Status = 1
            };

            var resultado = servicoTarefa.Inserir<ModeloEntradaTarefa, Tarefas, ValidadorTarefas>(modelo);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(usuarioLogado.Id, resultado.IdUsuario);
        }

        [Test, Order(2)]
        public void Alterar()
        {
            var baseTarefasRepositorioMock = new Mock<IRepositorioBase<Tarefas>>();
            var baseUsuariosRepostorioMock = new Mock<IRepositorioUsuario>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var mapperMock = new Mock<IMapper>();

            var usuarioLogado = new Usuario { Id = 1, Nome = "fabricio teste", Login = "fabricio.silva", Senha = "1234" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(usuarioLogado.Login);

            baseUsuariosRepostorioMock.Setup(x => x.PesquisarCamposTexto(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { usuarioLogado });

            var servicoTarefa = new ServicoTarefa(baseTarefasRepositorioMock.Object, mapperMock.Object, httpContextAccessorMock.Object, baseUsuariosRepostorioMock.Object);

            baseTarefasRepositorioMock.Setup(x => x.SelecionarPorId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Fabricio Silva",
                                              Login = "fabricio.silva"
                                          },
                                          Titulo = "Tarefa existente",
                                          Descricao = "Descrição anterior",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<ModeloAtualizacaoTarefa>()))
                .Returns((ModeloAtualizacaoTarefa input) => new Tarefas
                {
                    Id = input.Id,
                    IdUsuario = usuarioLogado.Id,
                    Titulo = input.Titulo,
                    Descricao = input.Descricao,
                    Data = input.Data,
                    Status = Status.Pendente
                });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<Tarefas>()))
                .Returns((Tarefas input) => new Tarefas { Titulo = "Tarefa 1", Descricao = "Descrição atualizada", Data = DateTime.UtcNow, Status = Status.Pendente, IdUsuario = 1, Id = 1 });
            var modelo = new ModeloAtualizacaoTarefa
            {
                Id = 1, 
                Titulo = "Tarefa atualizada",
                Descricao = "Descrição atualizada",
                Data = DateTime.UtcNow,
                Status = 1
            };

            var resultado = servicoTarefa.Atualizar<ModeloAtualizacaoTarefa, Tarefas, ValidadorTarefas>(modelo);

            Assert.IsNotNull(resultado, "O resultado não deve ser nulo");
            Assert.AreEqual("Descrição atualizada", resultado.Descricao, "A descrição não foi atualizada corretamente");
        }

        [Test, Order(3)]
        public void AlterarUsuarioDiferente()
        {
            var baseTarefasRepositorioMock = new Mock<IRepositorioBase<Tarefas>>();
            var baseUsuariosRepostorioMock = new Mock<IRepositorioUsuario>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var mapperMock = new Mock<IMapper>();

            var usuarioLogado = new Usuario { Id = 1, Nome = "fabricio teste", Login = "leandro.silva", Senha = "1234" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(usuarioLogado.Login);

            baseUsuariosRepostorioMock.Setup(x => x.PesquisarCamposTexto(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { usuarioLogado });

            var servicoTarefa = new ServicoTarefa(baseTarefasRepositorioMock.Object, mapperMock.Object, httpContextAccessorMock.Object, baseUsuariosRepostorioMock.Object);

            baseTarefasRepositorioMock.Setup(x => x.SelecionarPorId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Fabricio Silva",
                                              Login = "fabricio.silva"
                                          },
                                          Titulo = "Tarefa existente",
                                          Descricao = "Descrição anterior",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<ModeloAtualizacaoTarefa>()))
                .Returns((ModeloAtualizacaoTarefa input) => new Tarefas
                {
                    Id = input.Id,
                    IdUsuario = usuarioLogado.Id,
                    Titulo = input.Titulo,
                    Descricao = input.Descricao,
                    Data = input.Data,
                    Status = Status.Pendente
                });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<Tarefas>()))
                .Returns((Tarefas input) => new Tarefas { Titulo = "Tarefa 1", Descricao = "Descrição atualizada", Data = DateTime.UtcNow, Status = Status.Pendente, IdUsuario = 2, Id = 1 });
            var modelo = new ModeloAtualizacaoTarefa
            {
                Id = 1,
                Titulo = "Tarefa atualizada",
                Descricao = "Descrição atualizada",
                Data = DateTime.UtcNow,
                Status = 1
            };

            try
            {
                var resultado = servicoTarefa.Atualizar<ModeloAtualizacaoTarefa, Tarefas, ValidadorTarefas>(modelo);
                Assert.Fail("A exceção esperada não foi lançada.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Usuário não tem permissão para alterar esta tarefa.", ex.Message);
            }
        }

        [Test, Order(3)]
        public void ExcluirUsuarioDiferente()
        {
            var baseTarefasRepositorioMock = new Mock<IRepositorioBase<Tarefas>>();
            var baseUsuariosRepostorioMock = new Mock<IRepositorioUsuario>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var mapperMock = new Mock<IMapper>();

            var usuarioLogado = new Usuario { Id = 1, Nome = "fabricio teste", Login = "leandro.silva", Senha = "1234" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(usuarioLogado.Login);

            baseUsuariosRepostorioMock.Setup(x => x.PesquisarCamposTexto(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { usuarioLogado });

            var servicoTarefa = new ServicoTarefa(baseTarefasRepositorioMock.Object, mapperMock.Object, httpContextAccessorMock.Object, baseUsuariosRepostorioMock.Object);

            baseTarefasRepositorioMock.Setup(x => x.SelecionarPorId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Fabricio Silva",
                                              Login = "fabricio.silva"
                                          },
                                          Titulo = "Tarefa existente",
                                          Descricao = "Descrição anterior",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<ModeloAtualizacaoTarefa>()))
                .Returns((ModeloAtualizacaoTarefa input) => new Tarefas
                {
                    Id = input.Id,
                    IdUsuario = usuarioLogado.Id,
                    Titulo = input.Titulo,
                    Descricao = input.Descricao,
                    Data = input.Data,
                    Status = Status.Pendente
                });
          
            try
            {
                var resultado = servicoTarefa.Apagar(1);
                Assert.Fail("A exceção esperada não foi lançada.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Usuário não tem permissão para Excluir esta tarefa.", ex.Message);
            }
        }

        [Test, Order(4)]
        public void Excluir()
        {
            var baseTarefasRepositorioMock = new Mock<IRepositorioBase<Tarefas>>();
            var baseUsuariosRepostorioMock = new Mock<IRepositorioUsuario>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var mapperMock = new Mock<IMapper>();

            var usuarioLogado = new Usuario { Id = 1, Nome = "fabricio teste", Login = "fabricio.silva", Senha = "1234" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(usuarioLogado.Login);

            baseUsuariosRepostorioMock.Setup(x => x.PesquisarCamposTexto(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { usuarioLogado });

            var servicoTarefa = new ServicoTarefa(baseTarefasRepositorioMock.Object, mapperMock.Object, httpContextAccessorMock.Object, baseUsuariosRepostorioMock.Object);

            baseTarefasRepositorioMock.Setup(x => x.SelecionarPorId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Fabricio Silva",
                                              Login = "fabricio.silva"
                                          },
                                          Titulo = "Tarefa existente",
                                          Descricao = "Descrição anterior",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });
           
            var resultado = servicoTarefa.Apagar(1);
            Assert.IsTrue(resultado, "O resultado da exclusão deve ser verdadeiro.");
        }

    }
}