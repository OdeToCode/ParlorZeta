using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ParlorZeta.Azure.Certificates;

namespace ParlorZeta.Web.Models.CertificateSelection
{
    public class CertificateSelections
    {
        public CertificateSelections(IEnumerable<PublishSettings> publishSettingses, string selectedId)
        {
            Options = publishSettingses.Select(s => new SelectListItem
            {
                 Text = s.Name,
                 Value = s.Id,
                 Selected = s.Id == selectedId
            });
            SelectedCertificateId = selectedId;
        }

        public string SelectedCertificateId { get; set; }
        public IEnumerable<SelectListItem> Options { get; set; }
    }
}