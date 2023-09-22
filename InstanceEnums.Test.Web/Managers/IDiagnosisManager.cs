using InstanceEnums.Test.Web.Enums;

namespace InstanceEnums.Test.Web.Managers
{
    public interface IDiagnosisManager : DiagnosisTypes.IHighBloodPresure
    {
        string GetFix();
    }
}
