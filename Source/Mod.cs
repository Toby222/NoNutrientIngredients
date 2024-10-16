using System.Reflection;
using RimWorld;
using Verse;

#if DEBUG
#warning Compiling in Debug mode
#endif

namespace NoNutrientIngredients;

[StaticConstructorOnStartup]
public static class NoNutrientIngredients
{
    static NoNutrientIngredients()
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
        Log.Message(
            $"Running Version {Assembly.GetAssembly(typeof(NoNutrientIngredients)).GetName().Version} {build} compiled for RimWorld version {GAME_VERSION}"
                + build
        );
        new HarmonyLib.Harmony("dev.tobot.rimworld.nonutrientingredients").PatchAll();
    }

    [HarmonyLib.HarmonyPatch(
        typeof(Building_NutrientPasteDispenser),
        nameof(Building_NutrientPasteDispenser.TryDispenseFood)
    )]
	[JetBrains.Annotations.UsedImplicitly]
    public static class NutrientPatch
    {
        static void Postfix(ref Thing __result)
        {
            __result.TryGetComp<CompIngredients>()?.ingredients.Clear();
        }
    }
}
