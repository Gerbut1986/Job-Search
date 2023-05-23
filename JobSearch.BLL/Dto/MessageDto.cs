using System.ComponentModel.DataAnnotations;

namespace JobSearch.BLL.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public System.DateTime DateMessage { get; set; }
        public string TextMessage { get; set; }
        public string Title { get; set; }
        public bool IsReviwed { get; set; }
    }
}
