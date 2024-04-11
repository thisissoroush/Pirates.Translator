using Pirates.Translator.Service.Primitives.Enums;

namespace Pirates.Translator.Service.Primitives.Extensions;

public static class EnumExtensions
{
    public static string GetCode(this Enum value)
    {
        CodeAttribute[] array = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(CodeAttribute), inherit: false) as CodeAttribute[];
        if (array.Length == 0)
        {
            return null;
        }

        return array[0].Code;
    }
}
