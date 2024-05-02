
namespace Restuarant.Models.Repositories
{
    public class MasterWorkingHoursRepository : IRepository<MasterWorkingHours>
    {
        public AppDbContext AppDb { get; }
        public MasterWorkingHoursRepository(AppDbContext appDb)
        {
            AppDb= appDb;
        }
        public void Active(int id, MasterWorkingHours entity)
        {
            MasterWorkingHours data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterWorkingHours entity)
        {
            entity.IsDelete = false;
            entity.IsActive = true;
            AppDb.MasterWorkingHours.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterWorkingHours entity)
        {
            MasterWorkingHours data=Find(id);
            data.IsDelete = true;
            data.EditUser=entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public MasterWorkingHours Find(int id)
        {
            return AppDb.MasterWorkingHours.SingleOrDefault(x => x.MasterWorkingHoursId == id);
        }

        public void Update(int id, MasterWorkingHours entity)
        {
            AppDb.MasterWorkingHours.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterWorkingHours> View()
        {
            return AppDb.MasterWorkingHours.Where(x => x.IsDelete == false).ToList();

        }

        public IList<MasterWorkingHours> ViewFormClient()
        {
            return AppDb.MasterWorkingHours.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
