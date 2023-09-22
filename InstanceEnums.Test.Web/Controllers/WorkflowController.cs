using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.Model;
using Microsoft.AspNetCore.Mvc;
using InstanceEnums.Test.Web.Enums;

namespace InstanceEnums.Test.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        [HttpGet("{diagnosisType}")]
        public string Get(DiagnosisTypes.IDiagnosisType diagnosisType)
        {
            return GetDiagnosis((dynamic)diagnosisType);
        }

        private string GetDiagnosis(DiagnosisTypes.IHighBloodPresure value) {
            return "You are about to burst";
        }

        private string GetDiagnosis(DiagnosisTypes.ISleepingTooLittle value)
        {
            return "Why so tired?";
        }

        // [Route("one")]
        //[HttpGet(Name = "GetWeatherForecast")]
        //public int Get()
        //{
        //    return 1;
        //}
    }
}