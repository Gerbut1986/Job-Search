namespace JobSearch.BLL.Interfaces
{
    using JobSearch.BLL.Dto;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IJobSearchlService
    {
        #region Vacancy:
        void Insert(VacancyDto model);
        Task InsertAsync(VacancyDto model);
        void Update(VacancyDto model);
        Task UpdateAsync(int id);
        void Delete(VacancyDto model);
        Task DeleteAsync(int id);
        IEnumerable<VacancyDto> ReadAll();
        Task<IEnumerable<VacancyDto>> ReadAllAsync();
        #endregion
        #region Category:
        void Insert(CategoryDto model);
        Task InsertAsync(CategoryDto model);
        void Update(CategoryDto model);
        Task UpdateCategoryAsync(int id);
        void Delete(CategoryDto model);
        Task DeleteCategoryAsync(int id);
        IEnumerable<CategoryDto> ReadCategories();
        Task<IEnumerable<CategoryDto>> ReadCategoriesAsync();
        #endregion
        #region Message:
        void Insert(MessageDto model);
        Task InsertAsync(MessageDto model);
        void Update(MessageDto model);
        Task UpdateMessageAsync(int id);
        void Delete(MessageDto model);
        Task DeleteMessageAsync(int id);
        IEnumerable<MessageDto> ReadMessages();
        Task<IEnumerable<MessageDto>> ReadMessagesAsync();
        #endregion
    }
}
