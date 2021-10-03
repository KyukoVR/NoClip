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
    public class FlyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.kokuchi.monkeytag.noclip");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Fly
    {
        static bool flying = false;
        static bool notclipping = false;
        static float kissingmonkeys = 10f;
        private static void Postfix(GorillaLocomotion.Player __instance)
        {
            if (!PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
            {
                    List<InputDevice> list = new List<InputDevice>();
                    InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                    list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flying);

                    if (flying)
                    {
                        __instance.bodyCollider.attachedRigidbody.velocity = __instance.leftHandTransform.transform.forward * kissingmonkeys;
                    }
                }
            }
        }
    }
}
