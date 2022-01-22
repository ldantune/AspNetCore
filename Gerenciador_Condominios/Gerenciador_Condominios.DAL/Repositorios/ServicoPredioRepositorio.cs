using Gerenciador_Condominios.BLL.Models;
using Gerenciador_Condominios.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Condominios.DAL.Repositorios
{
    public class ServicoPredioRepositorio : RepositorioGenerico<ServicoPredio>, IServicoPredioRepositorio
    {
        public ServicoPredioRepositorio(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
