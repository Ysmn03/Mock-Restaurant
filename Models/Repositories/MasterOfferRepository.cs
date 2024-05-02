
namespace Restuarant.Models.Repositories
{
    public class MasterOfferRepository : IRepository<MasterOffer>
    {
        public AppDbContext AppDb { get; set; }
        public MasterOfferRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, MasterOffer entity)
        {
            MasterOffer data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterOffer entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.MasterOffer.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterOffer entity)
        {
            MasterOffer data=Find(id);
            data.IsDelete = true;
            data.IsActive=false;
            data.EditUser = entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public MasterOffer Find(int id)
        {
            return AppDb.MasterOffer.SingleOrDefault(x => x.MasterOfferId == id);
        }

        public void Update(int id, MasterOffer entity)
        {
            AppDb.MasterOffer.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterOffer> View()
        {
            return AppDb.MasterOffer.Where(x => x.IsDelete == false).ToList();
        }

        public IList<MasterOffer> ViewFormClient()
        {
            return AppDb.MasterOffer.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
