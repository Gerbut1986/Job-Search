namespace JobSearch.DAL.UOF
{
    using Repositories;
    using Interfaces;
    using Entities;
    using EF;

    public class UnitOfWork : IUnitOfWork
    {
        readonly DataContext db; 

        #region All Repos:    
        MessageRepository messageRepo;
        VacancyRepository vacancyRepo;
        CategoryRepository categoryRepo;
        #endregion

        public UnitOfWork(string conn) => db = new DataContext(conn);

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepo == null)
                    categoryRepo = new CategoryRepository(db);
                return categoryRepo;
            }
        }

        public IRepository<Vacancy> Vacancies
        {
            get
            {
                if (vacancyRepo == null)
                    vacancyRepo = new VacancyRepository(db);
                return vacancyRepo;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (messageRepo == null)
                    messageRepo = new MessageRepository(db);
                return messageRepo;
            }
        }

        #region Dispose:
        bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        #endregion

        public async System.Threading.Tasks.Task<int> SaveAsync() => await db.SaveChangesAsync();
    }
}
