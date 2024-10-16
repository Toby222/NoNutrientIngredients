using System.Reflection;
// using HarmonyLib;
using Verse;

namespace Template;

using Settings;
using UnityEngine;

#if DEBUG
#warning Compiling in Debug mode
#endif

public class TemplateMod : Mod
{
    public TemplateMod(ModContentPack content)
        : base(content)
    {
#if v1_5
        const string GAME_VERSION = "v1.5";
#else
#error No version defined
        const string GAME_VERSION = "UNDEFINED";
#endif

#if DEBUG
        const string build = "Debug";
#else
        const string build = "Release";
#endif
        Log(
            $"Running Version {Assembly.GetAssembly(typeof(TemplateMod)).GetName().Version} {build} compiled for RimWorld version {GAME_VERSION}"
                + build
        );

        Log(content.ModMetaData.packageIdLowerCase);

        Settings = GetSettings<TemplateSettings>();
        WriteSettings();

        // Harmony harmony = new(content.ModMetaData.packageIdLowerCase);
    }

#nullable disable // Set in constructor.

    public static TemplateSettings Settings { get; private set; }

#nullable enable

    public override void DoSettingsWindowContents(Rect inRect) =>
        SettingsWindow.DoSettingsWindowContents(inRect);

    public override string SettingsCategory() => SettingsWindow.SettingsCategory();

    const string LogPrefix = "Toby's Template Mod - ";

    public static void DebugError(string message, int? key = null)
    {
#if DEBUG
        Error(message, key);
#endif
    }

    public static void Error(string message, int? key = null)
    {
        if (key is int keyNotNull)
            Verse.Log.ErrorOnce(LogPrefix + message, keyNotNull);
        else
            Verse.Log.Error(LogPrefix + message);
    }

    public static void DebugWarn(string message, int? key = null)
    {
#if DEBUG
        Warn(message, key);
#endif
    }

    public static void Warn(string message, int? key = null)
    {
        if (key is int keyNotNull)
            Verse.Log.WarningOnce(LogPrefix + message, keyNotNull);
        else
            Verse.Log.Warning(LogPrefix + message);
    }

    public static void DebugLog(string message)
    {
#if DEBUG
        Log(message);
#endif
    }

    public static void Log(string message)
    {
        Verse.Log.Message(LogPrefix + message);
    }
}
