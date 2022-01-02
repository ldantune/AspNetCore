﻿using Gerenciador_Condominios.BLL.Models;
using Gerenciador_Condominios.DAL.Interfaces;
using Gerenciador_Condominios.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_Condominios.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IFuncaoRepositorio _funcaoRepositorio;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, IFuncaoRepositorio funcaoRepositorio, IWebHostEnvironment webHostEnvironment)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _funcaoRepositorio = funcaoRepositorio;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {

            return View(await _usuarioRepositorio.PegarTodos());
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = "~/Imagens/" + nomeFoto;
                    }
                }

                Usuario usuario = new Usuario();
                IdentityResult usuarioCriado;

                if (_usuarioRepositorio.VeririfcarSeExisteRegistro() == 0)
                {
                    usuario.UserName = model.NomeUsuario;
                    usuario.NomeCompleto = model.NomeCompleto;
                    usuario.CPF = model.CPF;
                    usuario.Email = model.Email;
                    usuario.PhoneNumber = model.Telefone;
                    usuario.Foto = model.Foto;
                    usuario.PrimeiroAcesso = false;
                    usuario.Status = StatusConta.Aprovado;

                    usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                    if (usuarioCriado.Succeeded)
                    {
                        await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, "Administrador");
                        await _usuarioRepositorio.LogarUsuario(usuario, false);
                        return RedirectToAction("Index", "Usuarios");
                    }

                }

                usuario.UserName = model.NomeUsuario;
                usuario.NomeCompleto = model.NomeCompleto;
                usuario.CPF = model.CPF;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Foto = model.Foto;
                usuario.PrimeiroAcesso = true;
                usuario.Status = StatusConta.Analisando;

                usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                {
                    return View("Analise", usuario.UserName);
                }

                else
                {
                    foreach (IdentityError erro in usuarioCriado.Errors)
                    {
                        ModelState.AddModelError("", erro.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _usuarioRepositorio.DeslogarUsuario();
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(model.Email);

                if (usuario != null)
                {
                    if (usuario.Status == StatusConta.Analisando)
                    {
                        return View("Analise", usuario.UserName);
                    }
                    else if (usuario.Status == StatusConta.Reprovado)
                    {
                        return View("Repovado", usuario.UserName);
                    }
                    else if (usuario.PrimeiroAcesso == true)
                    {
                        return RedirectToAction(nameof(RedefinirSenha), usuario);
                    }
                    else
                    {
                        PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
                        if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) != PasswordVerificationResult.Failed)
                        {
                            await _usuarioRepositorio.LogarUsuario(usuario, false);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Usuário e/ou senhas inválidos");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuário e/ou senhas inválidos");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _usuarioRepositorio.DeslogarUsuario();
            return RedirectToAction("Login");
        }

        public IActionResult Analise(string nome)
        {
            return View(nome);
        }

        public IActionResult Reprovado(string nome)
        {
            return View(nome);
        }

        public async Task<JsonResult> AprovarUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepositorio.PegarPeloId(usuarioId);
            usuario.Status = StatusConta.Aprovado;
            await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, "Morador");
            await _usuarioRepositorio.AtualizarUsuario(usuario);

            return Json(true);
        }

        public async Task<JsonResult> ReprovarUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepositorio.PegarPeloId(usuarioId);
            usuario.Status = StatusConta.Reprovado;
            await _usuarioRepositorio.AtualizarUsuario(usuario);

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GerenciarUsuario(string usuarioId, string nomeCompleto)
        {
            if (usuarioId == null)
                return NotFound();

            TempData["usuarioId"] = usuarioId;
            ViewBag.NomeCompleto = nomeCompleto;
            Usuario usuario = await _usuarioRepositorio.PegarPeloId(usuarioId);

            if (usuario == null)
                return NotFound();

            List<FuncaoUsuarioViewModel> viewModel = new List<FuncaoUsuarioViewModel>();

            foreach(Funcao funcao in await _funcaoRepositorio.PegarTodos())
            {
                FuncaoUsuarioViewModel model = new FuncaoUsuarioViewModel
                {
                    FuncaoId = funcao.Id,
                    Nome = funcao.Name,
                    Descricao = funcao.Descricao
                };

                if(await _usuarioRepositorio.VerificarSeUsuarioEstaEmFuncao(usuario, funcao.Name))
                {
                    model.isSelecionado = true;
                }
                else
                {
                    model.isSelecionado = false;
                }

                viewModel.Add(model);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GerenciarUsuario(List<FuncaoUsuarioViewModel> model)
        {
            string usuarioId = TempData["usuarioId"].ToString();

            Usuario usuario = await _usuarioRepositorio.PegarPeloId(usuarioId);

            if (usuario == null)
                return NotFound();

            IEnumerable<string> funcoes = await _usuarioRepositorio.PegarFuncoesUsuario(usuario);
            IdentityResult resultado = await _usuarioRepositorio.RemoverFuncoesUsuario(usuario, funcoes);

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funções do usuário");
                return View("GerenciarUsuario", usuarioId);
            }

            resultado = await _usuarioRepositorio.IncluirUsuarioEmFuncoes(usuario,
                model.Where(x => x.isSelecionado == true).Select(x => x.Nome));

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possível atualizar as funções do usuário");
                return View("GerenciarUsuario", usuarioId);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MinhasInformacoes()
        {
            return View(await _usuarioRepositorio.PegarUsuarioPeloNome(User));
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(string id)
        {
            Usuario usuario = await _usuarioRepositorio.PegarPeloId(id);

            if (usuario == null)
                return NotFound();

            AtualizarViewModel model = new AtualizarViewModel
            {
                UsuarioId = usuario.Id,
                NomeUsuario = usuario.UserName,
                NomeCompleto = usuario.NomeCompleto,
                CPF = usuario.CPF,
                Email = usuario.Email,
                Foto = usuario.Foto,
                Telefone = usuario.PhoneNumber
            };

            TempData["Foto"] = usuario.Foto;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(AtualizarViewModel viewModel, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        viewModel.Foto = "~/Imagens/" + nomeFoto;
                    }
                }

                else
                    viewModel.Foto = TempData["Foto"].ToString();

                Usuario usuario = await _usuarioRepositorio.PegaUsuarioPeloId(viewModel.UsuarioId);
                usuario.UserName = viewModel.NomeUsuario;
                usuario.NomeCompleto = viewModel.NomeCompleto;
                usuario.CPF = viewModel.CPF;
                usuario.PhoneNumber = viewModel.Telefone;
                usuario.Foto = viewModel.Foto;
                usuario.Email = viewModel.Email;

                await _usuarioRepositorio.AtualizarUsuario(usuario);

                TempData["Atualizacao"] = "Registro atualizado";

                if (await _usuarioRepositorio.VerificarSeUsuarioEstaEmFuncao(usuario, "Administrador") ||
                    await _usuarioRepositorio.VerificarSeUsuarioEstaEmFuncao(usuario, "Sindico"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(MinhasInformacoes));
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RedefinirSenha(Usuario usuario)
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = usuario.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(model.Email);
                model.Senha = _usuarioRepositorio.CodificarSenha(usuario, model.Senha);
                usuario.PasswordHash = model.Senha;
                usuario.PrimeiroAcesso = false;
                await _usuarioRepositorio.AtualizarUsuario(usuario);
                await _usuarioRepositorio.LogarUsuario(usuario, false);

                return RedirectToAction(nameof(MinhasInformacoes));
            }

            return View(model);
        }

    }
}
