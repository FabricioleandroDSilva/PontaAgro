using Ponta.Dominio.Entidades;
using Ponta.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.Entidades
{
    public class Tarefas: EntidadeBase
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Status Status { get; set; }
        public int IdUsuario { get; set; }  
        public Usuario Usuario { get; set; }
    }
}
