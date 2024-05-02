using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restuarant.Areas.Admin.ViewModels;
using Restuarant.Models;
using Restuarant.Models.Repositories;
using System.Security.Claims;

namespace Restuarant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MasterCategoryMenuController : Controller
    {
        public IRepository<MasterCategoryMenu> categoryMenu { get; set; }
        public MasterCategoryMenuController(IRepository<MasterCategoryMenu> _categoryMenu)
        {
            categoryMenu = _categoryMenu;
        }
        // GET: MasterCategoryMenuController
        public ActionResult Index(int idDelete)
        {
            if (idDelete != 0)
            {
                MasterCategoryMenu obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                categoryMenu.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<MasterCategoryMenu> dataList = categoryMenu.View();
            IList<MasterCategoryMenuModel> dataModelList = new List<MasterCategoryMenuModel>();
            foreach (var data in dataList)
            {
                MasterCategoryMenuModel dataModel = new MasterCategoryMenuModel
                {
                    MasterCategoryMenuId=data.MasterCategoryMenuId,
                    MasterCategoryMenuName=data.MasterCategoryMenuName,
                    IsActive=data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
                return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            categoryMenu.Active(id, new MasterCategoryMenu()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterCategoryMenuController/Create
        public ActionResult Create()
        {
            MasterCategoryMenuModel dataModel = new MasterCategoryMenuModel();
            return View(dataModel);
        }

        // POST: MasterCategoryMenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterCategoryMenuModel collection)
        {
            try
            {
                if (categoryMenu.View().Where(x => x.MasterCategoryMenuName.ToUpper()
                == collection.MasterCategoryMenuName.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return View(collection);
                }
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", errorMessage: "Required Field");
                    return View();
                }
                MasterCategoryMenu data = new MasterCategoryMenu()
                {
                    MasterCategoryMenuName=collection.MasterCategoryMenuName,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                categoryMenu.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterCategoryMenuController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = categoryMenu.Find(id);
            var obj = new MasterCategoryMenuModel
            {
                MasterCategoryMenuId = data.MasterCategoryMenuId,
                MasterCategoryMenuName = data.MasterCategoryMenuName,
                IsActive=data.IsActive,
            };
            return View(obj);
        }

        // POST: MasterCategoryMenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterCategoryMenuModel collection)
        {
            try
            {
                var data = categoryMenu.Find(id);
                data.MasterCategoryMenuName = collection.MasterCategoryMenuName;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                categoryMenu.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
