using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreAuthCookie.Data;
using AspNetCoreAuthCookie.Models;
using AspNetCoreAuthCookie.Services;
using AspNetCoreAuthCookie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AspNetCoreAuthCookie.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {                  
            if (ModelState.IsValid)
            {
                user.Password = Hash.GenerateHash(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = Hash.GenerateHash(user.Password);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewmodel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            if (_context.Users.Count(u => u.UserName == viewmodel.UserName) > 0)
            {
                ModelState.AddModelError("UserName", "Esse login já está em uso");
                return View(viewmodel);
            }

            User NewUser = new User
            {
                UserName = viewmodel.UserName,
                Password = Hash.GenerateHash(viewmodel.Password),
                ActiveUser = viewmodel.ActiveUser,
                UserTypes = viewmodel.UserTypes,
                RememberMe = viewmodel.RememberMe,
            };

            _context.Add(NewUser);
            await _context.SaveChangesAsync();

            TempData["RegistrationMessage"] = "Cadastro realizado com sucesso. Efetue login.";

            return RedirectToAction("Login", "Account");
        }

        // GET: Users/Register
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewmodel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "UserName").Value;

            var user = _context.Users.FirstOrDefault(u => u.UserName == login);

            if (Hash.GenerateHash(viewmodel.CurrentPassword) != user.Password)
            {
                ModelState.AddModelError("CurrentPassword", "Senha incorreta");
                return View();
            }

            user.Password = Hash.GenerateHash(viewmodel.NewPassword);
            _context.Update(user);
            await _context.SaveChangesAsync();

            TempData["RegistrationMessage"] = "Cadastro realizado com sucesso. Efetue login.";

            return RedirectToAction("Index", "Home");
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
