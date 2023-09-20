using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.Model;
using Microsoft.AspNetCore.Mvc;
using static InstanceEnums.Test.Web.Enums.DiagnosisType;

namespace InstanceEnums.Test.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        [HttpGet("{diagnosisType}")]
        public string Get(IDiagnosisType diagnosisType)
        {
            return GetDiagnosis((dynamic)diagnosisType);
        }

        private string GetDiagnosis(DiagnosisType.IHighBloodPresure value) {
            return "You are about to burst";
        }

        private string GetDiagnosis(DiagnosisType.ISleepingTooLittle value)
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