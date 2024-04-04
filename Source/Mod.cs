using System.Reflection;
// using HarmonyLib;
using Verse;

namespace Template;

using Settings;
using UnityEngine;

public class TemplateMod : Mod
{
    internal static string Translate(string key)
    {
        const string TranslationKey = nameof(TemplateMod);
        return (TranslationKey + "." + key).Translate();
    }

    public TemplateMod(ModContentPack content)
        : base(content)
    {
#if DEBUG
        const string build = "Debug";
#else
        const string build = "Release";
#endif
        Log.Message(
            $"Running Version {Assembly.GetAssembly(typeof(TemplateMod)).GetName().Version} "
                + build
        );

        Log.Message(content.ModMetaData.packageIdLowerCase);

        Settings = GetSettings<TemplateSettings>();
        WriteSettings();

        // Harmony harmony = new(content.ModMetaData.packageIdLowerCase);
    }

#nullable disable // Set in constructor.

    public static TemplateSettings Settings { get; private set; }

#nullable enable

    public void ResetSettings()
    {
        Settings = new();
    }

    public override void DoSettingsWindowContents(Rect inRect) =>
        SettingsWindow.DoSettingsWindowContents(inRect);

    public override string SettingsCategory() => SettingsWindow.SettingsCategory();

    public static class Log
    {
        const string LogPrefix = "Toby's Template Mod - ";

        public static void DebugError(string message)
        {
#if DEBUG
            Error(message);
#endif
        }

        public static void Error(string message)
        {
            Verse.Log.Error(LogPrefix + message);
        }

        public static void DebugWarn(string message)
        {
#if DEBUG
            Warn(message);
#endif
        }

        public static void Warn(string message)
        {
            Verse.Log.Warning(LogPrefix + message);
        }

        public static void DebugLog(string message)
        {
#if DEBUG
            Message(message);
#endif
        }

        public static void Message(string message)
        {
            Verse.Log.Message(LogPrefix + message);
        }
    }
}
