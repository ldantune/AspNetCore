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
    public class VeiculoRepositorio : RepositorioGenerico<Veiculo>, IVeiculoRepositorio
    {
        private readonly AppDbContext _contexto;
        public VeiculoRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Veiculo>> PegarVeiculosPorUsuario(string usuarioId)
        {
            try
            {
                return await _contexto.Veiculos.Where(v => v.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
