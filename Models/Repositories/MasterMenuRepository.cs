
namespace Restuarant.Models.Repositories
{
    public class MasterMenuRepository : IRepository<MasterMenu>
    {
        public AppDbContext AppDb { get; set; }
        public MasterMenuRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, MasterMenu entity)
        {
            MasterMenu data = Find(id);
            data.IsActive=!data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterMenu entity)
        {
            entity.IsActive=true;
            entity.IsDelete= false;
            AppDb.MasterMenus.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterMenu entity)
        {
            MasterMenu data= Find(id);
            data.IsDelete = true;
            data.IsActive = false;
            data.EditUser=entity.EditUser;
            data.EditDate=entity.EditDate;
            Update (id, data);
            AppDb.SaveChanges();
        }

        public MasterMenu Find(int id)
        {
            return AppDb.MasterMenus.SingleOrDefault(x => x.MasterMenuId == id);
        }

        public void Update(int id, MasterMenu entity)
        {
            AppDb.MasterMenus.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterMenu> View()
        {
            return AppDb.MasterMenus.Where(x => x.IsDelete == false).ToList();
        }

        public IList<MasterMenu> ViewFormClient()
        {
            return AppDb.MasterMenus.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
