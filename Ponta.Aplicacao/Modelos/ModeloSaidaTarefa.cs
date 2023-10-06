using Ponta.Dominio.Enumeradores;
using System;

namespace Ponta.Aplicacao.Modelos
{
    public class ModeloSaidaTarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
    }
}
