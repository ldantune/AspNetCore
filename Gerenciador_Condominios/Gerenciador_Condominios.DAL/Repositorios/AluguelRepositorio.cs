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
    public class AluguelRepositorio : RepositorioGenerico<Aluguel>, IAluguelRepositorio
    {
        private readonly AppDbContext _contexto;
        public AluguelRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public bool AluguelJaExiste(int mesId, int ano)
        {
            try
            {
                return _contexto.Alugueis.Any(a => a.MesId == mesId && a.Ano == ano);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public new async Task<IEnumerable<Aluguel>> PegarTodos()
        {
            try
            {
                return await _contexto.Alugueis.Include(a => a.Mes).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<int>> PegarTodosAnos()
        {
            try
            {
                return await _contexto.Alugueis.Select(a => a.Ano).Distinct().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
