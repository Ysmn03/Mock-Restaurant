
using System.Xml.Serialization;

namespace Restuarant.Models.Repositories
{
    public class MasterContactUsInformationRepository : IRepository<MasterContactUsInformation>
    {
        public AppDbContext AppDb { get; }
        public MasterContactUsInformationRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, MasterContactUsInformation entity)
        {
            MasterContactUsInformation data = Find(id);
            data.IsActive=!data.IsActive;
            data.EditDate = entity.EditDate;
            data.EditUser= entity.EditUser;
            Update(id, data);
        }

        public void Add(MasterContactUsInformation entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.MasterContactUsInformation.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterContactUsInformation entity)
        {
            MasterContactUsInformation data = Find(id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public MasterContactUsInformation Find(int id)
        {
            return AppDb.MasterContactUsInformation.SingleOrDefault(x => x.MasterContactUsInformationId == id);
        }

        public void Update(int id, MasterContactUsInformation entity)
        {
            AppDb.MasterContactUsInformation.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterContactUsInformation> View()
        {
            return AppDb.MasterContactUsInformation.Where(x => x.IsDelete == false).ToList();
        }

        public IList<MasterContactUsInformation> ViewFormClient()
        {
            return AppDb.MasterContactUsInformation.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
