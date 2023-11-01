using System.Linq.Dynamic.Core;
using System.Reflection;
using Trips.Core.Attributes.Searchable;
using Trips.Core.DTO.SearchableRequest;

namespace Trips.Core.Extensions.Queryable;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, SearchableRequest? searchableRequest)
    {
        if (searchableRequest == null || string.IsNullOrEmpty(searchableRequest.OrderBy))
        {
            return query;
        }
        else
        {
            return query.OrderBy($"{searchableRequest.OrderBy} {(searchableRequest.AscendingOrder ? "asc" : "desc")}").AsQueryable();
        }
    }

    public static IQueryable<TEntity> SearchBy<TEntity, TSearchableModel>(this IQueryable<TEntity> query, string? searchBy)
    {
        if (string.IsNullOrEmpty(searchBy))
        {
            return query;
        }
        else
        {
            var searchableAttributeType = typeof(SearchableAttribute);
            var properties = typeof(TSearchableModel).GetProperties().Where(p => Attribute.IsDefined(p, searchableAttributeType));
            var searchableQueryString = string.Join(" || ", properties.Select(p => PropertyToSearchableQuery(p)));
            return query.Where(searchableQueryString, searchBy.ToUpperInvariant());
        }
    }

    private static string PropertyToSearchableQuery(PropertyInfo property)
    {
        var propertyName = property.Name;
        var propertyType = property.PropertyType;
        var searchableAttrubute = property.GetCustomAttribute<SearchableAttribute>();

        var queryPropertyName = string.IsNullOrEmpty(searchableAttrubute.CustomPropertyName) ? propertyName : searchableAttrubute.CustomPropertyName;
        return propertyType == typeof(string) ? $"{queryPropertyName}.ToUpper().Contains(@0)" : $"{queryPropertyName}.ToString().ToUpper().Contains(@0)";
    }
}
