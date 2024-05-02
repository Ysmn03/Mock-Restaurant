
namespace Restuarant.Models.Repositories
{
    public class MasterPartnerRepository : IRepository<MasterPartner>
    {
        public AppDbContext AppDb { get; }
        public MasterPartnerRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, MasterPartner entity)
        {
            MasterPartner data = Find(id);
            data.IsActive=!data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterPartner entity)
        {
            entity.IsActive=true;
            entity.IsDelete=false;
            AppDb.MasterPartners.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterPartner entity)
        {
            MasterPartner data=Find(id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public MasterPartner Find(int id)
        {
            return AppDb.MasterPartners.SingleOrDefault(x => x.MasterPartnerId == id);
        }

        public void Update(int id, MasterPartner entity)
        {
            AppDb.MasterPartners.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterPartner> View()
        {
            return AppDb.MasterPartners.Where(x => x.IsDelete == false).ToList();

        }

        public IList<MasterPartner> ViewFormClient()
        {
            return AppDb.MasterPartners.Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
