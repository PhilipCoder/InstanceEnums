using InstanceEnums.Test.Web.Enums;
using InstanceEnums.Test.Web.ServiceContracts;

namespace InstanceEnums.Test.Web.Services
{
    public class InsomniaMedicationService : DiagnosisTypes.IInsomnia, IMedicationService
    {
        private const string _insomniaResult = "You have struggle sleeping. You need to take {0} of our potent tranqulizers before bed time. Age group: {1}";

        public string GetPrescription(AgeGroups.IAdult adult)
        {
            return String.Format(_insomniaResult, "10", "adult");
        }

        public string GetPrescription(AgeGroups.IChild child)
        {
            return String.Format(_insomniaResult, "6", "child");
        }

        public string GetPrescription(AgeGroups.ITeen teen)
        {
            return String.Format(_insomniaResult, "7", "teen");
        }

        public string GetPrescription(AgeGroups.ITodler todler)
        {
            return String.Format(_insomniaResult, "80 (make him sleep a long time)", "todler");
        }

        public string GetPrescription(AgeGroups.IAgeGroup ageGroup)
        {
            return "Unsupported age group.";
        }
    }
}
