
namespace Restuarant.Models.Repositories
{
    public class MasterServicesRepository : IRepository<MasterServices>
    {
        public AppDbContext AppDb { get; }
        public MasterServicesRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, MasterServices entity)
        {
            MasterServices data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterServices entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.MasterServices.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterServices entity)
        {
            MasterServices data=Find(id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public MasterServices Find(int id)
        {
            return AppDb.MasterServices.SingleOrDefault(x => x.MasterServicesId == id);
        }

        public void Update(int id, MasterServices entity)
        {
            AppDb.MasterServices.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterServices> View()
        {
            return AppDb.MasterServices.Where(x => x.IsDelete == false).ToList();

        }

        public IList<MasterServices> ViewFormClient()
        {
            return AppDb.MasterServices.Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
