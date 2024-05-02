using Restuarant.Models;

namespace Restuarant.ViewsModel
{
    public class HomeViewModel
    {
        public List<MasterMenu> ListMasterMenu { get; set; }
        public List<MasterSlider> ListMasterSlider { get; set; }
        public List<MasterSocialMedia> ListMasterSocialMedia { get; set;}
        public List<MasterContactUsInformation> ListMasterContactUsInformation { get; set; }
        public List<MasterWorkingHours> ListMasterWorkingHours { get; set;}
        public TransactionNewsletter TransactionNewsletter { get; set; }
        public List<MasterPartner> ListMasterPartner { get; set; }
        public MasterOffer MasterOffer { get; set; }
        public TransactionBookTable TransactionBookTable { get; set; }
        public List<MasterItemMenu> ListMasterItemMenu { get; set;}
        public SystemSetting SystemSetting { get; set; }
        public List<MasterServices> ListMasterServices { get; set; }
        public TransactionContactUs TransactionContactUs { get; set; }
        public List<MasterCategoryMenu> ListMasterCategoryMenu { get; set;}
    }
}
