using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Verse;

namespace Template.Settings;

public class TemplateSettings : ModSettings
{
    private static TemplateSettings DefaultValues() => new();

    #region Scribe Helpers
    private void LookStruct<T>(Expression<Func<T>> expression)
    {
        if (
            expression.Body
            is not MemberExpression
            {
                Member: MemberInfo { MemberType: MemberTypes.Field, Name: string memberName }
            }
        )
        {
            throw new ArgumentException(
                "Invalid expression passed to LookField",
                nameof(expression)
            );
        }

        FieldInfo fieldInfo = typeof(TemplateSettings).GetField(memberName);
        T? value = fieldInfo.GetValue(this).ChangeType<T>();
        T defaultValue = fieldInfo.GetValue(DefaultValues()).ChangeType<T>();
        Scribe_Values.Look(ref value, memberName, defaultValue);
        fieldInfo.SetValue(this, value);
    }

    private static void LookHashSet<T>(
        ref HashSet<T> valueHashSet,
        string label,
        HashSet<T> defaultValues
    )
        where T : notnull
    {
        if (Scribe.mode == LoadSaveMode.Saving && valueHashSet is null)
        {
            TemplateMod.Warn(label + " is null before saving. Reinitializing with default values.");
            valueHashSet = defaultValues;
        }
        Scribe_Collections.Look(ref valueHashSet, label, lookMode: LookMode.Value);
        if (Scribe.mode == LoadSaveMode.LoadingVars && valueHashSet is null)
        {
            TemplateMod.Warn(label + " is null after loading. Reinitializing with default values.");
            valueHashSet = defaultValues;
        }
    }
    #endregion

    public bool TemplateEnabled;

    public TemplateSettings()
    {
        Reset();
    }

    public void Reset()
    {
        TemplateEnabled = true;
    }

    public override void ExposeData()
    {
        base.ExposeData();

        LookStruct(() => TemplateEnabled);
    }
}
