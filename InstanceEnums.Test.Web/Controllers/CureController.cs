using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.Managers;
using Microsoft.AspNetCore.Mvc;

namespace InstanceEnums.Test.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CureController : ControllerBase
    {
        [HttpGet("{diagnosisType}")]
        public string Get(IDiagnosisManager diagnosisManager)
        {
            return diagnosisManager.GetFix();
        }
    }
}