using Ponta.Dominio.Enumeradores;
using System;

namespace Ponta.Aplicacao.Modelos
{
    
    public class ModeloEntradaTarefa
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }
    }
}
