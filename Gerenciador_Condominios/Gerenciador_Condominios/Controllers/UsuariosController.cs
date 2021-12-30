using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_Condominios.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
    }
}
