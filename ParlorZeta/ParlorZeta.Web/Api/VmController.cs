using System.Web.Http;
using ParlorZeta.Azure.Certificates;

namespace ParlorZeta.Web.Api
{
    public class VmController : ApiController
    {
        public VmController()
        {
            
            
        }

        public IHttpActionResult Get()
        {
            

            return Ok();
        }
    }
}
