using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Restuarant.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MasterCategoryMenu> MasterCategoryMenu { get; set; }
        public DbSet <MasterContactUsInformation> MasterContactUsInformation { get; set; }
        public DbSet<MasterItemMenu> MasterItemMenus { get; set; }
        public DbSet <MasterMenu> MasterMenus { get; set; }
        public DbSet <MasterOffer> MasterOffer { get; set; }
        public DbSet<MasterPartner> MasterPartners { get; set; }
        public DbSet<MasterServices> MasterServices { get; set; }
        public DbSet <MasterSlider> MasterSlider { get; set; }
        public DbSet<MasterSocialMedia> MasterSocialMedia { get; set; }
        public DbSet<MasterWorkingHours> MasterWorkingHours { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<TransactionBookTable> TransactionBookTable { get; set; }
        public DbSet<TransactionContactUs> TransactionContactUs { get; set; }
        public DbSet<TransactionNewsletter > TransactionNewsletter { get; set; }
    }
}
