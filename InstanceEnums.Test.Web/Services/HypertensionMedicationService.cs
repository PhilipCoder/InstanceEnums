using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.ServiceContracts;

namespace InstanceEnums.Test.Web.Services
{
    public class HypertensionMedicationService : DiagnosisTypes.IHypertension, IMedicationService
    {
        private const string _hypertensionResult = "You have hypertension. You need to take {0} of the Blood Letting Pills. 2 Times a day, after meals. Age group: {1}";

        public string GetPrescription(AgeGroups.IAdult adult)
        {
            return String.Format(_hypertensionResult, "4", "adult");
        }

        public string GetPrescription(AgeGroups.IChild child)
        {
            return String.Format(_hypertensionResult, "2", "child");
        }

        public string GetPrescription(AgeGroups.ITeen teen)
        {
            return String.Format(_hypertensionResult, "3", "teen");
        }

        public string GetPrescription(AgeGroups.IToddler todler)
        {
            return String.Format(_hypertensionResult, "0.5", "todler");
        }

        public string GetPrescription(AgeGroups.IAgeGroup ageGroup)
        {
            return "Unsupported age group.";
        }
    }
}
