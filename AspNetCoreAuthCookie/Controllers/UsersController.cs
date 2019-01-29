using AspNetCoreAuthCookie.Data;
using AspNetCoreAuthCookie.Models;
using AspNetCoreAuthCookie.Models.ViewModels;
using AspNetCoreAuthCookie.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

            if (_context.Users.Count(u => u.UserName == user.UserName) > 0)
            {
                ModelState.AddModelError("UserName", "Esse login já está em uso");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.Password = Hash.GenerateHash(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();
                TempData["MessageUser"] = "Usuário cadastrado com sucesso";
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

            User users = _context.Users.Find(user.UserId);

            if (!CheckUser(user.UserName, user.UserId))
            {
                users.UserName = user.UserName;
                users.UserTypes = user.UserTypes;
                users.ActiveUser = user.ActiveUser;            
                _context.Update(users);
                await _context.SaveChangesAsync();
                TempData["MessageUser"] = "Usuário atualizado com sucesso";
            }
            else
            {
                ModelState.AddModelError("UserName", "Este Usuário já está em uso");
                return View(user);
            }
            return RedirectToAction(nameof(Index));

            //// Ultilizando IsModifie = false e closed-fixed no frond end
            //if (id != user.UserId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    if (!CheckUser(user.UserName, user.UserId))
            //    {
            //        _context.Update(user).State = EntityState.Modified;
            //        _context.Update(user).Property(p => p.Password).IsModified = false;
            //        await _context.SaveChangesAsync();
            //        TempData["MessageUser"] = "Usúario atualizado com sucesso";
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("UserName", "Este Usuario já está em uso");
            //        return View(user);
            //    }              
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(user);
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
            TempData["MessageDeletedUser"] = "Usuário Deletado!!!";
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
            };

            _context.Add(NewUser);
            await _context.SaveChangesAsync();
            TempData["RegisteredUser"] = "Cadastro realizado com sucesso. Efetue login.";
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
            TempData["PasswordChanged"] = "Senha alterada com sucesso";
            return RedirectToAction("Index", "Home");
        }
              
        
        // GET: Users/EditPassword/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> EditPassword(int? id)
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


        // POST: Users/EditPassword/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(int id, User user)
        {
            // validar senha com mesmo de sem caracteres
            if (user.Password == null || user.Password.Length < 6)
            {
                return View(user);
            }           

            User users = _context.Users.Find(user.UserId);
            users.Password = Hash.GenerateHash(user.Password);
            _context.Update(users);
            await _context.SaveChangesAsync();
            TempData["MessageUser"] = "Senha alterada com sucesso";
            return RedirectToAction(nameof(Index));                   
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        public bool CheckUser(string name, int id)
        {
            var DoesExistUser = (from u in _context.Users
                                 where u.UserName == name
                                 where u.UserId != id
                                 select u).FirstOrDefault();
            if (DoesExistUser != null)
                return true;
            else
                return false;
        }
    }
}
