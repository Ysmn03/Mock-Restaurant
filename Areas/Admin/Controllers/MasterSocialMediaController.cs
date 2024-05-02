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
    public class MasterSocialMediaController : Controller
    {
        public IRepository<MasterSocialMedia> socialMedia { get; set; }
        public MasterSocialMediaController(IRepository<MasterSocialMedia> _socialMedia)
        {
            socialMedia = _socialMedia;
        }
        // GET: MasterSocialMediaController
        public ActionResult Index(int idDelete)
        {
            if (idDelete > 0)
            {
                MasterSocialMedia obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                socialMedia.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));

            }
            IList<MasterSocialMedia> dataList = socialMedia.View();
            IList<MasterSocialMediaModel> dataModelList = new List<MasterSocialMediaModel>();
            foreach (var data in dataList)
            {
                MasterSocialMediaModel dataModel = new MasterSocialMediaModel
                {
                    MasterSocialMediaId = data.MasterSocialMediaId,
                    MasterSocialMediaUrl = data.MasterSocialMediaUrl,
                    MasterSocialMediaImageUrl = data.MasterSocialMediaImageUrl,
                    IsActive = data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }
        public ActionResult Active(int id)
        {
            socialMedia.Active(id, new MasterSocialMedia()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterSocialMediaController/Create
        public ActionResult Create()
        {
            MasterSocialMediaModel dataModel = new MasterSocialMediaModel();
            return View(dataModel);
        }

        // POST: MasterSocialMediaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MasterSocialMediaModel collection)
        {
            try
            {
                MasterSocialMedia data = new MasterSocialMedia()
                {
                    MasterSocialMediaId = collection.MasterSocialMediaId,
                    MasterSocialMediaUrl = collection.MasterSocialMediaUrl,
                    MasterSocialMediaImageUrl = collection.MasterSocialMediaImageUrl,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                socialMedia.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterSocialMediaController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = socialMedia.Find(id);
            var obj = new MasterSocialMediaModel
            {
                MasterSocialMediaId = data.MasterSocialMediaId,
                MasterSocialMediaUrl = data.MasterSocialMediaUrl,
                MasterSocialMediaImageUrl = data.MasterSocialMediaImageUrl,
                IsActive = data.IsActive
            };
            return View(obj);
        }

        // POST: MasterSocialMediaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MasterSocialMediaModel collection)
        {
            try
            {
                var data = socialMedia.Find(id);
                data.MasterSocialMediaUrl = collection.MasterSocialMediaUrl;
                data.MasterSocialMediaImageUrl = collection.MasterSocialMediaImageUrl;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                socialMedia.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
