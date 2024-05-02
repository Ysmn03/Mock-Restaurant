using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restuarant.Areas.Admin.ViewModels;
using Restuarant.Models;
using Restuarant.Models.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Restuarant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MasterMenuController : Controller
    {
        // GET: MasterMenuController
        public IRepository<MasterMenu> masterMenu { get;}
        public MasterMenuController(IRepository<MasterMenu> _masterMenu)
        {
            masterMenu = _masterMenu;
        }
        public ActionResult Index(int idDelete)
        {
            if(idDelete !=0)
            {
                MasterMenu obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                masterMenu.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<MasterMenu> dataList = masterMenu.View();
            IList<MasterMenuModel>dataModelList= new List<MasterMenuModel>();
            foreach (var data in dataList)
            {
                MasterMenuModel dataModel = new MasterMenuModel
                {
                    MasterMenuId=data.MasterMenuId,
                    MasterMenuName=data.MasterMenuName,
                    MasterMenuUrl=data.MasterMenuUrl,
                    IsActive=data.IsActive
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            masterMenu.Active(id, new MasterMenu()
            {
                EditDate= DateTime.UtcNow,
                EditUser=User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Create(int id)
        {
            MasterMenuModel dataModel=new MasterMenuModel();
            return View(dataModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterMenuModel collection)
        {
            try
            {
                if (masterMenu.View().Where(x => x.MasterMenuName.ToUpper()
                == collection.MasterMenuName.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return View(collection);
                }
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", errorMessage: "Required Field");
                    return View();
                }
                MasterMenu data = new MasterMenu()
                {
                    MasterMenuName = collection.MasterMenuName,
                    MasterMenuUrl = collection.MasterMenuUrl,
                    CreateDate = DateTime.UtcNow,
                    CreateUser=User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser= User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive =true,
                    IsDelete=false
                };
                masterMenu.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var data = masterMenu.Find(id);
            var obj = new MasterMenuModel
            {
                MasterMenuId = data.MasterMenuId,
                MasterMenuUrl=data.MasterMenuUrl,
                MasterMenuName=data.MasterMenuName,
                IsActive = data.IsActive,
            };
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterMenuModel collection)
        {
            try
            {
                var data = masterMenu.Find(id);

                // Update the properties of the existing entity
                data.MasterMenuName = collection.MasterMenuName;
                data.MasterMenuUrl = collection.MasterMenuUrl;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                masterMenu.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return View();
            }
        }
    }
}
