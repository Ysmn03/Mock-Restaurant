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
    public class MasterContactUsInformationController : Controller
    {
        // GET: MasterContactUsInformationController
        public IRepository<MasterContactUsInformation> contactUsInfo { get; set; }
        public MasterContactUsInformationController(IRepository<MasterContactUsInformation> _contactUsInfo)
        {
            contactUsInfo = _contactUsInfo;
        }
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                MasterContactUsInformation obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                contactUsInfo.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));

            }
            IList<MasterContactUsInformation> dataList = contactUsInfo.View();
            IList<MasterContactUsInformationModel> dataModelList = new List<MasterContactUsInformationModel>();
            foreach (var data in dataList)
            {
                MasterContactUsInformationModel dataModel = new MasterContactUsInformationModel
                {
                    MasterContactUsInformationId = data.MasterContactUsInformationId,
                    MasterContactUsInformationIdesc = data.MasterContactUsInformationIdesc,
                    MasterContactUsInformationImageUrl = data.MasterContactUsInformationImageUrl,
                    MasterContactUsInformationRedirect = data.MasterContactUsInformationRedirect,
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            contactUsInfo.Active(id, new MasterContactUsInformation()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterContactUsInformationController/Create
        public ActionResult Create()
        {
            MasterContactUsInformationModel dataModel = new MasterContactUsInformationModel();
            return View(dataModel);
        }

        // POST: MasterContactUsInformationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterContactUsInformationModel collection)
        {
            try
            {
                MasterContactUsInformation data = new MasterContactUsInformation()
                {
                    MasterContactUsInformationId = collection.MasterContactUsInformationId,
                    MasterContactUsInformationIdesc = collection.MasterContactUsInformationIdesc,
                    MasterContactUsInformationRedirect = collection.MasterContactUsInformationRedirect,
                    MasterContactUsInformationImageUrl = collection.MasterContactUsInformationImageUrl,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                contactUsInfo.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterContactUsInformationController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = contactUsInfo.Find(id);
            var obj = new MasterContactUsInformationModel
            {
                MasterContactUsInformationId = data.MasterContactUsInformationId,
                MasterContactUsInformationIdesc = data.MasterContactUsInformationIdesc,
                MasterContactUsInformationRedirect = data.MasterContactUsInformationRedirect,
                MasterContactUsInformationImageUrl = data.MasterContactUsInformationImageUrl,
                IsActive = data.IsActive
            };
            return View(obj);
        }

        // POST: MasterContactUsInformationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterContactUsInformationModel collection)
        {
            try
            {
                var data = contactUsInfo.Find(id);
                data.MasterContactUsInformationIdesc = collection.MasterContactUsInformationIdesc;
                data.MasterContactUsInformationRedirect = collection.MasterContactUsInformationRedirect;
                data.MasterContactUsInformationImageUrl = collection.MasterContactUsInformationImageUrl;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                contactUsInfo.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
