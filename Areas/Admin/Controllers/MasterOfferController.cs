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
    public class MasterOfferController : Controller
    {
        // GET: MasterOfferController
        public IRepository<MasterOffer> masterOffer { get; }
        public IWebHostEnvironment host { get; set; }
        public MasterOfferController(IWebHostEnvironment _host, IRepository<MasterOffer> _masterOffer)
        {
            masterOffer = _masterOffer;
            host = _host;
        }
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                MasterOffer obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                masterOffer.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));

            }
            IList<MasterOffer> dataList = masterOffer.View();
            IList<MasterOfferModel> dataModelList = new List<MasterOfferModel>();
            foreach (var data in dataList)
            {
                MasterOfferModel dataModel = new MasterOfferModel
                {
                    MasterOfferId = data.MasterOfferId,
                    MasterOfferBreef = data.MasterOfferBreef,
                    MasterOfferDesc = data.MasterOfferDesc,
                    MasterOfferImageUrl = data.MasterOfferImageUrl,
                    MasterOfferTitle = data.MasterOfferTitle,
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            masterOffer.Active(id, new MasterOffer()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Create(int id)
        {
            MasterOfferModel dataModel = new MasterOfferModel();
            return View(dataModel);
        }

        // POST: MasterOfferController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterOfferModel collection)
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
                MasterOffer data = new MasterOffer()
                {
                    MasterOfferTitle = collection.MasterOfferTitle,
                    MasterOfferBreef = collection.MasterOfferBreef,
                    MasterOfferDesc = collection.MasterOfferDesc,
                    MasterOfferId = collection.MasterOfferId,
                    MasterOfferImageUrl = ImageName,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                masterOffer.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterOfferController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = masterOffer.Find(id);
            var obj = new MasterOfferModel
            {
                MasterOfferTitle = data.MasterOfferTitle,
                MasterOfferBreef = data.MasterOfferBreef,
                MasterOfferDesc = data.MasterOfferDesc,
                MasterOfferId = data.MasterOfferId,
                MasterOfferImageUrl = data.MasterOfferImageUrl,
                IsActive = data.IsActive
            };
            return View(obj);
        }

        // POST: MasterOfferController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterOfferModel collection)
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
                var data = masterOffer.Find(id);
                data.MasterOfferTitle = collection.MasterOfferTitle;
                data.MasterOfferBreef = collection.MasterOfferBreef;
                data.MasterOfferDesc = collection.MasterOfferDesc;
                data.MasterOfferImageUrl = (ImageName == "") ? collection.MasterOfferImageUrl : ImageName;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                masterOffer.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
