namespace JobSearch.DAL.Interfaces
{
    using Entities;

    public interface IUnitOfWork : System.IDisposable
    {
        IRepository<Vacancy> Vacancies { get; }
        IRepository<Category> Categories { get; }
        IRepository<Message> Messages { get; }
    }
}
