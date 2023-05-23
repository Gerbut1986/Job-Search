namespace JobSearch.PL.Controllers
{
    using System;
    using System.Web;
    using System.Linq;
    using BLL.Services;
    using System.Web.Mvc;
    using JobSearch.BLL.Dto;
    using JobSearch.PL.Models;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;

    public class EditController : Controller
    {
        private static DateTime DatePublish { get; set; }
        private static string PhotoName { get; set; }
        private readonly JobSearchService service;
        private ApplicationUser CurrentEmployer { get; set; }

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

        public EditController()
        {
            service = new JobSearchService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        [HttpGet]
        public async Task<ActionResult> Vacancy(int? id, string emplEmail)
        {
            var categories = await service.ReadCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Employer = CurrentEmployer = await UserManager.FindByEmailAsync(emplEmail);
            var vacancy = service.ReadAll().FirstOrDefault(v => v.Id == id);
            PhotoName = vacancy.Photo;
            DatePublish = vacancy.DatePublish;
            return View(vacancy);
        }

        [HttpPost]
        public ActionResult Vacancy(VacancyDto vacancy)
        {
            if (vacancy.Photo == null)
                vacancy.Photo = PhotoName;
            if(vacancy.DatePublish == DateTime.MinValue)
                vacancy.DatePublish = DatePublish;
            service.Update(vacancy);
            return RedirectToAction("Index", "Home");
        }
    }
}