using Gerenciador_Condominios.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Condominios.DAL.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        int VeririfcarSeExisteRegistro();
        Task LogarUsuario(Usuario usuario, bool lembrar);
        Task DeslogarUsuario();
        Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);
        Task IncluirUsuarioEmFuncao(Usuario usuario, string funcao);
        Task<Usuario> PegarUsuarioPeloEmail(string email);
    }
}
