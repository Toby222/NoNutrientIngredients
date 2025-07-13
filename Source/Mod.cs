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
#elif v1_6
        const string GAME_VERSION = "v1.6";
#else
#error No version defined
        const string GAME_VERSION = "UNDEFINED";
#endif

#if DEBUG
        const string BUILD = "Debug";
#else
        const string BUILD = "Release";
#endif
        Log.Message(
            $"Running Version {Assembly.GetAssembly(typeof(NoNutrientIngredients)).GetName().Version} {BUILD} compiled for RimWorld version {GAME_VERSION}"
                + BUILD
        );
        new HarmonyLib.Harmony("dev.tobot.rimworld.nonutrientingredients").PatchAll();
    }

    [HarmonyLib.HarmonyPatch(
        typeof(Building_NutrientPasteDispenser),
        nameof(Building_NutrientPasteDispenser.TryDispenseFood)
    )]
    public static class NutrientPatch
    {
        public static void Postfix(ref Thing __result) => __result.TryGetComp<CompIngredients>()?.ingredients.Clear();
    }
}
