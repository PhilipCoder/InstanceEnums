using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace InstanceEnums.Test.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CureController : ControllerBase
{
    [HttpGet("{diagnosis}/{ageGroup}")]
    public string Get(IMedicationService diagnosis, AgeGroups.IAgeGroup ageGroup)
    {
        //The diagnosis service is injected based on the diagnosis URL parameter value.
        //The correct method can then be invoked by using method overloading.
        //Please note the dynamic keyword to force the method overloading to be resolved
        //at runtime and not compile time.
        return diagnosis.GetPrescription((dynamic)ageGroup);
    }
}