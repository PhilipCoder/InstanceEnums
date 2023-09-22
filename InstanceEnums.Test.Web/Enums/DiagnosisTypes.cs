using InstanceEnums.testData;
using TypedEnums;

namespace InstanceEnums.Test.Web.Enums
{
    public class DiagnosisTypes : PolyEnum<DiagnosisTypes>
    {
        public interface ISleepingTooLittle : IDiagnosisType { }

        public interface IHighBloodPresure : IDiagnosisType { }

        [InstanceEnumMember(enumType:typeof(DiagnosisTypes))]
        public interface IDiagnosisType { }
    }
}
