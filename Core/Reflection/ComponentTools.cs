using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using Ternary.Components;

namespace Ternary.Reflection
{
    public static class ComponentTools
    {
        public static void SetComponentNames<T>(T component)
        {
            foreach (FieldInfo fi in component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                if (fi.FieldType.Namespace.StartsWith("Ternary") && (
                    typeof(IComponentOutput).IsAssignableFrom(fi.FieldType) || typeof(IMultiOutComponent).IsAssignableFrom(fi.FieldType) ||
                    typeof(IBusComponentOutput).IsAssignableFrom(fi.FieldType) || typeof(IMultiBusOutComponent).IsAssignableFrom(fi.FieldType)))
                {
                    PropertyInfo cn = fi.FieldType.GetProperty("ComponentName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    cn.SetValue(fi.GetValue(component), fi.Name);
                }
            }
        }
    }
}
