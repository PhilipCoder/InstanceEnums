using InstanceEnums.Test.Web.Enums;

namespace InstanceEnums.Test.Web.Managers
{
    public class DiagnosisManager : IDiagnosisManager, DiagnosisTypes.IDiagnosisType
    {
        public string GetFix()
        {
            return "Sorry, you are pretending";
        }
    }
}
