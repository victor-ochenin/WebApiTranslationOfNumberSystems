using System;

namespace WebApiTranslationOfNumberSystems.Controllers
{
    public record ConvertRequest(string Value, int FromBase, int ToBase);
    public record ErrorMessage(string Type, string Message);
    public record StringMessage(string Message, DateTime Time);
    public record ConversionResultMessage(string Result);
} 