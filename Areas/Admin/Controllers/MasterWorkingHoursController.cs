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
    public class MasterWorkingHoursController : Controller
    {
        // GET: MasterWorkingHoursController
        public IRepository<MasterWorkingHours>workingHours {  get; set; }
        public MasterWorkingHoursController(IRepository<MasterWorkingHours> _workingHours)
        {
            workingHours = _workingHours;
        }
        public ActionResult Index(int idDelete)
        {
            if (idDelete != 0)
            {
                MasterWorkingHours obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                workingHours.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<MasterWorkingHours> dataList = workingHours.View();
            IList<MasterWorkingHoursModel> dataModelList = new List<MasterWorkingHoursModel>();
            foreach(var data in dataList)
            {
                MasterWorkingHoursModel dataModel = new MasterWorkingHoursModel
                {
                    MasterWorkingHoursId=data.MasterWorkingHoursId,
                    MasterWorkingHoursIdName=data.MasterWorkingHoursIdName,
                    MasterWorkingHoursIdTimeFormTo=data.MasterWorkingHoursIdTimeFormTo,
                    IsActive=data.IsActive
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }

        public ActionResult Active(int id)
        {
            workingHours.Active(id, new MasterWorkingHours()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
        // GET: MasterWorkingHoursController/Create
        public ActionResult Create()
        {
            MasterWorkingHoursModel dataModel = new MasterWorkingHoursModel();
            return View(dataModel);
        }

        // POST: MasterWorkingHoursController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterWorkingHoursModel collection)
        {
            try
            {
                if (workingHours.View().Where(x => x.MasterWorkingHoursIdName.ToUpper()
                == collection.MasterWorkingHoursIdName.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return View(collection);
                }
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", errorMessage: "Required Field");
                    return View();
                }
                MasterWorkingHours data = new MasterWorkingHours()
                {
                    MasterWorkingHoursId=collection.MasterWorkingHoursId,
                    MasterWorkingHoursIdName=collection.MasterWorkingHoursIdName,
                    MasterWorkingHoursIdTimeFormTo=collection.MasterWorkingHoursIdTimeFormTo,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                workingHours.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterWorkingHoursController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = workingHours.Find(id);
            var obj = new MasterWorkingHoursModel
            {
                MasterWorkingHoursId = data.MasterWorkingHoursId,
                MasterWorkingHoursIdName = data.MasterWorkingHoursIdName,
                MasterWorkingHoursIdTimeFormTo = data.MasterWorkingHoursIdTimeFormTo,
                IsActive=data.IsActive,
            };
            return View(obj);
        }

        // POST: MasterWorkingHoursController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterWorkingHoursModel collection)
        {
            try
            {
                var data=workingHours.Find(id);
                data.MasterWorkingHoursIdName = collection.MasterWorkingHoursIdName;
                data.MasterWorkingHoursIdTimeFormTo = collection.MasterWorkingHoursIdTimeFormTo;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                workingHours.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
