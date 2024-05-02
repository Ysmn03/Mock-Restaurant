
namespace Restuarant.Models.Repositories
{
    public class MasterSliderRepository : IRepository<MasterSlider>
    {
        public AppDbContext AppDb { get; }
        public MasterSliderRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, MasterSlider entity)
        {
            MasterSlider data = Find(id);
            data.IsActive=!data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, entity);
        }

        public void Add(MasterSlider entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.MasterSlider.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterSlider entity)
        {
            MasterSlider data=Find(id);
            data.IsDelete = true;
            data.IsActive = false;
            data.EditUser=entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, entity);
            AppDb.SaveChanges();
        }

        public MasterSlider Find(int id)
        {
            return AppDb.MasterSlider.SingleOrDefault(x => x.MasterSliderId == id);
        }

        public void Update(int id, MasterSlider entity)
        {
            AppDb.MasterSlider.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterSlider> View()
        {
            return AppDb.MasterSlider.Where(x => x.IsDelete == false).ToList();

        }

        public IList<MasterSlider> ViewFormClient()
        {
            return AppDb.MasterSlider.Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
