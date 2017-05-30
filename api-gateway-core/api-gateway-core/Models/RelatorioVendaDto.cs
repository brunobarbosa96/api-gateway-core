using System.Collections.Generic;
using System.Linq;

namespace api_gateway_core.Models
{
    public class RelatorioVendaDto
    {
        public string NomeClienteMaisComprou { get; set; }
        public string NomeClienteMenosComprou { get; set; }
        public decimal ValorTotalVendas { get; set; }
        public int QuantidadeTotalProdutosVendidos { get; set; }
    }
}
