using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ponta.Aplicacao.Modelos;
using Ponta.Aplicacao.Utilidades;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.IServicos;
using Ponta.Servico.Servicos;
using Ponta.Servico.Validadores;
using System;
using System.Collections.Generic;

namespace Ponta.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly IServicoTarefa _servicoBase;

        private string logLabel = "[ROTA: TarefaController/{0}] [USUARIO: {1} ] [DADOS: {2} ] ";
        private List<string> camposlogs = new();
        private string logLabelController;

        private readonly ILogger<Tarefas> _logger;
        public TarefaController(IServicoTarefa servicoBase, ILogger<Tarefas> logger)
        {
            _servicoBase = servicoBase;
            _logger = logger;
        }


        [HttpPost]
        [Route("Incluir")]
        [Authorize]
        public IActionResult Inserir([FromBody] ModeloEntradaTarefa modelo)
        {
            logLabelController = "Inserir";
            return Execute(() => _servicoBase.Inserir<ModeloEntradaTarefa, Tarefas, ValidadorTarefas>(modelo));
       
        }


        [HttpPost]
        [Route("Atualizar")]
        [Authorize]
        public IActionResult Atualizar([FromBody] ModeloAtualizacaoTarefa modelo)
        {
            if (modelo == null)
                return NotFound();

            logLabelController = "Atualizar";

            return Execute(() => _servicoBase.Atualizar<ModeloAtualizacaoTarefa, Tarefas, ValidadorTarefas>(modelo));
        }

        [HttpDelete]
        [Route("Apagar")]
        [Authorize]
        public IActionResult Apagar(int id)
        {
            try
            {
                if (_servicoBase.Apagar(id))
                {
                    camposlogs.AddRange(TratamentoDeLogs.CamposLogs("Apagar | "+ HttpContext.User.Identity.Name +" | " + id.ToString() + "| Sucesso"));
                    _logger.LogInformation(TratamentoDeLogs.FormatarLabel(logLabel, camposlogs));

                    return Ok("Tarefa excluida com sucesso.");
                }

                camposlogs.AddRange(TratamentoDeLogs.CamposLogs("Apagar | " + HttpContext.User.Identity.Name + " | " + id.ToString() + "| Erro"));
                _logger.LogInformation(TratamentoDeLogs.FormatarLabel(logLabel, camposlogs));

                return BadRequest("Erro , entre em contato com o setor de TI.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "\r\n\r\nErro , entre em contato com o setor de TI.");
            }

        }

        [HttpGet]
        [Route("ListarTodos")]
        [Authorize]
        public IActionResult ListarTodos()
        {
            logLabelController = "ListarTodos";
            return Execute(() => _servicoBase.ListarTodos<ModeloSaidaTarefa>());
        }

        [HttpGet]
        [Route("ListarPorStatus")]
        [Authorize]
        public IActionResult ListarPorStatus([FromQuery] int? status) 
        {
            logLabelController = "ListarPorStatus";
            return Execute(() => _servicoBase.SelecionarPorStatus<ModeloSaidaTarefa>(status));
        } 
        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                camposlogs.AddRange(TratamentoDeLogs.CamposLogs(logLabelController + "|"+ HttpContext.User.Identity.Name + "|"+ JsonConvert.SerializeObject(result) + " | Sucesso"));
                _logger.LogInformation(TratamentoDeLogs.FormatarLabel(logLabel, camposlogs));

                return Ok(result);
            }
            catch (Exception ex)
            {
                camposlogs.AddRange(TratamentoDeLogs.CamposLogs(logLabelController+ "|" + HttpContext.User.Identity.Name + "|" + ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "| Erro"));
                _logger.LogCritical(TratamentoDeLogs.FormatarLabel(logLabel, camposlogs));
                return BadRequest(ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "\r\n\r\nErro , entre em contato com o setor de TI.");
            }
        }
    }
}
