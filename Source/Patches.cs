using RimWorld;
using Verse;

namespace NoNutrientIngredients
{
    [StaticConstructorOnStartup]
    public static class NoNutrientIngredientsMain
    {
        static NoNutrientIngredientsMain()
        {
            new HarmonyLib.Harmony("tobs.nonutrientingredients.mod").PatchAll();
        }

        [HarmonyLib.HarmonyPatch(typeof(Building_NutrientPasteDispenser), nameof(Building_NutrientPasteDispenser.TryDispenseFood))]
        public static class NutrientPatch
        {
            static void Postfix(ref Thing __result)
            {
                __result.TryGetComp<CompIngredients>()?.ingredients.Clear();
            }
        }
    }
}
