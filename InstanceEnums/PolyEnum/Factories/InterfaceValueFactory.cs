using System.Reflection;
using System.Reflection.Emit;

namespace InstanceEnums;

public class InterfaceValueFactory
{
    private readonly string _dynamicModuleName = "MainModule";
    private AssemblyBuilder _assemblyBuilder;
    private AssemblyName _assemblyName;
    private ModuleBuilder _moduleBuilder;

    public B CreateNewObject<B>(Type interfaceType) where B : class
    {
        var myType = CompileResultType(interfaceType, typeof(B));
        var myObject = Activator.CreateInstance(myType);
        return (B)myObject;
    }

    private Type CompileResultType(Type interfaceType, Type baseType)
    {
        TypeBuilder typeBuilder = GetTypeBuilder(interfaceType.Name);
        typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
        typeBuilder.AddInterfaceImplementation(interfaceType);
        typeBuilder.SetParent(baseType);
        Type objectType = typeBuilder.CreateType();
        return objectType;
    }

    private TypeBuilder GetTypeBuilder(string typeName)
    {
        _assemblyName = _assemblyName ?? new AssemblyName(Guid.NewGuid().ToString());
        _assemblyBuilder = _assemblyBuilder ?? AssemblyBuilder.DefineDynamicAssembly(_assemblyName, AssemblyBuilderAccess.Run);

        _moduleBuilder = _moduleBuilder ??  _assemblyBuilder.DefineDynamicModule(_dynamicModuleName);

        TypeBuilder typeBuilder = _moduleBuilder.DefineType(
            typeName,
            TypeAttributes.Public |
            TypeAttributes.Class |
            TypeAttributes.AutoClass |
            TypeAttributes.AnsiClass |
            TypeAttributes.BeforeFieldInit |
            TypeAttributes.AutoLayout,
                null);

        return typeBuilder;
    }
}
