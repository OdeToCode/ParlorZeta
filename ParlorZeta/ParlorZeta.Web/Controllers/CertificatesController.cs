using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParlorZeta.Azure.Certificates;
using ParlorZeta.Azure.FileSystem;

namespace ParlorZeta.Web.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly PublishSettingsStore _store;

        public CertificatesController(PublishSettingsStore store, PublishSettingsCache cache)
        {
            _store = store;
        }

        public ActionResult Index()
        {
            var model = _store.GetAllSettings();
            return View(model);
        }

        public ActionResult Upload(HttpPostedFileBase certificateFile)
        {
            _store.SaveSettings(certificateFile.FileName, certificateFile.InputStream);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var setting = _store.GetSettingById(id);
            if (setting != null)
            {
                var impactedSettings = _cache.GetSettings().Where(s => s.Filename == setting.Filename);
                return View(impactedSettings);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string fileName, FormCollection collection)
        {
            _store.DeleteSubscriptions(fileName);
            _cache.Refresh();
            return RedirectToAction("Index");
        }
    }
}