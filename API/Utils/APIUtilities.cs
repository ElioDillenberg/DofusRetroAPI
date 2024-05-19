using System.Reflection;

namespace DofusRetroAPI.Utils;

public static class APIUtilities
{
    public static T? UpdateNullProperties<T>(T instance1, T instance2) where T : class
    {
        if (instance1 == null || instance2 == null) return null;

        // Get the type of the class
        Type type = typeof(T);

        // Get all the properties of the class
        PropertyInfo[] properties = type.GetProperties();

        foreach (var property in properties)
        {
            // Check if the property type is nullable
            if (Nullable.GetUnderlyingType(property.PropertyType) != null)
            {
                // Get the value of the property from both instances
                var value1 = property.GetValue(instance1);
                var value2 = property.GetValue(instance2);

                // If the value in instance1 is null, update it with the value from instance2
                if (value1 == null && value2 != null)
                {
                    property.SetValue(instance1, value2);
                }
            }
        }

        return instance1;
    }
}