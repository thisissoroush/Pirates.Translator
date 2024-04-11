
namespace Pirates.Translator.Primitives.Enums;


[AttributeUsage(AttributeTargets.Field)]
internal class CodeAttribute : Attribute
{
    internal string Code { get; private set; }

    internal CodeAttribute(string code)
    {
        Code = code;
    }
}