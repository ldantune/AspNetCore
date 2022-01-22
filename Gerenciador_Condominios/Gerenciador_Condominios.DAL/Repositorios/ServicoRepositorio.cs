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
    public class ServicoRepositorio : RepositorioGenerico<Servico>, IServicoRepositorio
    {
        private readonly AppDbContext _contexto;
        public ServicoRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Servico>> PegarServicosPeloUsuario(string usuarioId)
        {
            try
            {
                return await _contexto.Servicos.Where(s => s.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
