using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.Model;
using Microsoft.AspNetCore.Mvc;
using InstanceEnums.Test.Web.Enums;
using InstanceEnums.PolyEnum.ModelBinding;
using System.ComponentModel;

namespace InstanceEnums.Test.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        [TypeConverter(typeof(SomeWrapperTypeTypeConverter))]
        [HttpGet("{diagnosisType}")]
        public string Get( DiagnosisTypes.IDiagnosisType diagnosisType)
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

        private string GetDiagnosis(DiagnosisTypes.IDiagnosisType value)
        {
            return "I don't know what is wrong";
        }

    }

    public enum Numbers
    {
        one, two, three, four, five
    }
}