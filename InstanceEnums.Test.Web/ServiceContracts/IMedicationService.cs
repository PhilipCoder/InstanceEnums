using InstanceEnums.Test.Web.Enums;

namespace InstanceEnums.Test.Web.ServiceContracts;

public interface IMedicationService : DiagnosisTypes.IDiagnosisType
{
    string GetPrescription(AgeGroups.IAdult ageGroup);

    string GetPrescription(AgeGroups.IChild ageGroup);

    string GetPrescription(AgeGroups.ITeen ageGroup);

    string GetPrescription(AgeGroups.IToddler ageGroup);

    string GetPrescription(AgeGroups.IAgeGroup ageGroup);
}
