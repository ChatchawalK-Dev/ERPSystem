using System.Reflection;

public static class ReflectionHelper
{
    public static void SetProperty<T>(object obj, string propertyName, T value)
    {
        var property = obj.GetType().GetProperty(propertyName);
        if (property != null && property.CanWrite)
        {
            property.SetValue(obj, value);
        }
    }
    public static object? GetPropertyValue(object obj, string propertyName)
    {
        var property = obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new ArgumentException($"Property {propertyName} not found.");
        }

        // Return the value, which may be null
        return property.GetValue(obj);
    }

    public static void SetPropertyValue<T>(T obj, string propertyName, object value)
    {
        var property = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (property == null) throw new ArgumentException("Property not found");
        property.SetValue(obj, value);
    }
}
