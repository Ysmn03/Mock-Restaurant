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
    public class MasterSliderController : Controller
    {
        // GET: MasterSliderController
        public IRepository<MasterSlider> slider { get; set; }
        public IWebHostEnvironment host { get; set; }
        public MasterSliderController(IRepository<MasterSlider> _slider, IWebHostEnvironment _host)
        {
            slider = _slider;
            host = _host;
        }
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                MasterSlider obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                slider.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));

            }
            IList<MasterSlider> dataList = slider.View();
            IList<MasterSliderModel> dataModelList = new List<MasterSliderModel>();
            foreach (var data in dataList)
            {
                MasterSliderModel dataModel = new MasterSliderModel
                {
                    MasterSliderBreef = data.MasterSliderBreef,
                    MasterSliderDesc = data.MasterSliderDesc,
                    MasterSliderId = data.MasterSliderId,
                    MasterSliderImageUrl = data.MasterSliderImageUrl,
                    MasterSliderTitle = data.MasterSliderTitle,
                    MasterSliderUrl = data.MasterSliderUrl,
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            slider.Active(id, new MasterSlider()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
        // GET: MasterSliderController/Create
        public ActionResult Create()
        {
            MasterSliderModel dataModel = new MasterSliderModel();
            return View(dataModel);
        }

        // POST: MasterSliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSliderModel collection)
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
                MasterSlider data = new MasterSlider
                {
                    MasterSliderBreef = collection.MasterSliderBreef,
                    MasterSliderDesc = collection.MasterSliderDesc,
                    MasterSliderId = collection.MasterSliderId,
                    MasterSliderImageUrl = ImageName,
                    MasterSliderTitle = collection.MasterSliderTitle,
                    MasterSliderUrl = collection.MasterSliderUrl,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                slider.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterSliderController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = slider.Find(id);
            var obj = new MasterSliderModel
            {
                MasterSliderBreef = data.MasterSliderBreef,
                MasterSliderDesc = data.MasterSliderDesc,
                MasterSliderId = data.MasterSliderId,
                MasterSliderImageUrl = data.MasterSliderImageUrl,
                MasterSliderTitle = data.MasterSliderTitle,
                MasterSliderUrl = data.MasterSliderUrl,
                IsActive = data.IsActive
            };
            return View(obj);
        }

        // POST: MasterSliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterSliderModel collection)
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
                var data = slider.Find(id);
                data.MasterSliderBreef = collection.MasterSliderBreef;
                data.MasterSliderDesc = collection.MasterSliderDesc;
                data.MasterSliderImageUrl = (ImageName == "") ? collection.MasterSliderImageUrl : ImageName;
                data.MasterSliderTitle = collection.MasterSliderTitle;
                data.MasterSliderUrl = collection.MasterSliderUrl;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                slider.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
