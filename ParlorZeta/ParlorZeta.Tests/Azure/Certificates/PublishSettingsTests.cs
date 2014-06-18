using System.Reflection;
using System.Xml.Linq;
using ParlorZeta.Azure.Certificates;
using Xunit;

namespace ParlorZeta.Tests.Azure.Certificates
{
    public class PublishSettingsTests
    {
        [Fact]
        public void Can_Read_Publish_Settings_File()
        {
            var doc = OpenSampleFile();
            var settings = new PublishSettings(doc, "sample.publishsettings");

            Assert.Equal(settings.Name, "Sample");
            Assert.Equal(settings.Id, "e5a20609-d80f-4d64-b0e6-dc75fa9a4250");
            Assert.Equal(settings.Filename, "sample.publishsettings");
            Assert.NotNull(settings.Certificate);
        }

        XDocument OpenSampleFile()
        {
            var stream =
                Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("ParlorZeta.Tests.Azure.Certificates.sample.publishsettings");

            return XDocument.Load(stream);
        }
    }
}
