using InstanceEnums.testData;
using TypedEnums;

namespace InstanceEnums.Test.Web.Enums
{
    public class DiagnosisType : PolyEnum<DiagnosisType>
    {
        public interface ISleepingTooLittle : IDiagnosisType { }

        public interface IHighBloodPresure : IDiagnosisType { }

        [InstanceEnumMember(enumType:typeof(DiagnosisType))]
        public interface IDiagnosisType { }
    }
}
