using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace InstanceEnums.Test.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CureController : ControllerBase
    {
        [HttpGet("{diagnosisManager}/{ageGroup}")]
        public string Get(IMedicationService diagnosisManager, AgeGroups.IAgeGroup ageGroup)
        {
            return diagnosisManager.GetPrescription((dynamic)ageGroup);
        }
    }
}