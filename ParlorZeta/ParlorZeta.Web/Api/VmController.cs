using System.Threading.Tasks;
using System.Web.Http;
using ParlorZeta.Azure.VirtualMachines;

namespace ParlorZeta.Web.Api
{
    public class VmController : ApiController
    {
        private readonly VirtualMachineClient _client;

        public VmController(VirtualMachineClient client)
        {
            _client = client;
        }

        public async Task<IHttpActionResult> Get()
        {            
            return Ok(await _client.GetMachines());
        }
    }
}
