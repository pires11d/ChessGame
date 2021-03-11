using System.ComponentModel;
using System.Linq;

namespace System
{
    public static class EnumExtension
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static string GetDescription<T>(this T value) where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum) return null;

            var fi = type.GetField(value.ToString());

            if (fi == null) return string.Empty;

            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

            return attribute?.Description ?? value.ToString();
        }

        public static int GetCode<T>(this T value) where T : struct
        {
            var type = typeof(T);

            return !type.IsEnum ? default(int) : Convert.ToInt32(value);
        }
    }
}
