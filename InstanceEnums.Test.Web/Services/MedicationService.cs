using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.ServiceContracts;

namespace InstanceEnums.Test.Web.Services;

public class MedicationService : IMedicationService, DiagnosisTypes.IDiagnosisType
{
    private const string _medicationResult = "We don't know what is wrong with you. But take some random pills.";
    public string GetPrescription(AgeGroups.IAdult adult) => _medicationResult;

    public string GetPrescription(AgeGroups.IChild child) => _medicationResult;

    public string GetPrescription(AgeGroups.ITeen teen) => _medicationResult;

    public string GetPrescription(AgeGroups.IToddler todler) => _medicationResult;

    public string GetPrescription(AgeGroups.IAgeGroup ageGroup) => _medicationResult;
}
