using InstanceEnums.PolyEnum.ModelBinding;
using InstanceEnums.testData;
using System.ComponentModel;
using System.Globalization;
using TypedEnums;

namespace InstanceEnums.Test.Web.Enums
{
    public class DiagnosisTypes : PolyEnum<DiagnosisTypes>
    {
        public interface ISleepingTooLittle : IDiagnosisType { }

        public interface IHighBloodPresure : IDiagnosisType { }

        //[TypeConverter(typeof(SomeWrapperTypeTypeConverter))]
        public interface IDiagnosisType { }
    }
}
