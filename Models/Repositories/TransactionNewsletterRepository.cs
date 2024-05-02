
namespace Restuarant.Models.Repositories
{
    public class TransactionNewsletterRepository : IRepository<TransactionNewsletter>
    {
        public AppDbContext AppDb { get; }
        public TransactionNewsletterRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, TransactionNewsletter entity)
        {
            TransactionNewsletter data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser=entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(TransactionNewsletter entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            AppDb.TransactionNewsletter.Update(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, TransactionNewsletter entity)
        {
            TransactionNewsletter data = Find(id);
            data.IsDelete = true;
            data.IsActive = false;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public TransactionNewsletter Find(int id)
        {
            return AppDb.TransactionNewsletter.SingleOrDefault(z => z.TransactionNewsletterId == id);
        }

        public void Update(int id, TransactionNewsletter entity)
        {
            AppDb.TransactionNewsletter.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<TransactionNewsletter> View()
        {
            return AppDb.TransactionNewsletter.Where(x => x.IsDelete == false).ToList();

        }

        public IList<TransactionNewsletter> ViewFormClient()
        {
            return AppDb.TransactionNewsletter.Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
