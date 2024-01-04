using System;
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace Plugin
{
    [HarmonyPatch(typeof(GorillaGameManager), "LaunchSlingshotProjectile", MethodType.Normal)]

    public class AntiCrashPatch
    {
        private static bool Prefix(Vector3 slingshotLaunchLocation, Vector3 slingshotLaunchVelocity, int projHash, int trailHash, bool forLeftHand, int projectileCount, bool shouldOverrideColor, float colorR, float colorG, float colorB, float colorA, PhotonMessageInfo info)
        {
            bool flag = info.Sender != PhotonNetwork.LocalPlayer;
            if (flag)
            {
                bool flag2 = Vector3.Distance(GorillaGameManager.instance.FindPlayerVRRig(info.Sender).transform.position, slingshotLaunchLocation) > 1.5f;
                if (flag2)
                {
                    return false;
                }
                bool flag3 = Vector3.Distance(slingshotLaunchLocation, Player.Instance.transform.position) > 10f;
                if (flag3)
                {
                    return false;
                }
                bool flag4 = ObjectPools.instance.GetPoolByHash(projHash).objectToPool.GetComponent<SlingshotProjectileTrail>() != null;
                if (flag4)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

