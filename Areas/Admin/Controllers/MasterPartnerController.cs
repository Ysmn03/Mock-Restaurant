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
    public class MasterPartnerController : Controller
    {
        // GET: MasterPartnerController
        public IRepository<MasterPartner> partner { get; set; }
        public IWebHostEnvironment host { get; set; }
        public MasterPartnerController(IRepository<MasterPartner> _partner, IWebHostEnvironment _host)
        {
            partner = _partner;
            host = _host;
        }
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                MasterPartner obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                partner.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));

            }
            IList<MasterPartner> dataList = partner.View();
            IList<MasterPartnerModel> dataModelList = new List<MasterPartnerModel>();
            foreach (var data in dataList)
            {
                MasterPartnerModel dataModel = new MasterPartnerModel
                {
                    MasterPartnerId = data.MasterPartnerId,
                    MasterPartnerWebsiteUrl = data.MasterPartnerWebsiteUrl,
                    MasterPartnerName = data.MasterPartnerName,
                    MasterPartnerLogoImageUrl = data.MasterPartnerLogoImageUrl,
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            partner.Active(id, new MasterPartner()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterPartnerController/Create
        public ActionResult Create()
        {
            MasterPartnerModel dataModel = new MasterPartnerModel();
            return View(dataModel);
        }

        // POST: MasterPartnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterPartnerModel collection)
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
                MasterPartner data = new MasterPartner()
                {
                    MasterPartnerId = collection.MasterPartnerId,
                    MasterPartnerWebsiteUrl = collection.MasterPartnerWebsiteUrl,
                    MasterPartnerName = collection.MasterPartnerName,
                    MasterPartnerLogoImageUrl = ImageName,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                partner.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterPartnerController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = partner.Find(id);
            var obj = new MasterPartnerModel
            {
                MasterPartnerId = data.MasterPartnerId,
                MasterPartnerWebsiteUrl = data.MasterPartnerWebsiteUrl,
                MasterPartnerName = data.MasterPartnerName,
                MasterPartnerLogoImageUrl = data.MasterPartnerLogoImageUrl,
                IsActive = data.IsActive
            };
            return View(obj);
        }

        // POST: MasterPartnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterPartnerModel collection)
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
                var data = partner.Find(id);
                data.MasterPartnerWebsiteUrl = collection.MasterPartnerWebsiteUrl;
                data.MasterPartnerName = collection.MasterPartnerName;
                data.MasterPartnerLogoImageUrl = (ImageName == "") ? collection.MasterPartnerLogoImageUrl : ImageName;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                partner.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
