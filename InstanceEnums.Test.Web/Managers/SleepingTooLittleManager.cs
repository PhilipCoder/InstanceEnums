using InstanceEnums.Test.Web.Enums;

namespace InstanceEnums.Test.Web.Managers
{
    public class SleepingTooLittleManager : DiagnosisTypes.ISleepingTooLittle, IDiagnosisManager
    {
        public string GetFix() => "Sleeping pills, enough but not too much so that you die.";
    }
}
