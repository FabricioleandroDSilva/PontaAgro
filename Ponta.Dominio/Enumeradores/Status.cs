using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.Enumeradores
{
    public enum Status
    {
        [Description("P")]   
        Pendente,

        [Description("EC")]
        EmAndamento,

        [Description("C")]
        Completo
    }

}
