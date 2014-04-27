using System.Web;
using System.Web.Mvc;
using ParlorZeta.Azure.Certificates;
using ParlorZeta.Azure.FileSystem;

namespace ParlorZeta.Web.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly IFileSystem _fileSystem;
        private readonly PublishSettingsStore _store;
        private readonly PublishSettingsCache _cache;

        public CertificatesController(PublishSettingsStore store, PublishSettingsCache cache)
        {
            _store = store;
            _cache = cache;
        }

        public ActionResult Index()
        {
            var model = _cache.GetSettings();
            return View(model);
        }

        public ActionResult Upload(HttpPostedFileBase certificateFile)
        {
            _store.SaveSettings(certificateFile.FileName, certificateFile.InputStream);
            _cache.Refresh();
            return RedirectToAction("Index");
        }
    }
}