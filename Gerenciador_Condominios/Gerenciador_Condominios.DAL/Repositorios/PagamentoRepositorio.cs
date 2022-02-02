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
    public class PagamentoRepositorio : RepositorioGenerico<Pagamento>, IPagamentoRepositorio
    {
        private readonly AppDbContext _contexto;
        public PagamentoRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Pagamento>> PegarPagamentoPorUsuario(string usuarioId)
        {
            try
            {
                return await _contexto.Pagamentos.Include(p => p.Aluguel).ThenInclude(p => p.Mes)
                    .Where(p => p.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
