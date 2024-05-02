
namespace Restuarant.Models.Repositories
{
    public class MasterCategoryMenuRepository : IRepository<MasterCategoryMenu>
    {
        public AppDbContext AppDb { get; }
        public MasterCategoryMenuRepository(AppDbContext appDbContext)
        {
            AppDb= appDbContext;
        }
        public void Active(int id, MasterCategoryMenu entity)
        {
            MasterCategoryMenu data = Find(id);
            data.IsActive=!data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterCategoryMenu entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.MasterCategoryMenu.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterCategoryMenu entity)
        {
            MasterCategoryMenu data = Find(id);
            data.IsDelete = true;
            data.IsActive = false;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public MasterCategoryMenu Find(int id)
        {
            return AppDb.MasterCategoryMenu.SingleOrDefault(x => x.MasterCategoryMenuId == id);
        }

        public void Update(int id, MasterCategoryMenu entity)
        {
            AppDb.MasterCategoryMenu.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterCategoryMenu> View()
        {
            return AppDb.MasterCategoryMenu.Where(x => x.IsDelete == false).ToList();
        }

        public IList<MasterCategoryMenu> ViewFormClient()
        {
            return AppDb.MasterCategoryMenu.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
        
    }
}
