# Instance Enums

## Problem

Enums represent types. But with no backing type. They can only be base level reference structs. As soon as you see an enum in code, you know that code is not SOLID. 
Enums originate back to the C days, and I believe that they serve no place in modern object orientated language.

Let's show a small example. You have to write a REST API endpoint that calculates medication and the amount of pills you have to take based on diagnosis and age. If you
decide to pass enums as parameters (after all you can simply pass a number or name to represent the enum), you will be forced to either write if statements, or if you heard somewhere that switches are better than ifs, you will write switch statements:

```C#
public enum PatientAges
{
    Adult,
    Teen,
    Child,
    Toddler,
    AgeGroup 
}

public enum Diagnoses
{
    Insomnia,
    Hypertension,
    DiagnosisType
}

[HttpGet("{diagnosis}/{ageGroup}")]
public string Get(Diagnoses diagnosis, PatientAges ageGroup)
{
    int pillCount;
    string resultString;
    switch (diagnosis)
    {
        case Diagnoses.Insomnia:
            resultString = "You have struggle sleeping. You need to take {0} of our potent tranquilizers before bed time. Age group: {1}";
            switch (ageGroup)
            {
                case PatientAges.Adult:
                    pillCount = 10;
                    break;
                case PatientAges.Child:
                    pillCount = 6;
                    break;
                case PatientAges.Teen:
                    pillCount = 7;
                    break;
                case PatientAges.Toddler:
                    pillCount = 80;
                    break;
                default:
                    return "Unsupported age group.";
                }
            break;
        case Diagnoses.Hypertension:
            resultString = "You have hypertension. You need to take {0} of the Blood Letting Pills. 2 Times a day, after meals. Age group: {1}";
            switch (ageGroup)
            {
                case PatientAges.Adult:
                    pillCount = 4;
                    break;
                case PatientAges.Child:
                    pillCount = 2;
                    break;
                case PatientAges.Teen:
                    pillCount = 3;
                    break;
                case PatientAges.Toddler:
                    pillCount = 1;
                    break;
                default:
                    return "Unsupported age group.";
            }
            break;
        default:
            return "We don't know what is wrong with you. But take some random pills.";
    }
    return String.Format(resultString, pillCount, nameof(ageGroup));
}

```

The problem with the code above, is that as soon as when you want to add a new diagnosis type or age range, you have to change the code. Simply put, it is not open for extension.

## Solution

_Please see the working example in InstanceEnums.Test.Web_

If every enum value represented a interface, we can use polymorphism and even method overloading to get rid of all the conditional statements. InstanceEnum is a small library that gives types to enums.

Rewriting switches with the InstanceEnum Library:

### Creating The Instance Enums

Instance enums are records with nested interfaces. Every interface represent an enum member. The enum should also have a base interface that will be the "default" value of the enum.

__DiagnosisTypes.cs__

```csharp
public record DiagnosisTypes : InstanceEnum<DiagnosisTypes>
{
    public interface IInsomnia : IDiagnosisType { }

    public interface IHypertension : IDiagnosisType { }

    public interface IDiagnosisType { }
}
```

__AgeGroups.cs__

```csharp
public record AgeGroups : InstanceEnum<AgeGroups>
{
    public interface IAdult : IAgeGroup { }

    public interface ITeen : IAgeGroup { }

    public interface IChild : IAgeGroup { }

    public interface IToddler : IAgeGroup { }

    public interface IAgeGroup { }
}
```

As in the InstanceEnum classes above, a InstanceEnum is a class that inherits from InstanceEnum with nested interfaces. A base interface (IAgeGroup and IDiagnosisType) 
has to be declared and the Enum members are inheriting the base interface.

> Enum members are convertible to integers and strings. The integer values are incremented for each member declaration, just like normal enums. Custom names and values can be assigned.

### Activating Instance Enums Via Middleware

Instance enums are activated via middleware in Program.cs or StartUp.cs. The extension method *__ActivateEnums__* on the application builder can be used. It needs to be called just before the .Build() call:

```csharp
builder.ActivateEnums();
```

The ActivateEnums extension method will also add the following to your API:

