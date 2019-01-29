using AspNetCoreAuthCookie.Data;
using AspNetCoreAuthCookie.Models.ViewModels;
using AspNetCoreAuthCookie.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreAuthCookie.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: 
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == viewmodel.UserName);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "Usurio incorreto");
                return View(viewmodel);
            }

            if (user.Password != Hash.GenerateHash(viewmodel.Password))
            {
                ModelState.AddModelError("Password", "Senha incorreta");
                return View(viewmodel);
            }

            // Validação de usuarios ativos e desativados
            if (user.ActiveUser == false)
            {
                ModelState.AddModelError("ActiveUser", "Usuario não tem permissão");
                return View(viewmodel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserName", user.UserName),
                new Claim(ClaimTypes.Role, user.UserTypes.ToString()) //pega a String definida da model user 
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                 

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            });
                       
            return RedirectToAction("Index", "HOME");
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData["MessageLogout"] = "Usuário deslogado com sucesso!!!";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
