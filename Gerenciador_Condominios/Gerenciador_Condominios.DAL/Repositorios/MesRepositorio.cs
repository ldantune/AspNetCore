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
    public class MesRepositorio : RepositorioGenerico<Mes>, IMesRepositorio
    {
        private readonly AppDbContext _contexto;
        public MesRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public new async Task<IEnumerable<Mes>> PegarTodos()
        {
            try
            {
                return await _contexto.Meses.OrderBy(m => m.MesId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
