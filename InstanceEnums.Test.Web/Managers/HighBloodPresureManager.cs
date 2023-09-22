using InstanceEnums.Test.Web.Enums;

namespace InstanceEnums.Test.Web.Managers
{
    public class HighBloodPresureManager : DiagnosisTypes.IHighBloodPresure, IDiagnosisManager
    {
        public string GetFix() => "Hipertension pills, lots of it."; 
    }
}
