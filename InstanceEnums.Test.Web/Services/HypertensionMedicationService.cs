using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.ServiceContracts;

namespace InstanceEnums.Test.Web.Services;

public class HypertensionMedicationService : DiagnosisTypes.IHypertension, IMedicationService
{
    private const string _hypertensionResult = "You have hypertension. You need to take {0} of the Blood Letting Pills. 2 Times a day, after meals. Age group: {1}";

    public string GetPrescription(AgeGroups.IAdult ageGroup) => String.Format(_hypertensionResult, "4", ageGroup.ToString());

    public string GetPrescription(AgeGroups.IChild ageGroup) => String.Format(_hypertensionResult, "2", ageGroup.ToString());

    public string GetPrescription(AgeGroups.ITeen ageGroup) => String.Format(_hypertensionResult, "3", ageGroup.ToString());

    public string GetPrescription(AgeGroups.IToddler ageGroup) => String.Format(_hypertensionResult, "0.5", ageGroup.ToString());

    public string GetPrescription(AgeGroups.IAgeGroup ageGroup) => "Unsupported age group.";
}
