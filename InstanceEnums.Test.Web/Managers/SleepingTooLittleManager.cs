using static InstanceEnums.Test.Web.Enums.DiagnosisType;

namespace InstanceEnums.Test.Web.Managers
{
    public class SleepingTooLittleManager : ISleepingTooLittle, IDiagnosisManager
    {
        public string GetFix() => "Sleeping pills, enough but not too much so that you die.";
    }
}
