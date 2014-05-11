using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Scheduler;
using ParlorZeta.Azure.Certificates;

namespace ParlorZeta.Azure.VirtualMachines
{
    public class VirtualMachineClient
    {
        public VirtualMachineClient(PublishSettingsStore settingsStore)
        {
            var settings = settingsStore.GetUserSelectedSettings();
            var credentials = settings.CreateCredentials();
            _client = new CloudServiceManagementClient(credentials);
        }

        public async Task<IEnumerable<VirtualMachineModel>> GetMachines()
        {
            var response = await _client.CloudServices.ListAsync();
            return response.CloudServices.Select(service => new VirtualMachineModel{ Name=service.Name });
        }

        private readonly CloudServiceManagementClient _client;
    }
}