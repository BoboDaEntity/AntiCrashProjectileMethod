using System;
using BepInEx;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Plugin
{
    [BepInPlugin("com.Bobo.Anti-Crash", "Anti-Crash", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        private void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        private void OnGUI()
        {
            GUI.backgroundColor = Color.clear;
            Main.Toggle = GUI.Toggle(new Rect(-10f, 1070f, 200f, 200f), Main.Toggle, " ");
            if (Main.Toggle)
            {
                GUI.skin.label.fontSize = 15;
                GUI.skin.label.fontStyle = FontStyle.BoldAndItalic;
                GUI.color = Color.magenta;
                GUI.backgroundColor = Color.black;
                this.modcheck = GUILayout.Toggle(this.modcheck, "ModCheck", Array.Empty<GUILayoutOption>());
                if (this.modcheck && PhotonNetwork.InRoom)
                {
                    GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
                    foreach (Player player in PhotonNetwork.PlayerList)
                    {
                        string[] array = new string[7];
                        array[0] = "<b>( USERID : <color=grey>";
                        array[1] = player.UserId;
                        array[2] = "</color>, NAME : <color=grey>";
                        array[3] = player.NickName;
                        array[4] = "</color>, UTILLA MODS GUIDS : <color=red>";
                        int num = 5;
                        object obj = player.CustomProperties["mods"];
                        array[num] = ((obj != null) ? obj.ToString() : null);
                        array[6] = "</color>)</b>\n";
                        GUILayout.Label(string.Concat(array), Array.Empty<GUILayoutOption>());
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.Space(10f);
                if (PhotonNetwork.InRoom)
                {
                    GUILayout.Label("In Room", Array.Empty<GUILayoutOption>());
                }
            }
        }

        public static bool Toggle = true;

        private bool modcheck;
    }
}
