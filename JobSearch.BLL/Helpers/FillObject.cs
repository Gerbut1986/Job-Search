namespace JobSearch.BLL.Helpers
{
    using Dto;
    using DAL.Entities;
    using System.Collections.Generic;

    public class FillObject
    {
        #region Vacancy fill it in:
        public static IEnumerable<VacancyDto> VacanciesList(List<Vacancy> list)
        {
            var dto = new List<VacancyDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new VacancyDto());
                dto[i].Id = list[i].Id;
                dto[i].JobTitle = list[i].JobTitle;
                dto[i].JobDescription = list[i].JobDescription;
                dto[i].JobPayment = list[i].JobPayment;
                dto[i].CompanyName = list[i].CompanyName;
                dto[i].Experience = list[i].Experience;
                dto[i].EmployerId = list[i].EmployerId;
                dto[i].Photo = list[i].Photo;
                dto[i].CategoryId = list[i].CategoryId;
                dto[i].DatePublish = list[i].DatePublish;
            }
            return dto;
        }

        public static Vacancy Vacancy(VacancyDto dto)
        {
            if (dto != null)
                return new Vacancy
                {
                    Id = dto.Id,
                    JobTitle = dto.JobTitle,
                    JobDescription = dto.JobDescription,
                    JobPayment = dto.JobPayment,
                    CompanyName = dto.CompanyName,
                    Experience = dto.Experience,
                    EmployerId = dto.EmployerId,
                    CategoryId = dto.CategoryId,
                    Photo = dto.Photo,
                    DatePublish = dto.DatePublish
                };
            else return null;
        }
        #endregion
        #region Message fill it in:
        public static IEnumerable<MessageDto> MessagesList(List<Message> list)
        {
            var dto = new List<MessageDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new MessageDto());
                dto[i].Id = list[i].Id;
                dto[i].SenderId = list[i].SenderId;
                dto[i].RecipientId = list[i].RecipientId;
                dto[i].DateMessage = list[i].DateMessage;
                dto[i].TextMessage = list[i].TextMessage;
                dto[i].Title = list[i].Title;
                dto[i].IsReviwed = list[i].IsReviwed;
            }
            return dto;
        }

        public static Message Message(MessageDto dto)
        {
            if (dto != null)
                return new Message
                {
                    Id = dto.Id,
                    SenderId = dto.SenderId,
                    RecipientId = dto.RecipientId,
                    DateMessage = dto.DateMessage,
                    TextMessage = dto.TextMessage,
                    Title = dto.Title,
                    IsReviwed = dto.IsReviwed
                };
            else return null;
        }
        #endregion
        #region Category fill it in:
        public static IEnumerable<CategoryDto> CategoriesList(List<Category> list)
        {
            var dto = new List<CategoryDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new CategoryDto());
                dto[i].Id = list[i].Id;
                dto[i].Name = list[i].Name;
            }
            return dto;
        }

        public static Category Category(CategoryDto dto)
        {
            if (dto != null)
                return new Category
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            else return null;
        }
        #endregion     
    }
}
