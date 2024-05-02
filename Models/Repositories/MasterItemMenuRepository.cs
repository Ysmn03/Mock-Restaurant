
namespace Restuarant.Models.Repositories
{
    public class MasterItemMenuRepository : IRepository<MasterItemMenu>
    {
        public AppDbContext AppDb { get; }
        public MasterItemMenuRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, MasterItemMenu entity)
        {
            MasterItemMenu data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate= entity.EditDate;
            Update(id,data);
        }

        public void Add(MasterItemMenu entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.MasterItemMenus.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterItemMenu entity)
        {
            MasterItemMenu data=Find(id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate= entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public MasterItemMenu Find(int id)
        {
            return AppDb.MasterItemMenus.SingleOrDefault(x => x.MasterItemMenuId == id);
        }

        public void Update(int id, MasterItemMenu entity)
        {
            AppDb.MasterItemMenus.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterItemMenu> View()
        {
            return AppDb.MasterItemMenus.Where(x => x.IsDelete == false).ToList();

        }

        public IList<MasterItemMenu> ViewFormClient()
        {
            return AppDb.MasterItemMenus.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
