
namespace Restuarant.Models.Repositories
{
    public class TransactionContactUsRepository : IRepository<TransactionContactUs>
    {
        public AppDbContext AppDb { get; }
        public TransactionContactUsRepository(AppDbContext appDb)
        {
            AppDb= appDb;
        }
        public void Active(int id, TransactionContactUs entity)
        {
            TransactionContactUs data = Find(id);
            Update(id, data);
        }

        public void Add(TransactionContactUs entity)
        {
            AppDb.TransactionContactUs.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, TransactionContactUs entity)
        {
            TransactionContactUs data=Find(id);
            Update(id, data);
            AppDb.SaveChanges() ;
        }

        public TransactionContactUs Find(int id)
        {
            return AppDb.TransactionContactUs.SingleOrDefault(x => x.TransactionContactUsId == id);
        }

        public void Update(int id, TransactionContactUs entity)
        {
            AppDb.TransactionContactUs.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<TransactionContactUs> View()
        {
            return AppDb.TransactionContactUs.ToList();

        }

        public IList<TransactionContactUs> ViewFormClient()
        {
            return AppDb.TransactionContactUs.ToList();

        }
    }
}
