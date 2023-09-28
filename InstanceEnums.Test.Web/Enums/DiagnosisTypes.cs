using TypedEnums;

namespace InstanceEnums.Test.Web.Enums
{
    public class DiagnosisTypes : InstanceEnum<DiagnosisTypes>
    {
        public interface IInsomnia : IDiagnosisType { }

        public interface IHypertension : IDiagnosisType { }

        public interface IDiagnosisType { }
    }
}