* Model binding for WebAPI.
* Swagger data type support
* Register and cache all the instance enums and instance derived services in your project.

### Polymorphic Logic Services

You can now create a _logic service_ that uses polymorphism and method overloading to do the medication calculation without a single conditional statement:

*_Logic Service Interface:_*
```csharp
public interface IMedicationService : DiagnosisTypes.IDiagnosisType
{
    string GetPrescription(AgeGroups.IAdult ageGroup);

    string GetPrescription(AgeGroups.IChild ageGroup);

    string GetPrescription(AgeGroups.ITeen ageGroup);

    string GetPrescription(AgeGroups.IToddler ageGroup);

    string GetPrescription(AgeGroups.IAgeGroup ageGroup);
}
```

*Default Logic Service:_*
```csharp
public class MedicationService : IMedicationService, DiagnosisTypes.IDiagnosisType
{
    private const string _medicationResult = "We don't know what is wrong with you. But take some random pills.";
    public string GetPrescription(AgeGroups.IAdult adult) => _medicationResult;

    public string GetPrescription(AgeGroups.IChild child) => _medicationResult;

    public string GetPrescription(AgeGroups.ITeen teen) => _medicationResult;

    public string GetPrescription(AgeGroups.IToddler todler) => _medicationResult;

    public string GetPrescription(AgeGroups.IAgeGroup ageGroup) => _medicationResult;
}
```

*_Hypertension Medication Service_*
```csharp
public class HypertensionMedicationService : DiagnosisTypes.IHypertension, IMedicationService
{
    private const string _hypertensionResult = "You have hypertension. You need to take {0} of the Blood Letting Pills. 2 Times a day, after meals. Age group: {1}";

    public string GetPrescription(AgeGroups.IAdult ageGroup) => String.Format(_hypertensionResult, "4", ageGroup.ToString());

    public string GetPrescription(AgeGroups.IChild ageGroup) => String.Format(_hypertensionResult, "2", ageGroup.ToString());

    public string GetPrescription(AgeGroups.ITeen ageGroup) => String.Format(_hypertensionResult, "3", ageGroup.ToString());

    public string GetPrescription(AgeGroups.IToddler ageGroup) => String.Format(_hypertensionResult, "0.5", ageGroup.ToString());

    public string GetPrescription(AgeGroups.IAgeGroup ageGroup) => "Unsupported age group.";
}
```

*_Insomnia Medication Service_*
```csharp
public class InsomniaMedicationService : DiagnosisTypes.IInsomnia, IMedicationService
{
    private const string _insomniaResult = "You have struggle sleeping. You need to take {0} of our potent tranquilizers before bed time. Age group: {1}";

    public string GetPrescription(AgeGroups.IAdult ageGroup) => String.Format(_insomniaResult, "10", "adult");

    public string GetPrescription(AgeGroups.IChild ageGroup) => String.Format(_insomniaResult, "6", "child");

    public string GetPrescription(AgeGroups.ITeen ageGroup) => String.Format(_insomniaResult, "7", "teen");

    public string GetPrescription(AgeGroups.IToddler ageGroup) => String.Format(_insomniaResult, "80 (make him sleep a long time)", "toddler");

    public string GetPrescription(AgeGroups.IAgeGroup ageGroup) => "Unsupported age group.";
}
```

### Injecting The Service Based On Enum In Controller

You can now have the service as a parameter in your action, and the model binder will automatically bind the correct service instance according to the parameter passed via the request:

*_Cure Controller_*
```csharp
[ApiController]
[Route("[controller]")]
public class CureController : ControllerBase
{
    [HttpGet("{diagnosis}/{ageGroup}")]
    public string Get(IMedicationService diagnosis, AgeGroups.IAgeGroup ageGroup)
    {
        //The diagnosis service is injected based on the diagnosis URL parameter value.
        //The correct method can then be invoked by using method overloading.
        //Please note the dynamic keyword to force the method overloading to be resolved
        //at runtime and not compile time.
        return diagnosis.GetPrescription((dynamic)ageGroup);
    }
}
```

### Running The Service And Calling Via Swagger

When you run the service, you can now pass the values via Swagger to the controller and get the correct results:

![img](img/swaggerWithEnum.png)