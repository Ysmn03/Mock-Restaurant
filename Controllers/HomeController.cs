using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restuarant.Areas.Admin.ViewModels;
using Restuarant.Models;
using Restuarant.Models.Repositories;
using Restuarant.ViewsModel;
using System.Security.Claims;

namespace Restuarant.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public IRepository<MasterMenu> masterMenu { get; }
        public IRepository<MasterSlider> masterSlider { get; }
        public IRepository<MasterSocialMedia> masterSocialMedia { get; }
        public IRepository<MasterContactUsInformation> masterContactUsInformation { get; }
        public IRepository<MasterWorkingHours> masterWorkingHours { get; }
        public IRepository<TransactionNewsletter> transactionNewsletter { get; }
        public IRepository<MasterPartner> masterPartner { get; }
        public IRepository<MasterOffer> masterOffer { get; }
        public IRepository<TransactionBookTable> transactionBookTable { get; }
        public IRepository<MasterItemMenu> masterItemMenu { get; }
        public IRepository<SystemSetting> systemSetting { get; }
        public IRepository<MasterServices> masterServices { get; }
        public IRepository<TransactionContactUs> transactionContactUs { get; }
        public IRepository<MasterCategoryMenu> masterCategoryMenu { get; }
        public HomeController(IRepository<MasterMenu> _masterMenu,
            IRepository<MasterSlider> _masterSlider,
            IRepository<MasterSocialMedia> _masterSocialMedia,
            IRepository<MasterContactUsInformation> _masterContactUsInformation,
            IRepository<MasterWorkingHours> _masterWorkingHours,
            IRepository<TransactionNewsletter> _transactionNewsletter,
            IRepository<MasterPartner> _masterPartner,
            IRepository<MasterOffer> _masterOffer,
            IRepository<TransactionBookTable> _transactionBookTable,
            IRepository<MasterItemMenu> _masterItemMenu,
            IRepository<SystemSetting> _systemSetting,
            IRepository<MasterServices> _masterServices,
            IRepository<TransactionContactUs> _transactionContactUs,
            IRepository<MasterCategoryMenu> _masterCategoryMenu)
        {
            masterMenu = _masterMenu;
            masterSlider = _masterSlider;
            masterSocialMedia = _masterSocialMedia;
            masterContactUsInformation = _masterContactUsInformation;
            masterWorkingHours = _masterWorkingHours;
            transactionNewsletter = _transactionNewsletter;
            masterPartner = _masterPartner;
            masterOffer = _masterOffer;
            transactionBookTable = _transactionBookTable;
            masterItemMenu = _masterItemMenu;
            systemSetting = _systemSetting;
            masterServices = _masterServices;
            transactionContactUs = _transactionContactUs;
            masterCategoryMenu = _masterCategoryMenu;
        }
        public ActionResult Index()
        {
            var data = new HomeViewModel();
            data.ListMasterMenu = masterMenu.ViewFormClient().ToList();
            data.ListMasterSlider = masterSlider.ViewFormClient().ToList();
            data.ListMasterSocialMedia = masterSocialMedia.ViewFormClient().ToList();
            data.ListMasterContactUsInformation = masterContactUsInformation.ViewFormClient().ToList();
            data.ListMasterWorkingHours = masterWorkingHours.ViewFormClient().ToList();
            data.ListMasterPartner= masterPartner.ViewFormClient().ToList();
            data.MasterOffer = masterOffer.ViewFormClient().Where(x => x.MasterOfferId == 3).SingleOrDefault(); ;
            data.ListMasterItemMenu = masterItemMenu.ViewFormClient().OrderByDescending(x => x.MasterItemMenuId).TakeLast(5).ToList();
            data.SystemSetting = systemSetting.ViewFormClient().Where(x => x.SystemSettingId == 10).SingleOrDefault();
            return View(data);
        }
        public IActionResult About()
        {
            var data=new HomeViewModel();
            data.ListMasterMenu = masterMenu.ViewFormClient().ToList();
            data.SystemSetting = systemSetting.ViewFormClient().Where(x => x.SystemSettingId == 10).SingleOrDefault();
            data.ListMasterSocialMedia = masterSocialMedia.ViewFormClient().ToList();
            data.ListMasterContactUsInformation = masterContactUsInformation.ViewFormClient().ToList();
            data.ListMasterWorkingHours = masterWorkingHours.ViewFormClient().ToList();
            data.ListMasterServices = masterServices.ViewFormClient().ToList();
            return View(data);
        }
        public IActionResult ContactUs()
        {
            var data = new HomeViewModel();
            data.ListMasterMenu = masterMenu.ViewFormClient().ToList();
            data.SystemSetting = systemSetting.ViewFormClient().Where(x => x.SystemSettingId == 10).SingleOrDefault();
            data.ListMasterSocialMedia = masterSocialMedia.ViewFormClient().ToList();
            data.ListMasterContactUsInformation = masterContactUsInformation.ViewFormClient().ToList();
            data.ListMasterWorkingHours = masterWorkingHours.ViewFormClient().ToList();
            
            return View(data);
        }
        public IActionResult Menu()
        {
            var data = new HomeViewModel();
            data.ListMasterMenu = masterMenu.ViewFormClient().ToList();
            data.SystemSetting = systemSetting.ViewFormClient().Where(x => x.SystemSettingId == 10).SingleOrDefault();
            data.ListMasterSocialMedia = masterSocialMedia.ViewFormClient().ToList();
            data.ListMasterContactUsInformation = masterContactUsInformation.ViewFormClient().ToList();
            data.ListMasterWorkingHours = masterWorkingHours.ViewFormClient().ToList();
            data.ListMasterItemMenu = masterItemMenu.ViewFormClient().ToList();
            data.ListMasterCategoryMenu = masterCategoryMenu.ViewFormClient().ToList();
            data.ListMasterPartner = masterPartner.ViewFormClient().ToList();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTransactionNewsletter(HomeViewModel dataViewModel)
        {
            try
            {
                if (transactionNewsletter.View().Where(data => data.TransactionNewsletterEmail.ToUpper()
                == dataViewModel.TransactionNewsletter.TransactionNewsletterEmail.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return RedirectToAction(nameof(Index));
                }
                TransactionNewsletter data = new TransactionNewsletter()
                {
                    TransactionNewsletterEmail = dataViewModel.TransactionNewsletter.TransactionNewsletterEmail,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "",
                    EditUser = "",
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                transactionNewsletter.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTransactionContactUs(HomeViewModel dataViewModel)
        {
            try
            {
                TransactionContactUs data = new TransactionContactUs()
                {
                    TransactionContactUsFullName = dataViewModel.TransactionContactUs.TransactionContactUsFullName,
                    TransactionContactUsEmail = dataViewModel.TransactionContactUs.TransactionContactUsEmail,
                    TransactionContactUsSubject = dataViewModel.TransactionContactUs.TransactionContactUsSubject,
                    TransactionContactUsMessage = dataViewModel.TransactionContactUs.TransactionContactUsMessage,
                };
                transactionContactUs.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTransactionBookTable(HomeViewModel dataViewModel)
        {
            try
            {
                if (transactionBookTable.View().Where(data => data.TransactionBookTableFullName.ToUpper()
                == dataViewModel.TransactionBookTable.TransactionBookTableFullName.ToUpper()).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "This name is already used.");
                    return View(dataViewModel);
                }
                TransactionBookTable data = new TransactionBookTable()
                {
                    TransactionBookTableFullName=dataViewModel.TransactionBookTable.TransactionBookTableFullName,
                    TransactionBookTableEmail=dataViewModel.TransactionBookTable.TransactionBookTableEmail,
                    TransactionBookTableMobileNumber=dataViewModel.TransactionBookTable.TransactionBookTableMobileNumber,
                    TransactionBookTableDate=dataViewModel.TransactionBookTable.TransactionBookTableDate,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = "",
                    EditUser = "",
                    EditDate = DateTime.UtcNow,
                    IsActive = true,
                    IsDelete = false
                };
                transactionBookTable.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
