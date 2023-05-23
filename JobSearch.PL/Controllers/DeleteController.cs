namespace JobSearch.PL.Controllers
{
    using System.Linq;
    using BLL.Services;
    using System.Web.Mvc;

    public class DeleteController : Controller
    {
        private readonly JobSearchService service;

        public DeleteController()
        {
            service = new JobSearchService(_829528a_441d_484m_862i_22475963ffdn.Credential.ConnStr);
        }

        [HttpGet]
        public ActionResult Vacancy(int? id)
        {
            return View(service.ReadAll().FirstOrDefault(v => v.Id == id));
        }

        [HttpPost]
        public ActionResult Vacancy(int id)
        {
            service.Delete(service.ReadAll().FirstOrDefault(v => v.Id == id));
            return RedirectToAction("Index", "Home");
        }
    }
}