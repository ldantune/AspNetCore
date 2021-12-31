using Gerenciador_Condominios.BLL.Models;
using Gerenciador_Condominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Condominios.DAL.Repositorios
{
    public class FuncaoRepositorio : RepositorioGenerico<Funcao>, IFuncaoRepositorio
    {
        private readonly AppDbContext _contexto;
        public FuncaoRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
