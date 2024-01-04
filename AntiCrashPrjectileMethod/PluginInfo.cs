﻿using System;
using System.Reflection;
using HarmonyLib;

namespace Plugin
{
    public class HarmonyPatches
    {
        public static bool IsPatched { get; private set; }

        internal static void ApplyHarmonyPatches()
        {
            bool flag = !HarmonyPatches.IsPatched;
            if (flag)
            {
                bool flag2 = HarmonyPatches.instance == null;
                if (flag2)
                {
                    HarmonyPatches.instance = new Harmony("com.Bobo.Anti-Crash");
                }
                HarmonyPatches.instance.PatchAll(Assembly.GetExecutingAssembly());
                HarmonyPatches.IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            bool flag = HarmonyPatches.instance != null && HarmonyPatches.IsPatched;
            if (flag)
            {
                HarmonyPatches.instance.UnpatchSelf();
                HarmonyPatches.IsPatched = false;
            }
        }

        private static Harmony instance;

        public const string InstanceId = "com.Bobo.Anti-Crash";
    }
}
