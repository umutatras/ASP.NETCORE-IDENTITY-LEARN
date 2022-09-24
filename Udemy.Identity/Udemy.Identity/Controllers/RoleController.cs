using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Udemy.Identity.Entities;
using Udemy.Identity.Models;

namespace Udemy.Identity.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var list = _roleManager.Roles.ToList();
            return View(list);
        }
        public IActionResult Create()
        {
            return View(new RoleCreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateModel model)
        {
            var result=await _roleManager.CreateAsync(new AppRole
            {
                CreatedTime=DateTime.Now,
                Name= model.Name
            });
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
