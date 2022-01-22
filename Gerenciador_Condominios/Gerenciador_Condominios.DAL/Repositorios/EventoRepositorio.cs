using Gerenciador_Condominios.BLL.Models;
using Gerenciador_Condominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Condominios.DAL.Repositorios
{
    public class EventoRepositorio : RepositorioGenerico<Evento>, IEventoRepositorio
    {
        private readonly AppDbContext _contexto;

        public EventoRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Evento>> PegarEventosPeloId(string usuarioId)
        {
            try
            {
                return await _contexto.Eventos.Where(e => e.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
