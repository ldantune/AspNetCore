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
    public class ApartamentoRepositorio : RepositorioGenerico<Apartamento>, IApartamentoRepositorio
    {
        private readonly AppDbContext _contexto;
        public ApartamentoRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Apartamento>> PegarApartamentoPeloUsuario(string usuarioId)
        {
            try
            {
                return await _contexto.Apartamentos
                    .Include(a => a.Morador).Include(a => a.Proprietario)
                    .Where(a => a.MoradorId == usuarioId || a.ProprietarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public new async Task<IEnumerable<Apartamento>> PegarTodos()
        {
            try
            {
                return await _contexto.Apartamentos.Include(a => a.Morador).Include(a => a.Proprietario).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
