using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Servico.Validadores
{
    using FluentValidation;
    using Ponta.Dominio.Entidades;

    public class ValidadorUsuario : AbstractValidator<Usuario>
    {
        public ValidadorUsuario()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Campo obrigatório! Preencha o campo Nome.")
                .NotNull().WithMessage("Campo obrigatório! Preencha o campo Nome.")
                .MaximumLength(100).WithMessage("Campo comporta até 100 caracteres. Verifique o mesmo e tente novamente.");

            RuleFor(c => c.Login)
                .NotEmpty().WithMessage("Campo obrigatório! Preencha o campo Login.")
                .NotNull().WithMessage("Campo obrigatório! Preencha o campo Login.")
                .MaximumLength(25).WithMessage("Campo comporta até 25 caracteres. Verifique o mesmo e tente novamente.");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("Campo obrigatório! Preencha o campo Senha.")
                .NotNull().WithMessage("Campo obrigatório! Preencha o campo Senha.")
                .MaximumLength(30).WithMessage("Campo comporta até 30 caracteres. Verifique o mesmo e tente novamente.");
        }
    }

}
