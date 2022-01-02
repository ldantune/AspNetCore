using Gerenciador_Condominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_Condominios.ViewComponents
{
    public class VeiculosViewComponent : ViewComponent
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculosViewComponent(IVeiculoRepositorio veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return View(await _veiculoRepositorio.PegarVeiculosPorUsuario(id));
        }
    }
}
