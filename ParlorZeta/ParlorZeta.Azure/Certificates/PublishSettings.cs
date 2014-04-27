using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace ParlorZeta.Azure.Certificates
{
    public class PublishSettings
    {
        public PublishSettings(XElement element)
        {
            Id = (string)element.Attribute("Id");
            Name = (string) element.Attribute("Name");
            Certificate = new X509Certificate2(Convert.FromBase64String((string)element.Attribute("ManagementCertificate")));
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public X509Certificate2 Certificate { get; set; }       
    }
}
