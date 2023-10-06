using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Servico.Validadores
{
    using FluentValidation;
    using Ponta.Dominio.Entidades;

    public class ValidadorTarefas : AbstractValidator<Tarefas>
    {
        public ValidadorTarefas()
        {
            
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("Campo obrigatório! Preencha o campo Título.")
                .NotNull().WithMessage("Campo obrigatório! Preencha o campo Título.")
                .MaximumLength(80).WithMessage("Campo comporta até 80 caracteres. Verifique o mesmo e tente novamente.");

            RuleFor(c => c.Descricao)
                 .NotEmpty().WithMessage("Campo obrigatório! Preencha o campo Descrição.")
                .NotNull().WithMessage("Campo obrigatório! Preencha o campo Descrição.")
                .MaximumLength(200).WithMessage("Campo comporta até 200 caracteres. Verifique o mesmo e tente novamente.");

            RuleFor(c => c.Data)
                .NotEmpty().WithMessage("Campo obrigatório! Preencha o campo Data.")
                .NotNull().WithMessage("Campo obrigatório! Preencha o campo Data.");

            RuleFor(c => c.Status)
                .NotNull().WithMessage("Campo obrigatório! Utilize: \n 0 - Pendente \n 1 - Em Conclusão \n 2 - Concluido.")
             .IsInEnum().WithMessage("Status incorreto.Utilize: \n 0 - Pendente \n 1 - Em Conclusão \n 2 - Concluido.");

        }
    }

}
