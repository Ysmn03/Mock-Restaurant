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
    public class MasterItemMenuController : Controller
    {
        // GET: MasterItemMenuController
        IRepository<MasterItemMenu> itemMenu { get; set; }
        IRepository<MasterCategoryMenu> categoryMenu { get; set; }
        public IWebHostEnvironment host { get; set; }
        public MasterItemMenuController(IRepository<MasterItemMenu> _itemMenu, IRepository<MasterCategoryMenu> _categoryMenu, IWebHostEnvironment _host)
        {
            itemMenu = _itemMenu;
            categoryMenu = _categoryMenu;
            host = _host;
        }
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                MasterItemMenu obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                itemMenu.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<MasterItemMenu> dataList = itemMenu.View();
            IList<MasterItemMenuModel> dataModelList = new List<MasterItemMenuModel>();
            foreach (var data in dataList)
            {
                MasterItemMenuModel dataModel = new MasterItemMenuModel
                {
                    MasterCategoryMenuId = data.MasterCategoryMenuId,
                    MasterItemMenuBreef = data.MasterItemMenuBreef,
                    MasterItemMenuDesc = data.MasterItemMenuDesc,
                    MasterItemMenuDate = data.MasterItemMenuDate,
                    MasterItemMenuPrice = data.MasterItemMenuPrice,
                    MasterItemMenuTitle = data.MasterItemMenuTitle,
                    MasterItemMenuImageUrl = data.MasterItemMenuImageUrl,
                    MasterItemMenuId = data.MasterItemMenuId,
                    MasterCategoryMenu = categoryMenu.Find((int)data.MasterCategoryMenuId),
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }

        public ActionResult Active(int id)
        {
            itemMenu.Active(id, new MasterItemMenu()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterItemMenuController/Create
        public ActionResult Create()
        {
            ViewBag.ListCategory = categoryMenu.View();
            MasterItemMenuModel dataModel = new MasterItemMenuModel();
            return View(dataModel);
        }

        // POST: MasterItemMenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterItemMenuModel collection)
        {
            try
            {
                string ImageName = "";
                if (collection.File != null)
                {
                    string ImagePath = Path.Combine(host.WebRootPath, "images");
                    FileInfo fn = new FileInfo(collection.File.FileName);
                    ImageName = "Image" + Guid.NewGuid() + fn.Extension;
                    string FullPath = Path.Combine(ImagePath, ImageName);
                    collection.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                MasterItemMenu data = new MasterItemMenu()
                {
                    MasterItemMenuId = collection.MasterItemMenuId,
                    MasterItemMenuBreef = collection.MasterItemMenuBreef,
                    MasterItemMenuDesc = collection.MasterItemMenuDesc,
                    MasterItemMenuDate = collection.MasterItemMenuDate,
                    MasterItemMenuPrice = collection.MasterItemMenuPrice,
                    MasterItemMenuTitle = collection.MasterItemMenuTitle,
                    MasterItemMenuImageUrl = ImageName,
                    MasterCategoryMenuId = collection.MasterCategoryMenuId,
                    MasterCategoryMenu=collection.MasterCategoryMenu,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                itemMenu.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterItemMenuController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListCategory = categoryMenu.View();
            var data = itemMenu.Find(id);
            //data.MasterCategoryMenu = categoryMenu.Find((int)data.MasterCategoryMenuId);
            var obj = new MasterItemMenuModel
            {
                MasterItemMenuId = data.MasterItemMenuId,
                MasterItemMenuBreef = data.MasterItemMenuBreef,
                MasterItemMenuDesc = data.MasterItemMenuDesc,
                MasterItemMenuDate = data.MasterItemMenuDate,
                MasterItemMenuPrice = data.MasterItemMenuPrice,
                MasterItemMenuTitle = data.MasterItemMenuTitle,
                MasterItemMenuImageUrl = data.MasterItemMenuImageUrl,
                MasterCategoryMenuId = data.MasterCategoryMenuId,
                MasterCategoryMenu=data.MasterCategoryMenu,
                IsActive = data.IsActive,
            };
            return View(obj);
        }

        // POST: MasterItemMenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterItemMenuModel collection)
        {
            try
            {
                string ImageName = "";
                if (collection.File != null)
                {
                    string ImagePath = Path.Combine(host.WebRootPath, "images");
                    FileInfo fn = new FileInfo(collection.File.FileName);
                    ImageName = "Image" + Guid.NewGuid() + fn.Extension;
                    string FullPath = Path.Combine(ImagePath, ImageName);
                    collection.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                var data = itemMenu.Find(id);
                data.MasterItemMenuBreef = collection.MasterItemMenuBreef;
                data.MasterItemMenuDesc = collection.MasterItemMenuDesc;
                data.MasterItemMenuDate = collection.MasterItemMenuDate;
                data.MasterItemMenuPrice = collection.MasterItemMenuPrice;
                data.MasterItemMenuTitle = collection.MasterItemMenuTitle;
                data.MasterItemMenuImageUrl = (ImageName == "") ? collection.MasterItemMenuImageUrl : ImageName;
                data.MasterCategoryMenuId = collection.MasterCategoryMenuId;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                itemMenu.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
