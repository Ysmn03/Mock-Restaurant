
namespace Restuarant.Models.Repositories
{
    public class SystemSettingRepository : IRepository<SystemSetting>
    {
        public AppDbContext AppDb { get; }
        public SystemSettingRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, SystemSetting entity)
        {
            SystemSetting data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
        }

        public void Add(SystemSetting entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.SystemSettings.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, SystemSetting entity)
        {
            SystemSetting data = Find(id);
            data.IsDelete = true;
            data.IsActive = false;
            data.EditUser = entity.EditUser;
            data.EditDate= entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public SystemSetting Find(int id)
        {
            return AppDb.SystemSettings.SingleOrDefault(z => z.SystemSettingId == id);
        }

        public void Update(int id, SystemSetting entity)
        {
            AppDb.SystemSettings.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<SystemSetting> View()
        {
            return AppDb.SystemSettings.Where(x => x.IsDelete == false).ToList();

        }

        public IList<SystemSetting> ViewFormClient()
        {
            return AppDb.SystemSettings.Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
