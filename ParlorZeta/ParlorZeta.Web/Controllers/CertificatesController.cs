using System.Web;
using System.Web.Mvc;
using ParlorZeta.Azure.Certificates;
using ParlorZeta.Web.Infrastructure;
using ParlorZeta.Web.Models.Certificates;

namespace ParlorZeta.Web.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly PublishSettingsStore _settingsStore;
        private readonly CookieStore _cookieStore;

        public CertificatesController(PublishSettingsStore settingsStore, CookieStore cookieStore)
        {
            _settingsStore = settingsStore;
            _cookieStore = cookieStore;
        }

        public ActionResult Index()
        {
            var settings = _settingsStore.GetAllSettings();
            var model = new CertificateList
            {
                PublishSettingses = settings,
                SelectedId = _cookieStore.GetSelectedSubscriptionId()
            };
            return View(model);
        }

        public ActionResult Upload(HttpPostedFileBase certificateFile)
        {
            _settingsStore.SaveSettings(certificateFile.FileName, certificateFile.InputStream);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Select(string selectedCertificateId)
        {
            _cookieStore.SetSelectedSubscriptionId(selectedCertificateId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var setting = _settingsStore.GetSettingById(id);
            if (setting != null)
            {
                var impactedSettings = _settingsStore.GetSettingByFile(setting.Filename);
                return View(impactedSettings);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string fileName, FormCollection collection)
        {
            _settingsStore.DeleteSubscriptions(fileName);
            return RedirectToAction("Index");
        }
    }
}