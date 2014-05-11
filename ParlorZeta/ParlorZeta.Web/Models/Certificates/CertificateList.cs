using System.Collections.Generic;
using ParlorZeta.Azure.Certificates;

namespace ParlorZeta.Web.Models.Certificates
{
    public class CertificateList
    {
        public static CertificateList FromStore(PublishSettingsStore store)
        {
            var list = new CertificateList();
            list.PublishSettingses = store.GetAllSettings();
            var selected = store.GetUserSelectedSettings();
            if (selected != null)
            {
                list.SelectedId = selected.Id;
            }
            return list;
        }

        public string SelectedId { get; set; }
        public IList<PublishSettings> PublishSettingses { get; set; }  
    }
}