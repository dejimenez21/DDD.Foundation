using System.Reflection;

namespace DDD.CommonKit.Domain;

public abstract class Enumeration
{
    public string Name { get; }

    protected Enumeration(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Enumeration otherValue)
            return false;

        return Name.Equals(otherValue.Name);
    }

    public override int GetHashCode() => Name.GetHashCode();

    public static TEnumeration FromName<TEnumeration>(string name) where TEnumeration : Enumeration
    {
        return GetAll<TEnumeration>()
            .SingleOrDefault(mt => string.Equals(mt.Name, name, StringComparison.OrdinalIgnoreCase))
            ?? throw new ArgumentException($"The specified name '{name}' is not a valid {typeof(TEnumeration).Name}.", nameof(name));
    }

    public static IEnumerable<TEnumeration> GetAll<TEnumeration>() where TEnumeration : Enumeration
    {
        var fields = typeof(TEnumeration).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        return fields.Select(f => f.GetValue(null)).Cast<TEnumeration>();
    }
}