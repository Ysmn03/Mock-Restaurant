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
    public class MasterServicesController : Controller
    {
        public IRepository<MasterServices> services { get; set; }
        public MasterServicesController(IRepository<MasterServices> _services)
        {
            services = _services;
        }
        // GET: MasterServicesController
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                MasterServices obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                services.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));

            }
            IList<MasterServices> dataList = services.View();
            IList<MasterServicesModel> dataModelList = new List<MasterServicesModel>();
            foreach (var data in dataList)
            {
                MasterServicesModel dataModel = new MasterServicesModel
                {
                    MasterServicesId = data.MasterServicesId,
                    MasterServicesDesc = data.MasterServicesDesc,
                    MasterServicesTitle = data.MasterServicesTitle,
                    MasterServicesImage = data.MasterServicesImage,
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }

        public ActionResult Active(int id)
        {
            services.Active(id, new MasterServices()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterServicesController/Create
        public ActionResult Create()
        {
            MasterServicesModel dataModel = new MasterServicesModel();
            return View(dataModel);
        }

        // POST: MasterServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterServicesModel collection)
        {
            try
            {
                MasterServices data = new MasterServices()
                {
                    MasterServicesId = collection.MasterServicesId,
                    MasterServicesDesc = collection.MasterServicesDesc,
                    MasterServicesTitle = collection.MasterServicesTitle,
                    MasterServicesImage = collection.MasterServicesImage,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                services.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterServicesController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = services.Find(id);
            var obj = new MasterServicesModel
            {
                MasterServicesId = data.MasterServicesId,
                MasterServicesDesc = data.MasterServicesDesc,
                MasterServicesTitle = data.MasterServicesTitle,
                MasterServicesImage = data.MasterServicesImage,
                IsActive = data.IsActive
            };
            return View(obj);
        }

        // POST: MasterServicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterServicesModel collection)
        {
            try
            {
                var data = services.Find(id);
                data.MasterServicesDesc = collection.MasterServicesDesc;
                data.MasterServicesTitle = collection.MasterServicesTitle;
                data.MasterServicesImage = collection.MasterServicesImage;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                services.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
