namespace JobSearch.DAL.EF
{
    using Entities;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext(string connection) : base(connection)
        {
        }

        public virtual DbSet<Vacancy> Vacancies { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }
}
