using static InstanceEnums.Test.Web.Enums.DiagnosisType;

namespace InstanceEnums.Test.Web.Managers
{
    public class HighBloodPresureManager : IHighBloodPresure, IDiagnosisManager
    {
        public string GetFix() => "Hipertension pills, lots of it."; 
    }
}
