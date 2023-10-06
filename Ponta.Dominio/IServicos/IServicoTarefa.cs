using Ponta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.IServicos
{
    public interface IServicoTarefa : IServicoBase<Tarefas>
    {
        IEnumerable<TOutputModel> SelecionarPorStatus<TOutputModel>(int? status) where TOutputModel : class;
    }
}
