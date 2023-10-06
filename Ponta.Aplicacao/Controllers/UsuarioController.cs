using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ponta.Aplicacao.Modelos;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.IServicos;
using Ponta.Servico.Validadores;
using System;

namespace Ponta.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IServicoLogin _servicoBase;
        public LoginController(IServicoLogin servicoBase)
        {
            _servicoBase = servicoBase;
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] ModeloLogin model)
        {
            if (model == null)
                return NotFound();

            var token = _servicoBase.Login(model.Login, model.Senha);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        [HttpPost]
        [Route("Inserir")]
        [AllowAnonymous]
        public IActionResult Inserir([FromBody] ModeloEntradaUsuario modelo)
        {
            if (modelo == null)
                return NotFound();

            return Execute(() => _servicoBase.Inserir<ModeloEntradaUsuario, Usuario, ValidadorUsuario>(modelo));
        }
        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "\r\n\r\nErro , entre em contato com o setor de TI.");
            }
        }
    }
}
