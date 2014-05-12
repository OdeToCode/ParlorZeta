using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Compute;
using ParlorZeta.Azure.Certificates;

namespace ParlorZeta.Azure.VirtualMachines
{
    public class VirtualMachineClient
    {
        public VirtualMachineClient(PublishSettingsStore settingsStore)
        {
            var settings = settingsStore.GetUserSelectedSettings();
            var credentials = settings.CreateCredentials();
            _client = new ComputeManagementClient(credentials);
        }

        public async Task<IEnumerable<VirtualMachineModel>> GetMachines()
        {
            var response = await _client.HostedServices.ListAsync();
            return response.Select(service => new VirtualMachineModel{ Name=service.ServiceName });
        }

        private readonly ComputeManagementClient _client;
    }
}