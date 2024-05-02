
namespace Restuarant.Models.Repositories
{
    public class TransactionBookTableRepository : IRepository<TransactionBookTable>
    {
        public AppDbContext AppDb { get; }
        public TransactionBookTableRepository(AppDbContext appDb)
        {
            AppDb = appDb;
        }
        public void Active(int id, TransactionBookTable entity)
        {
            TransactionBookTable data = Find(id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
        }

        public void Add(TransactionBookTable entity)
        {
            entity.IsActive = true;
            entity.IsDelete= false;
            AppDb.TransactionBookTable.Add(entity);
            AppDb.SaveChanges();
        }

        public void Delete(int id, TransactionBookTable entity)
        {
            TransactionBookTable data=Find(id);
            data.IsDelete = true;
            data.IsActive = false;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(id, data);
            AppDb.SaveChanges();
        }

        public TransactionBookTable Find(int id)
        {
            return AppDb.TransactionBookTable.SingleOrDefault(x => x.TransactionBookTableId == id);
        }

        public void Update(int id, TransactionBookTable entity)
        {
            AppDb.TransactionBookTable.Update(entity);
            AppDb.SaveChanges();
        }

        public IList<TransactionBookTable> View()
        {
            return AppDb.TransactionBookTable.Where(x => x.IsDelete == false).ToList();

        }

        public IList<TransactionBookTable> ViewFormClient()
        {
            return AppDb.TransactionBookTable.Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
