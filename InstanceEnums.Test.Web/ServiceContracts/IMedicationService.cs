using InstanceEnums.Test.Web.Enums;

namespace InstanceEnums.Test.Web.ServiceContracts
{
    public interface IMedicationService : DiagnosisTypes.IDiagnosisType
    {
        string GetPrescription(AgeGroups.IAdult adult);

        string GetPrescription(AgeGroups.IChild child);

        string GetPrescription(AgeGroups.ITeen teen);

        string GetPrescription(AgeGroups.ITodler todler);

        string GetPrescription(AgeGroups.IAgeGroup ageGroup);
    }
}
