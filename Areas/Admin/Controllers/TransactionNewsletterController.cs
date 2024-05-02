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
    public class TransactionNewsletterController : Controller
    {
        public IRepository<TransactionNewsletter>newsletter {  get; set; }
        public TransactionNewsletterController(IRepository<TransactionNewsletter> _newsletter)
        {
            newsletter = _newsletter;
        }
        // GET: TransactionNewsletterController1
        public ActionResult Index(int idDelete)
        {
            if (idDelete != 0)
            {
                TransactionNewsletter obj = new()
                {
                    EditDate = DateTime.Now,
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                newsletter.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<TransactionNewsletter> dataList = newsletter.View();
            IList<TransactionNewsletterModel> dataModelList = new List<TransactionNewsletterModel>();
            foreach(var data in dataList)
            {
                TransactionNewsletterModel dataModel = new TransactionNewsletterModel
                {
                    TransactionNewsletterId=data.TransactionNewsletterId,
                    TransactionNewsletterEmail=data.TransactionNewsletterEmail,
                    IsActive=data.IsActive,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }

        public ActionResult Active(int id)
        {
            newsletter.Active(id, new TransactionNewsletter()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: TransactionNewsletterController1/Create
        public ActionResult Create(int id)
        {
            TransactionNewsletterModel dataModel = new TransactionNewsletterModel();
            return View(dataModel);
        }

        // POST: TransactionNewsletterController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionNewsletterModel collection)
        {
            try
            {
                if (newsletter.View().Where(x => x.TransactionNewsletterEmail.ToUpper()
                == collection.TransactionNewsletterEmail.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return View(collection);
                }
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", errorMessage: "Required Field");
                    return View();
                }
                TransactionNewsletter data = new TransactionNewsletter()
                {
                    TransactionNewsletterEmail=collection.TransactionNewsletterEmail,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                newsletter.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionNewsletterController1/Edit/5
        public ActionResult Edit(int id)
        {
            var data = newsletter.Find(id);
            var obj = new TransactionNewsletterModel
            {
                TransactionNewsletterEmail=data.TransactionNewsletterEmail,
                TransactionNewsletterId=data.TransactionNewsletterId,
                IsActive = data.IsActive,
            };
            return View(obj);
        }

        // POST: TransactionNewsletterController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionNewsletterModel collection)
        {
            try
            {
                var data = newsletter.Find(id);
                data.TransactionNewsletterEmail=collection.TransactionNewsletterEmail;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                newsletter.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
