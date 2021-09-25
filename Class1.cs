using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;

namespace NoClip
{
    [BepInPlugin("org.kokuchi.monkeytag.noclip", "NoClip", "1.0.0.0")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.kokuchi.monkeytag.noclip");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Class1
    {
        static bool flying = false;
        static bool notclipping = false;
        static float speed = 12f;
        static bool primaryDown = false;
        static bool secondaryDown = false;
        private static void Postfix(GorillaLocomotion.Player __instance)
        {
            if (!PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
            {
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryDown);
                list[0].TryGetFeatureValue(CommonUsages.primaryButton, out primaryDown);

                if (secondaryDown)
                {
                    foreach (MeshCollider mc in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        mc.transform.position = new Vector3(-0f, -0f, -0f);
                }
            }
        }
    }
}