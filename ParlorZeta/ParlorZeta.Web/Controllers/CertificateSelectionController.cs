using System.Web.Mvc;
using ParlorZeta.Azure.Certificates;

namespace ParlorZeta.Web.Controllers
{
    public class CertificateSelectionController : Controller
    {
        private readonly PublishSettingsStore _store;

        public CertificateSelectionController(PublishSettingsStore store)
        {
            _store = store;
        }

        public PartialViewResult Index()
        {
            var model = _store.GetUserSelectedSettings();
            return PartialView(model);
        }        
    }
}