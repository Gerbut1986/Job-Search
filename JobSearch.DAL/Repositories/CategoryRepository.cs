namespace JobSearch.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class CategoryRepository : IRepository<Category>
    {
        readonly DataContext db;

        public CategoryRepository(DataContext db) => this.db = db;

        public void Create(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
        }

        public async Task CreateAsync(Category entity)
        {
            db.Categories.Add(entity);
            await db.SaveChangesAsync();
        }

        public IQueryable<Category> GetAll()
        {
            return db.Categories;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var lst = (from ci in db.Categories select ci);
            return await lst.ToListAsync();
        }

        public void Update(Category entity)
        {
            var res = db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Categories.FindAsync(id)).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.Categories.FindAsync(id);
            db.Categories.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await db.Categories.FindAsync(id);
            db.Categories.Remove(found);
        }
    }
}
