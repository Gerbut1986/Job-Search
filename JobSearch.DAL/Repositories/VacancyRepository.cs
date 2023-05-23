namespace JobSearch.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class VacancyRepository : IRepository<Vacancy>
    {
        readonly DataContext db;

        public VacancyRepository(DataContext db) => this.db = db;

        public void Create(Vacancy entity)
        {
            db.Vacancies.Add(entity);
            db.SaveChanges();
        }

        public async Task CreateAsync(Vacancy entity)
        {
            db.Vacancies.Add(entity);
            await db.SaveChangesAsync();
        }

        public IQueryable<Vacancy> GetAll()
        {
            return db.Vacancies;
        }

        public async Task<List<Vacancy>> GetAllAsync()
        {
            var lst = (from ci in db.Vacancies select ci);
            return await lst.ToListAsync();
        }

        public void Update(Vacancy entity)
        {
            var res = db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();            
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Vacancies.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.Vacancies.FindAsync(id);
            db.Vacancies.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await db.Vacancies.FindAsync(id);
            db.Vacancies.Remove(found);
        }
    }
}
