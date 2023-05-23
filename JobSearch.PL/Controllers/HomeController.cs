namespace JobSearch.PL.Controllers
{
    using BLL.Dto;
    using System.Web;
    using System.Linq;
    using BLL.Services;
    using System.Web.Mvc;
    using JobSearch.PL.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.Owin;
    using System;

    public class HomeController : Controller
    {
        #region Fields & Properties:
        private int _userId = 0;
        private string _role = null;
        private static bool _isRespond = false;
        private string WelcomeStr { get; set; }
        private string CurrentUserEmail { get; set; }
        private bool _isLogin = false, _updUserInfo = false;
        private ApplicationUser CurrentLocalUser { get; set; }
        private readonly JobSearchService service;

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
        #endregion

        public HomeController()
        {
            service = new JobSearchService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        [HttpGet]
        public async Task<ActionResult> Index(Searcher search, int? categoryId, string isRespond = null)
        {
            if (isRespond != null)
            {
                if (isRespond.Equals("respond"))
                    ViewBag.ModalMsg = "Відгук на вакансію відправлено успішно!";
                else ViewBag.ModalMsg = "Відповідь успішна відправлена адресату!";
                _isRespond = true;
            }
            var categories = await service.ReadCategoriesAsync();
            var vacancies = await service.ReadAllAsync();
            if (categoryId != null)
            {
                vacancies = vacancies.Where(c => c.CategoryId == categoryId);
                if(!vacancies.Any())
                {
                    _isRespond = true;
                    ViewBag.ModalMsg = $"Категорія '{categories.FirstOrDefault(n=>n.Id == categoryId).Name}' " +
                        $"ще не має вакансій...";
                }
            }
            if (search.EmailBy != null && search.SearchBy != null) 
            {
                vacancies = SearchVacancy(search, vacancies as List<VacancyDto>);
                if (vacancies.Count() == 0)
                {
                    _isRespond = true;
                    ViewBag.ModalMsg = "За данним запитом вакансія(ї) відсутні!";
                }
            }
            ViewBag.Categories = categories;
            ViewBag.IsLogin = _isLogin;
            ViewBag.Role = _role; 
            ViewBag.UserId = _userId;
            ViewBag.WelcomeStr = WelcomeStr;
            ViewBag.MyEmail = CurrentUserEmail;
            ViewBag.IsRespond = _isRespond;
            return View(vacancies);
        }

        [HttpGet]
        public ActionResult UserProfile(string isUpdate = null)
        {
            if (isUpdate != null) _isRespond = true;
            ViewBag.IsRespond = _isRespond;
            return View(CurrentLocalUser);
        }

        [HttpPost]
        public async Task<ActionResult> UserProfile(ApplicationUser user)
        {
            var u = await UserManager.FindByIdAsync(CurrentLocalUser.Id);
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.DateBirthday = user.DateBirthday;
            u.City = user.City;
            u.Email = user.Email;
            u.PhoneNumber = user.PhoneNumber;
            u.Skills = user.Skills;
            u.Experience = user.Experience;
            var updRes = await UserManager.UpdateAsync(u);
            if (updRes.Succeeded)
                _updUserInfo = true;
            ViewBag.UpdUserInfo = _updUserInfo;
            ViewBag.IsRespond = _isRespond;
            return RedirectToAction("UserProfile", "Home", new { isUpdate = "Yes" });
        }

        [HttpGet]
        public ActionResult FullVacancy(int id)
        {
            var vacancy = service.ReadAll().FirstOrDefault(i => i.Id == id);
            return View(vacancy);
        }

        public ActionResult CloseAlerts()
        {
            _isRespond = false;
            return RedirectToAction("../Home/Index");
        }

        public IEnumerable<VacancyDto> SearchVacancy(Searcher search, List<VacancyDto> lo)
        {
            switch (search.SearchBy)
            {
                case "1": 
                    var found = 
                        lo.FirstOrDefault(s => s.JobTitle.ToLower().Contains(search.EmailBy.ToLower()));
                    if (found != null)
                    {
                        var found_lst = new List<VacancyDto>();
                        found_lst.Add(found);
                        return found_lst;
                    }
                    else return new List<VacancyDto>();
                case "2":
                    var foundLst = lo.Where(s => s.CompanyName.ToLower().Contains(search.EmailBy.ToLower()));
                    if (foundLst != null)
                        return foundLst;
                    else return new List<VacancyDto>();
                case "0": return new List<VacancyDto>();
                default: return new List<VacancyDto>();
            }
        }

        #region Upload product photo[Post]:
        [HttpPost]
        public async Task<ActionResult> PhotoUpload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string PhotoPath;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            PhotoPath = testfiles[testfiles.Length - 1];
                        }
                        else PhotoPath = file.FileName;
                        string _path = System.IO.Path.Combine(Server.MapPath($"~/Images/Users/{PhotoPath}"));
                        file.SaveAs(_path);
                        var u = await UserManager.FindByIdAsync(CurrentLocalUser.Id);
                        u.Photo = PhotoPath;
                        var updRes = await UserManager.UpdateAsync(u);
                    }
                    return Json("Фото успішно завантажене! Оновіть сторінку.");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else return Json("Спочатку завантажте своє фото!");
        }
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUserEmail = filterContext.HttpContext.User.Identity.Name;
                try
                {
                    _isLogin = true;
                    CurrentLocalUser = UserManager.Users.FirstOrDefault(u => u.Email == CurrentUserEmail);
                    WelcomeStr = $"Привіт {CurrentLocalUser.FirstName} {CurrentLocalUser.LastName} !";
                    _role = CurrentLocalUser.Role;
                    _userId = CurrentLocalUser.UserId;
                }
                catch { }
            }
            else // When User Log off, or still doesn't enter 
            {
                _role = Role.Guest.ToString();
                CurrentUserEmail = null;
                CurrentLocalUser = new ApplicationUser { UserId = 0 };
            }
        }
    }
}