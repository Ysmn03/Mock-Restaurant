using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restuarant.Areas.Admin.ViewModels;
using Restuarant.Models;
using Restuarant.Models.Repositories;
using System.Security.Claims;

namespace Restuarant.Areas.Admin.Controllers
{
    public class ImageHandler
    {
        private string _hostWebRootPath;
        private string _imagePath;

        public ImageHandler(string hostWebRootPath)
        {
            _hostWebRootPath = hostWebRootPath;
            _imagePath = Path.Combine(_hostWebRootPath, "images");
        }

        public string SaveImage(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                FileInfo fileInfo = new FileInfo(imageFile.FileName);
                string imageName = "Image" + Guid.NewGuid() + fileInfo.Extension;
                string fullPath = Path.Combine(_imagePath, imageName);
                imageFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                return imageName;
            }
            return string.Empty;
        }
    }

    [Area("Admin")]
    [Authorize]
    public class SystemSettingController : Controller
    {
        public IRepository<SystemSetting> systemSetting { get; set; }
        public IWebHostEnvironment host { get; set; }
        public SystemSettingController(IRepository<SystemSetting> _systemSetting, 
            IWebHostEnvironment _host)
        {
            systemSetting = _systemSetting;
            host = _host;
        }
        // GET: SystemSettingController
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                SystemSetting obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                systemSetting.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<SystemSetting> dataList = systemSetting.View();
            IList<SystemSettingModel> dataModelList = new List<SystemSettingModel>();
            foreach (var data in dataList)
            {
                SystemSettingModel dataModel = new SystemSettingModel
                {
                    SystemSettingId = data.SystemSettingId,
                    SystemSettingCopyright = data.SystemSettingCopyright,
                    SystemSettingMapLocation = data.SystemSettingMapLocation,
                    SystemSettingWelcomeNoteBreef = data.SystemSettingWelcomeNoteBreef,
                    SystemSettingWelcomeNoteDesc = data.SystemSettingWelcomeNoteDesc,
                    SystemSettingWelcomeNoteTitle = data.SystemSettingWelcomeNoteTitle,
                    SystemSettingLogoImageUrl = data.SystemSettingLogoImageUrl,
                    SystemSettingLogoImageUrl2 = data.SystemSettingLogoImageUrl2,
                    SystemSettingWelcomeNoteUrl = data.SystemSettingWelcomeNoteUrl,
                    SystemSettingWelcomeNoteImageUrl = data.SystemSettingWelcomeNoteImageUrl,
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            systemSetting.Active(id, new SystemSetting()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
        // GET: SystemSettingController/Create
        public ActionResult Create()
        {
            SystemSettingModel dataModel = new SystemSettingModel();
            return View(dataModel);
        }

        // POST: SystemSettingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SystemSettingModel collection)
        {
            try
            {
                //string ImageName1 = "";
                //if (collection.ImageUrl1 != null)
                //{
                //    string ImagePath = Path.Combine(host.WebRootPath, "images");
                //    FileInfo fn = new FileInfo(collection.ImageUrl1.FileName);
                //    ImageName1 = "Image" + Guid.NewGuid() + fn.Extension;
                //    string FullPath = Path.Combine(ImagePath, ImageName1);
                //    collection.ImageUrl1.CopyTo(new FileStream(FullPath, FileMode.Create));
                //}
                //string ImageName2 = "";
                //if (collection.ImageUrl2 != null)
                //{
                //    string ImagePath = Path.Combine(host.WebRootPath, "images");
                //    FileInfo fn = new FileInfo(collection.ImageUrl2.FileName);
                //    ImageName2 = "Image" + Guid.NewGuid() + fn.Extension;
                //    string FullPath = Path.Combine(ImagePath, ImageName2);
                //    collection.ImageUrl2.CopyTo(new FileStream(FullPath, FileMode.Create));
                //}
                //string ImageName3 = "";
                //if (collection.NoteImageUrl != null)
                //{
                //    string ImagePath = Path.Combine(host.WebRootPath, "images");
                //    FileInfo fn = new FileInfo(collection.NoteImageUrl.FileName);
                //    ImageName3 = "Image" + Guid.NewGuid() + fn.Extension;
                //    string FullPath = Path.Combine(ImagePath, ImageName3);
                //    collection.NoteImageUrl.CopyTo(new FileStream(FullPath, FileMode.Create));
                //}
                ImageHandler imageHandler = new ImageHandler(host.WebRootPath);
                string imageName1 = imageHandler.SaveImage(collection.ImageUrl1);
                string imageName2 = imageHandler.SaveImage(collection.ImageUrl2);
                string imageName3 = imageHandler.SaveImage(collection.NoteImageUrl);
                SystemSetting data = new SystemSetting()
                {
                    SystemSettingId = collection.SystemSettingId,
                    SystemSettingCopyright = collection.SystemSettingCopyright,
                    SystemSettingMapLocation = collection.SystemSettingMapLocation,
                    SystemSettingWelcomeNoteBreef = collection.SystemSettingWelcomeNoteBreef,
                    SystemSettingWelcomeNoteDesc = collection.SystemSettingWelcomeNoteDesc,
                    SystemSettingWelcomeNoteTitle = collection.SystemSettingWelcomeNoteTitle,
                    SystemSettingLogoImageUrl = imageName1,
                    SystemSettingLogoImageUrl2 = imageName2,
                    SystemSettingWelcomeNoteUrl = collection.SystemSettingWelcomeNoteUrl,
                    SystemSettingWelcomeNoteImageUrl = imageName3,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                systemSetting.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SystemSettingController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = systemSetting.Find(id);
            var obj = new SystemSettingModel
            {
                SystemSettingId = data.SystemSettingId,
                SystemSettingCopyright = data.SystemSettingCopyright,
                SystemSettingMapLocation = data.SystemSettingMapLocation,
                SystemSettingWelcomeNoteBreef = data.SystemSettingWelcomeNoteBreef,
                SystemSettingWelcomeNoteDesc = data.SystemSettingWelcomeNoteDesc,
                SystemSettingWelcomeNoteTitle = data.SystemSettingWelcomeNoteTitle,
                SystemSettingLogoImageUrl = data.SystemSettingLogoImageUrl,
                SystemSettingLogoImageUrl2 = data.SystemSettingLogoImageUrl2,
                SystemSettingWelcomeNoteUrl = data.SystemSettingWelcomeNoteUrl,
                SystemSettingWelcomeNoteImageUrl = data.SystemSettingWelcomeNoteImageUrl,
                IsActive = data.IsActive
            };
            return View(obj);
        }

        // POST: SystemSettingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SystemSettingModel collection)
        {
            try
            {
                string ImageName1 = "";
                if (collection.ImageUrl1 != null)
                {
                    string ImagePath = Path.Combine(host.WebRootPath, "images");
                    FileInfo fn = new FileInfo(collection.ImageUrl1.FileName);
                    ImageName1 = "Image" + Guid.NewGuid() + fn.Extension;
                    string FullPath = Path.Combine(ImagePath, ImageName1);
                    collection.ImageUrl1.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                string ImageName2 = "";
                if (collection.ImageUrl2 != null)
                {
                    string ImagePath = Path.Combine(host.WebRootPath, "images");
                    FileInfo fn = new FileInfo(collection.ImageUrl2.FileName);
                    ImageName2 = "Image" + Guid.NewGuid() + fn.Extension;
                    string FullPath = Path.Combine(ImagePath, ImageName2);
                    collection.ImageUrl2.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                string ImageName3 = "";
                if (collection.NoteImageUrl != null)
                {
                    string ImagePath = Path.Combine(host.WebRootPath, "images");
                    FileInfo fn = new FileInfo(collection.NoteImageUrl.FileName);
                    ImageName3 = "Image" + Guid.NewGuid() + fn.Extension;
                    string FullPath = Path.Combine(ImagePath, ImageName3);
                    collection.NoteImageUrl.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                var data = systemSetting.Find(id);
                data.SystemSettingCopyright = collection.SystemSettingCopyright;
                data.SystemSettingMapLocation = collection.SystemSettingMapLocation;
                data.SystemSettingWelcomeNoteBreef = collection.SystemSettingWelcomeNoteBreef;
                data.SystemSettingWelcomeNoteDesc = collection.SystemSettingWelcomeNoteDesc;
                data.SystemSettingWelcomeNoteTitle = collection.SystemSettingWelcomeNoteTitle;
                data.SystemSettingLogoImageUrl = (ImageName1 == "") ? collection.SystemSettingLogoImageUrl : ImageName1;
                data.SystemSettingLogoImageUrl2 = (ImageName2 == "") ? collection.SystemSettingLogoImageUrl2 : ImageName2;
                data.SystemSettingWelcomeNoteUrl = collection.SystemSettingWelcomeNoteUrl;
                data.SystemSettingWelcomeNoteImageUrl = (ImageName3 == "") ? collection.SystemSettingWelcomeNoteImageUrl : ImageName3;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                systemSetting.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SystemSettingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SystemSettingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
