using System.Web;
using System.Web.Mvc;

namespace ParlorZeta.Web.Controllers
{
    public class CertificatesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(HttpPostedFile certificateFile)
        {
            //certificateFile.
            return View();
        }
    }
}