namespace Trips.Core.Attributes.Searchable;

[AttributeUsage(AttributeTargets.Property)]
public class SearchableAttribute : Attribute
{
    public string? CustomPropertyName { get; }

    public SearchableAttribute(string? customPropertyName = null)
    {
        CustomPropertyName = customPropertyName;
    }
}
