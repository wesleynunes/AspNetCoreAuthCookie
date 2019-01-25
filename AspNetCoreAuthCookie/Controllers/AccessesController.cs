using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthCookie.Controllers
{

    //Controller para testar o acesso de cada tipo de usúario
    [Authorize]
    public class AccessesController : Controller
    {
        [Authorize(Roles = "Admin")]  // somente usuarios admin podem acessar
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Manager")] //somente usuarios admin e manager podem acessar
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Users")] // somente usuarios admim e users podem acessar
        public IActionResult Users()
        {
            return View();
        }
    }
}