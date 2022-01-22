using Gerenciador_Condominios.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Condominios.DAL.Interfaces
{
    public interface IEventoRepositorio : IRepositorioGenerico<Evento>
    {
        Task<IEnumerable<Evento>> PegarEventosPeloId(string usuarioId);
    }
}
