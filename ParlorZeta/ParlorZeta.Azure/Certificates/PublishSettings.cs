using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Microsoft.WindowsAzure;

namespace ParlorZeta.Azure.Certificates
{
    public class PublishSettings
    {
        public PublishSettings(XDocument document, string fileName)
        {
            try
            {
                var profile = document.Element("PublishData").Element("PublishProfile");
                var subscription = profile.Element("Subscription");

                Id = (string) subscription.Attribute("Id");
                Name = (string) subscription.Attribute("Name");
                Certificate = new X509Certificate2(Convert.FromBase64String((string) profile.Attribute("ManagementCertificate")));
                Filename = fileName;
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidPublishSettings(ex);
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public X509Certificate2 Certificate { get; set; }
        public string Filename { get; set; }

        public SubscriptionCloudCredentials CreateCredentials()
        {
            return new CertificateCloudCredentials(Id, Certificate);
        }
    }
}
