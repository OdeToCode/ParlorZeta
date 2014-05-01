using System.Web.Mvc;
using ParlorZeta.Azure.Certificates;
using ParlorZeta.Web.Infrastructure;

namespace ParlorZeta.Web.Controllers
{
    public class CertificateSelectionController : Controller
    {
        private readonly PublishSettingsStore _store;
        private readonly CookieStore _cookies;

        public CertificateSelectionController(PublishSettingsStore store, CookieStore cookies)
        {
            _store = store;
            _cookies = cookies;
        }

        public PartialViewResult Index()
        {
            var selected = _cookies.GetSelectedSubscriptionId();
            var model = _store.GetSettingById(selected);
            return PartialView(model);
        }        
    }
}