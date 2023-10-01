namespace TypedEnums;

public abstract class TypedEnumMember : ITypedEnumMember, IConvertible
{
    public int EnumValue { get; set; }

    public string Name { get; set; }

    public TypeCode GetTypeCode() => throw new NotImplementedException();

    public bool ToBoolean(IFormatProvider provider) => throw new NotImplementedException();

    public byte ToByte(IFormatProvider provider) => throw new NotImplementedException();

    public char ToChar(IFormatProvider provider) => throw new NotImplementedException();

    public DateTime ToDateTime(IFormatProvider provider) => throw new NotImplementedException();

    public decimal ToDecimal(IFormatProvider provider) => throw new NotImplementedException();

    public double ToDouble(IFormatProvider provider) => throw new NotImplementedException();

    public short ToInt16(IFormatProvider provider) => (Int16)EnumValue;

    public int ToInt32(IFormatProvider provider) => EnumValue;

    public long ToInt64(IFormatProvider provider) => EnumValue;

    public sbyte ToSByte(IFormatProvider provider) => throw new NotImplementedException();

    public float ToSingle(IFormatProvider provider) => throw new NotImplementedException();

    public string ToString(IFormatProvider provider) => this.ToString(provider);

    public object ToType(Type conversionType, IFormatProvider provider) => throw new NotImplementedException();

    public ushort ToUInt16(IFormatProvider provider) => throw new NotImplementedException();

    public uint ToUInt32(IFormatProvider provider) => throw new NotImplementedException();

    public static explicit operator int(TypedEnumMember obj) => obj.EnumValue;

    public static explicit operator string(TypedEnumMember obj) => obj.Name;

    public override bool Equals(object obj) => (obj as TypedEnumMember)?.Name == this.Name;

    public override string ToString() => Name;

    public override int GetHashCode() => HashCode.Combine(EnumValue, Name);

    public ulong ToUInt64(IFormatProvider provider) => throw new NotImplementedException();
}