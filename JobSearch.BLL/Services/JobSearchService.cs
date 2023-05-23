namespace JobSearch.BLL.Services
{
    using Dto;
    using Helpers;
    using DAL.UOF;
    using System.Linq;
    using DAL.Interfaces;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class JobSearchService : Interfaces.IJobSearchlService
    {
        private readonly IUnitOfWork Db;

        public JobSearchService(string connStr)
        {
            Db = new UnitOfWork(conn: connStr);
        }

        #region Delete:
        public void Delete(VacancyDto model)
        {
            Db.Vacancies.Delete(FillObject.Vacancy(model).Id);
        }

        public void Delete(CategoryDto model)
        {
            Db.Vacancies.Delete(FillObject.Category(model).Id);
        }

        public void Delete(MessageDto model)
        {
            Db.Vacancies.Delete(FillObject.Message(model).Id);
        }
        #endregion
        #region Delete async:
        public async Task DeleteAsync(int id)
        {
            await Db.Vacancies.DeleteAsync(id);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await Db.Categories.DeleteAsync(id);
        }

        public async Task DeleteMessageAsync(int id)
        {
            await Db.Messages.DeleteAsync(id);
        }
        #endregion
        #region Create:
        public void Insert(VacancyDto model)
        {
            Db.Vacancies.Create(FillObject.Vacancy(model));
        }

        public void Insert(CategoryDto model)
        {
            Db.Categories.Create(FillObject.Category(model));
        }

        public void Insert(MessageDto model)
        {
            Db.Messages.Create(FillObject.Message(model));
        }
        #endregion
        #region Create async:
        public async Task InsertAsync(VacancyDto model)
        {
            await Db.Vacancies.CreateAsync(FillObject.Vacancy(model));
        }

        public async Task InsertAsync(CategoryDto model)
        {
            await Db.Categories.CreateAsync(FillObject.Category(model));
        }

        public async Task InsertAsync(MessageDto model)
        {
            await Db.Messages.CreateAsync(FillObject.Message(model));
        }
        #endregion
        #region Read & Read async:
        public IEnumerable<VacancyDto> ReadAll()
        {
            return FillObject.VacanciesList(Db.Vacancies.GetAll().ToList());
        }

        public async Task<IEnumerable<VacancyDto>> ReadAllAsync()
        {
            return FillObject.VacanciesList(await Db.Vacancies.GetAllAsync());
        }

        public IEnumerable<CategoryDto> ReadCategories()
        {
            return FillObject.CategoriesList(Db.Categories.GetAll().ToList());
        }

        public async Task<IEnumerable<CategoryDto>> ReadCategoriesAsync()
        {
            return FillObject.CategoriesList(await Db.Categories.GetAllAsync());
        }

        public IEnumerable<MessageDto> ReadMessages()
        {
            return FillObject.MessagesList(Db.Messages.GetAll().ToList());
        }

        public async Task<IEnumerable<MessageDto>> ReadMessagesAsync()
        {
            return FillObject.MessagesList(await Db.Messages.GetAllAsync());
        }
        #endregion
        #region Update:
        public void Update(VacancyDto model)
        {
            Db.Vacancies.Update(FillObject.Vacancy(model));
        }

        public void Update(CategoryDto model)
        {
            Db.Categories.Update(FillObject.Category(model));
        }
 
        public void Update(MessageDto model)
        {
            Db.Messages.Update(FillObject.Message(model));
        }
        #endregion
        #region Update async:
        public async  Task UpdateAsync(int id)
        {
            await Db.Vacancies.UpdateAsync(id);
        }

        public async Task UpdateCategoryAsync(int id)
        {
            await Db.Categories.UpdateAsync(id);
        }

        public async Task UpdateMessageAsync(int id)
        {
            await Db.Messages.UpdateAsync(id);
        }
        #endregion
    }
}
