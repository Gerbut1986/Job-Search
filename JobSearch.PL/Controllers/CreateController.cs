namespace JobSearch.PL.Controllers
{
    using System;
    using System.Web;
    using BLL.Services;
    using System.Web.Mvc;
    using JobSearch.BLL.Dto;
    using JobSearch.PL.Models;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;

    public class CreateController : Controller
    {
        private static string _path;
        private static int _vacancyId, _userId;
        private static string PhotoPath { get; set; }
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

        public CreateController()
        {
            service = new JobSearchService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        [HttpGet]
        public async Task<ActionResult> Vacancy(string emplEmail)
        {
            var categories = await service.ReadCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Employer = CurrentEmployer = await UserManager.FindByEmailAsync(emplEmail);
            _userId = CurrentEmployer.UserId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Vacancy(VacancyDto vacancy)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    vacancy.Photo = PhotoPath;
                    vacancy.DatePublish = DateTime.Now;
                    vacancy.EmployerId = _userId;
                    service.Insert(vacancy);
                    return RedirectToAction("Index", "Home");
                }
                catch 
                {
                    var categories = await service.ReadCategoriesAsync();
                    ViewBag.Categories = new SelectList(categories, "Id", "Name");
                    ViewBag.Employer = CurrentEmployer;
                    return View();
                }
            }
            ViewBag.Categories = await service.ReadCategoriesAsync();
            return View();
        }

        #region Upload product photo[Post]:
        [HttpPost]
        public ActionResult PhotoUpload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
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
                        _path = System.IO.Path.Combine(Server.MapPath($"~/Images/Vacancies/{PhotoPath}"));
                        file.SaveAs(_path);
                    }

                    return Json("Upload!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else return Json("U should to upload image!");
        }
        #endregion
    }
}