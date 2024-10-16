using UnityEngine;
using Verse;

namespace Template.Settings;

public static class SettingsWindow
{
    private static Vector2 settingsScrollPosition = new();

    private static float settingsHeight;

    private static TemplateSettings Settings => TemplateMod.Settings;

    public static void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listing = new();
        Rect viewRect = new(inRect.x, inRect.y, inRect.width - 16f, settingsHeight);
        Widgets.BeginScrollView(inRect, ref settingsScrollPosition, viewRect);
        listing.Begin(new Rect(viewRect.x, viewRect.y, viewRect.width, float.PositiveInfinity));

        listing.End();
        settingsHeight = listing.CurHeight;
        Widgets.EndScrollView();
    }

    public static string SettingsCategory()
    {
        return "TemplateMod.SettingsCategory".TranslateSafe();
    }
}
