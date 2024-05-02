
namespace Restuarant.Models.Repositories
{
    public class MasterSocialMediaRepository : IRepository<MasterSocialMedia>
    {
        public AppDbContext AppDb { get; }
        public MasterSocialMediaRepository(AppDbContext appDb)
        {
            AppDb= appDb;
        }
        public void Active(int id, MasterSocialMedia entity)
        {
            MasterSocialMedia data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
        }

        public void Add(MasterSocialMedia entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.MasterSocialMedia.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, MasterSocialMedia entity)
        {
            MasterSocialMedia data= Find(id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate=entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public MasterSocialMedia Find(int id)
        {
            return AppDb.MasterSocialMedia.SingleOrDefault(x => x.MasterSocialMediaId == id);
        }

        public void Update(int id, MasterSocialMedia entity)
        {
            AppDb.MasterSocialMedia.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<MasterSocialMedia> View()
        {
            return AppDb.MasterSocialMedia.Where(x => x.IsDelete == false).ToList();

        }

        public IList<MasterSocialMedia> ViewFormClient()
        {
            return AppDb.MasterSocialMedia.Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
