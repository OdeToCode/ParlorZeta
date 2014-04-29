using System.Collections.Generic;
using ParlorZeta.Azure.Certificates;

namespace ParlorZeta.Web.Models.Certificates
{
    public class CertificateList
    {
        public string SelectedId { get; set; }
        public IList<PublishSettings> PublishSettingses { get; set; }  
    }
}