namespace JobSearch.DAL.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<IEntity> where IEntity : class
    {
        Task Delete(int id);
        Task DeleteAsync(int id);
        void Create(IEntity entity);
        Task CreateAsync(IEntity entity);
        void Update(IEntity entity); 
        Task UpdateAsync(int id);
        IQueryable<IEntity> GetAll();
        Task<System.Collections.Generic.List<IEntity>> GetAllAsync();
    }
}
