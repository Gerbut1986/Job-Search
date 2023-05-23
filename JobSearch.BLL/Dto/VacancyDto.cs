namespace JobSearch.BLL.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class VacancyDto
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobPayment { get; set; }
        public string CompanyName { get; set; }
        public string Experience { get; set; }
        public int EmployerId { get; set; }
        public int CategoryId { get; set; }
        public string Photo { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime DatePublish { get; set; }
    }
}
