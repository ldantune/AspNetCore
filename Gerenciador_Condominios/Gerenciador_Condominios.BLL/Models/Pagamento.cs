using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Condominios.BLL.Models
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }

        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int AluguelId { get; set; }
        public Aluguel Aluguel { get; set; }

        public DateTime? DataPagamento { get; set; }

        public StatusPagamento Status { get; set; }
    }

    public enum StatusPagamento
    {
        Pago, Pendente, Atrasado
    }
}
