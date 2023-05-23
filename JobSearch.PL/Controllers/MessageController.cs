namespace JobSearch.PL.Controllers
{
    using BLL.Dto;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using JobSearch.BLL.Services;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;

    public class MessageController : Controller
    {
        private readonly JobSearchService service;
        private static Models.ApplicationUser CurrentUser { get; set; }
        private static int _senderId, _recipientId;

        public MessageController()
        {         
            service = new JobSearchService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        #region SignInManager & UserManager:
        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }
        #endregion

        public async Task<ActionResult> Inbox(string userName)
        {
            var im = UserManager.Users.FirstOrDefault(u => u.Email == userName);
            var messages = await service.ReadMessagesAsync();
            var myMessages = messages.Where(m => m.RecipientId == im.UserId);
            ViewBag.Sender = userName;
            return View(myMessages);
        }

        public async Task<ActionResult> FullMessage(int msgId)
        {
            var messages = await service.ReadMessagesAsync();
            var msg = messages.FirstOrDefault(m => m.Id == msgId);
            msg.IsReviwed = true;
            _senderId = msg.SenderId;
            _recipientId = msg.RecipientId;
            await service.UpdateMessageAsync(msgId);
            ViewBag.Recipient = UserManager.Users.FirstOrDefault(u => u.UserId == msg.SenderId);
            ViewBag.Sender = UserManager.Users.FirstOrDefault(u => u.UserId == msg.RecipientId);
            return View(msg);
        }

        [HttpPost]
        public async Task<ActionResult> Send(EmailData mailInfo)
        {
            await service.InsertAsync(new MessageDto
            {
                Title = mailInfo.Title,
                SenderId = _senderId,
                RecipientId = _recipientId,
                TextMessage = mailInfo.Message,
                DateMessage = System.DateTime.Now,
                IsReviwed = false
            });
            return RedirectToAction("Index", "Home", new { isRespond = "sent"});
        }

        public async Task<ActionResult> Respond(string userEmail, string vacancyTitle, int recipient)
        {
            CurrentUser = UserManager.Users.FirstOrDefault(m => m.Email == userEmail);
            await service.InsertAsync(
                new MessageDto
                {
                    Title = $"Новий відгук на вакансію '{vacancyTitle}'",
                    TextMessage = $"Доброго дня мене звати {CurrentUser.FirstName} {CurrentUser.LastName}|" +
                    $"Мої скіли: {CurrentUser.Skills}|Досвід роботи: {CurrentUser.Experience}|" +
                    $"Місто: {CurrentUser.City}|Мої контакти: {CurrentUser.Email}",
                    SenderId = CurrentUser.UserId,
                    RecipientId = recipient,
                    DateMessage = System.DateTime.Now,
                    IsReviwed = false
                });
            return RedirectToAction("Index", "Home", new { isRespond = "respond"});
        }

        public class EmailData
        {
            public string Title { get; set; }
            public string Message { get; set; } = string.Empty;
        }
    }
}