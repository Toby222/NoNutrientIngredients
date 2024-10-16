global using static Template.Extensions.TranslateExtension;

namespace Template.Extensions;

static class TranslateExtension
{
    public static Verse.TaggedString TranslateSafe(
        this string self,
        params Verse.NamedArgument[] args
    )
    {
        if (!Verse.Translator.CanTranslate(self))
        {
            TemplateMod.DebugError(
                $"Untranslated key: {self}",
                Verse.GenString.GetHashCodeSafe(self)
            );
        }
        return Verse.TranslatorFormattedStringExtensions.Translate(self, args);
    }
}
