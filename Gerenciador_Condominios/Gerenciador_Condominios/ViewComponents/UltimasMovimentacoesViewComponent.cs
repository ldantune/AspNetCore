using Gerenciador_Condominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_Condominios.ViewComponents
{
    public class UltimasMovimentacoesViewComponent : ViewComponent
    {
        private readonly IHistoricoRecursosRepositorio _historicoRecursosRepositorio;

        public UltimasMovimentacoesViewComponent(IHistoricoRecursosRepositorio historicoRecursosRepositorio)
        {
            _historicoRecursosRepositorio = historicoRecursosRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _historicoRecursosRepositorio.PegarUltimasMovimentacoes());
        }
    }
}
