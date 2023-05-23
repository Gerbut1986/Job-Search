namespace JobSearch.DAL.Repositories
{
    using EF;
    using Entities;
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class MessageRepository : IRepository<Message>
    {
        readonly DataContext db;

        public MessageRepository(DataContext db) => this.db = db;

        public void Create(Message entity)
        {
            db.Messages.Add(entity);
            db.SaveChanges();
        }

        public async Task CreateAsync(Message entity)
        {
            db.Messages.Add(entity);
            await db.SaveChangesAsync();
        }

        public IQueryable<Message> GetAll()
        {
            return db.Messages;
        }

        public async Task<List<Message>> GetAllAsync()
        {
            var lst = (from ci in db.Messages select ci);
            return await lst.ToListAsync();
        }

        public void Update(Message entity)
        {
            var res = db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public async Task UpdateAsync(int id)
        {
            db.Entry(await db.Messages.FindAsync(id)).State = EntityState.Modified;
            int res = await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var en = await db.Messages.FindAsync(id);
            db.Messages.Remove(en);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await db.Messages.FindAsync(id);
            db.Messages.Remove(found);
        }
    }
}
