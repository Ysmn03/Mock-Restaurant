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
    public class TransactionContactUsController : Controller
    {
        // GET: TransactionContactUsController
        public IRepository<TransactionContactUs> contactUs { get; set; }
        public TransactionContactUsController(IRepository<TransactionContactUs> _contactUs)
        {
            contactUs = _contactUs;
        }
        public ActionResult Index(int idDelete)
        {
            if (idDelete != 0)
            {
                TransactionContactUs obj = new()
                {
                    
                };
                contactUs.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<TransactionContactUs> dataList = contactUs.View();
            IList<TransactionContactUsModel> dataModelList = new List<TransactionContactUsModel>();
            foreach (var data in dataList)
            {
                TransactionContactUsModel dataModel = new TransactionContactUsModel
                {
                    TransactionContactUsEmail=data.TransactionContactUsEmail,
                    TransactionContactUsFullName=data.TransactionContactUsFullName,
                    TransactionContactUsId=data.TransactionContactUsId,
                    TransactionContactUsMessage=data.TransactionContactUsMessage,
                    TransactionContactUsSubject =data.TransactionContactUsSubject,
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }

        // GET: TransactionContactUsController/Create
        public ActionResult Create(int id)
        {
            TransactionContactUsModel dataModel =new TransactionContactUsModel();
            return View(dataModel);
        }

        // POST: TransactionContactUsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionContactUsModel collection)
        {
            try
            {
                if (contactUs.View().Where(x => x.TransactionContactUsFullName.ToUpper()
                == collection.TransactionContactUsFullName.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return View(collection);
                }
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", errorMessage: "Required Field");
                    return View();
                }
                TransactionContactUs data = new TransactionContactUs()
                {
                    TransactionContactUsEmail = collection.TransactionContactUsEmail,
                    TransactionContactUsFullName = collection.TransactionContactUsFullName,
                    TransactionContactUsMessage = collection.TransactionContactUsMessage,
                    TransactionContactUsSubject = collection.TransactionContactUsSubject,
                };
                contactUs.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionContactUsController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = contactUs.Find(id);
            var obj = new TransactionContactUsModel
            {
                TransactionContactUsEmail = data.TransactionContactUsEmail,
                TransactionContactUsFullName = data.TransactionContactUsFullName,
                TransactionContactUsId = data.TransactionContactUsId,
                TransactionContactUsMessage = data.TransactionContactUsMessage,
                TransactionContactUsSubject = data.TransactionContactUsSubject,
            };
            return View(obj);
        }

        // POST: TransactionContactUsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionContactUsModel collection)
        {
            try
            {
                var data = contactUs.Find(id);

                // Update the properties of the existing entity
                data.TransactionContactUsFullName = collection.TransactionContactUsFullName;
                data.TransactionContactUsEmail = collection.TransactionContactUsEmail;
                data.TransactionContactUsMessage = collection.TransactionContactUsMessage;
                data.TransactionContactUsSubject = collection.TransactionContactUsSubject;
                contactUs.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
