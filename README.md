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

If every enum value represented a interface, we can use polymorphism and even method overloading to get rid of all the conditional statements. InstanceEnum is a small library that gives types to enums.

Rewriting switches with the InstanceEnum Library:

1) First we have to create the diagnosis and age group enums:

__DiagnosisTypes.cs__

```csharp
using TypedEnums;

public class DiagnosisTypes : InstanceEnum<DiagnosisTypes>
{
    public interface IInsomnia : IDiagnosisType { }

    public interface IHypertension : IDiagnosisType { }

    public interface IDiagnosisType { }
}
```

__AgeGroups.cs__

```csharp
using TypedEnums;

public class AgeGroups : InstanceEnum<AgeGroups>
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

2) Instance Enums must be registered in the EnumRegistry:

__Program.cs__

```csharp
var builder = WebApplication.CreateBuilder(args);
//Register your enums just after the web application builder is created:
EnumRegistry.RegisterEnum<DiagnosisTypes, DiagnosisTypes.IDiagnosisType>();
EnumRegistry.RegisterEnum<AgeGroups, AgeGroups.IAgeGroup>();
```

In order to enable the instance enums in Swagger, we 