﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using Verse;

namespace CookCarefully.Utilities
{
	// From https://github.com/alextd/RimWorld-EnhancementPack/blob/master/Source/Utilities/PatchCompilerGenerated.cs
	static class PatchCompilerGenerated
    {
		public static void PatchGeneratedMethod(this Harmony harmony, Type masterType, Func<MethodInfo, bool> check, HarmonyMethod prefix = null, HarmonyMethod postfix = null, HarmonyMethod transpiler = null)
		{
			// Find the compiler-created method nested in masterType that passes the check, Patch it
			List<Type> nestedTypes = new List<Type>(masterType.GetNestedTypes(BindingFlags.NonPublic));
			while (nestedTypes.Any())
			{
				Type type = nestedTypes.Pop();
				nestedTypes.AddRange(type.GetNestedTypes(BindingFlags.NonPublic));

				foreach (MethodInfo method in AccessTools.GetDeclaredMethods(type).Where(check))
				{
					harmony.Patch(method, prefix, postfix, transpiler);
				}
			}
		}
	}
}
