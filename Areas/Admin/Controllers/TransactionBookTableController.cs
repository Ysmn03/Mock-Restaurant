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
    public class TransactionBookTableController : Controller
    {
        // GET: TransactionBookTableController
        public IRepository<TransactionBookTable> bookTable {  get; set; }
        public TransactionBookTableController(IRepository<TransactionBookTable> _bookTable)
        {
            bookTable = _bookTable;
        }
        public ActionResult Index(int idDelete)
        {
            if(idDelete!=0)
            {
                TransactionBookTable obj = new()
                {
                    EditDate=DateTime.Now,
                    EditUser= User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                bookTable.Delete(idDelete, obj);
                return RedirectToAction(nameof(Index));
            }
            IList<TransactionBookTable> dataList = bookTable.View();
            IList<TransactionBookTableModel>dataModelList=new List<TransactionBookTableModel>();
            foreach(var data in dataList)
            {
                TransactionBookTableModel dataModel = new TransactionBookTableModel
                {
                    TransactionBookTableId=data.TransactionBookTableId,
                    TransactionBookTableDate=data.TransactionBookTableDate,
                    TransactionBookTableEmail=data.TransactionBookTableEmail,
                    TransactionBookTableFullName=data.TransactionBookTableFullName,
                    TransactionBookTableMobileNumber=data.TransactionBookTableMobileNumber,
                    IsActive = data.IsActive
                };
                dataModelList.Add(dataModel);
            }
            return View(dataModelList);
        }

        // GET: TransactionBookTableController/Details/5
        public ActionResult Active(int id)
        {
            bookTable.Active(id, new TransactionBookTable()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
        // GET: TransactionBookTableController/Create
        public ActionResult Create(int id)
        {
            TransactionBookTableModel dataModel = new TransactionBookTableModel();
            return View(dataModel);
        }

        // POST: TransactionBookTableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionBookTableModel collection)
        {
            try
            {
                if (bookTable.View().Where(x => x.TransactionBookTableFullName.ToUpper()
                == collection.TransactionBookTableFullName.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return View(collection);
                }
                if(!ModelState.IsValid)
                {
                    ModelState.AddModelError("", errorMessage: "Required Field");
                    return View();
                }
                TransactionBookTable data = new TransactionBookTable()
                {
                    TransactionBookTableFullName = collection.TransactionBookTableFullName,
                    TransactionBookTableEmail = collection.TransactionBookTableEmail,
                    TransactionBookTableMobileNumber = collection.TransactionBookTableMobileNumber,
                    TransactionBookTableDate = collection.TransactionBookTableDate,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                bookTable.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionBookTableController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = bookTable.Find(id);
            var obj = new TransactionBookTableModel
            {
                TransactionBookTableId=data.TransactionBookTableId,
                TransactionBookTableFullName=data.TransactionBookTableFullName,
                TransactionBookTableEmail=data.TransactionBookTableEmail,
                TransactionBookTableMobileNumber=data.TransactionBookTableMobileNumber,
                TransactionBookTableDate=data.TransactionBookTableDate,
                IsActive = data.IsActive,
            };
            return View(obj);
        }

        // POST: TransactionBookTableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionBookTableModel collection)
        {
            try
            {
                var data=bookTable.Find(id);
                data.TransactionBookTableFullName = collection.TransactionBookTableFullName;
                data.TransactionBookTableEmail = collection.TransactionBookTableEmail;
                data.TransactionBookTableMobileNumber = collection.TransactionBookTableMobileNumber;
                data.TransactionBookTableDate = collection.TransactionBookTableDate;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                bookTable.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
