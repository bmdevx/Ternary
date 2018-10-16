using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using Ternary.Components;
using System.Text.RegularExpressions;

namespace Ternary.Reflection
{
    //TODO handle recursion levels
    public static class ComponentTools
    {
        public static void SetComponentNames<T>(T component, string prefix = null)
        {
            foreach (FieldInfo fi in component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                if (fi.FieldType.Namespace.StartsWith("Ternary"))
                {
                    if (IsComponentType(fi.FieldType))
                        SetComponentName(fi, component);
                    
                    ///handle arrays
                    else if (fi.FieldType.IsArray)
                    {
                        int dimensions = Regex.Matches(fi.FieldType.FullName, ",").Count + 1;

                        if (GetArrayElementType(fi, out Type type) && IsComponentType(type))
                        {
                            switch (dimensions)
                            {
                                case 1:
                                    {
                                        Array arr = fi.GetValue(component) as Array;
                                        for (int i = 0; i < arr.Length; i++)
                                            SetComponentNameU(arr.GetValue(i), $"{fi.Name}[{i}] ({type.Name})", prefix);
                                        break;
                                    }
                                case 2:
                                    {
                                        Array arr = fi.GetValue(component) as Array;
                                        int len = (int)Math.Sqrt(arr.Length) + 1;
                                        for (int i = 0; i < len; i++)
                                        {
                                            for (int j = 0; j < len; j++)
                                            {
                                                try
                                                {
                                                    SetComponentNameU(arr.GetValue(i, j), $"{fi.Name}[{i}][{j}] ({type.Name})", prefix);
                                                }
                                                catch { }
                                            }
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        Array arr = fi.GetValue(component) as Array;
                                        int len = (int)Math.Pow(arr.Length, 1.0/3.0) + 1;
                                        for (int i = 0; i < len; i++)
                                        {
                                            for (int j = 0; j < len; j++)
                                            {
                                                for (int k = 0; k < len; k++)
                                                {
                                                    try
                                                    {
                                                        SetComponentNameU(arr.GetValue(i, j, k), $"{fi.Name}[{i}][{j}][{k}] ({type.Name})", prefix);
                                                    }
                                                    catch { }
                                                }
                                            }
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
        }

        private static void SetComponentName(FieldInfo fi, object component, string prefix = null)
        {
            PropertyInfo cn = fi.FieldType.GetProperty("ComponentName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            cn.SetValue(fi.GetValue(component), $"{(prefix != null ? $"{prefix} : " : String.Empty)}{fi.Name}");
        }

        private static void SetComponentNameU(object component, string name, string prefix = null)
        {
            if (component != null)
            {
                PropertyInfo cn = component.GetType().GetProperty("ComponentName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                cn.SetValue(component, name);
            }
        }

        private static bool IsComponentType(Type type)
        {
            return typeof(IComponentOutput).IsAssignableFrom(type) || typeof(IMultiOutComponent).IsAssignableFrom(type) ||
                    typeof(IBusComponentOutput).IsAssignableFrom(type) || typeof(IMultiBusOutComponent).IsAssignableFrom(type);
        }

        private static bool GetArrayElementType(FieldInfo field, out Type elementType)
        {
            if (field.FieldType.IsArray && field.FieldType.FullName.EndsWith("]"))
            {
                string fullName = field.FieldType.FullName.Substring(0, field.FieldType.FullName.IndexOf("["));
                elementType = Type.GetType(string.Format("{0},{1}", fullName, field.FieldType.Assembly.GetName().Name));
                return true;
            }
            elementType = null;
            return false;
        }
    }
}
