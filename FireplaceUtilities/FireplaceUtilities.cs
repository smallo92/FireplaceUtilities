using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FireplaceUtilities
{
    [BepInPlugin("smallo.mods.fireplaceutilities", "Fireplace Utilities", "1.0.2")]
    [HarmonyPatch]
    class FireplaceUtilitiesPlugin : BaseUnityPlugin
    {
        private static ConfigEntry<bool> enableMod;
        private static ConfigEntry<string> blacklistBurn;
        private static ConfigEntry<bool> giveLeftoverFuel;
        private static ConfigEntry<bool> torchBurn;
        private static ConfigEntry<bool> giveCoal;
        private static ConfigEntry<bool> torchUseCoal;
        private static ConfigEntry<bool> burnItems;
        private static ConfigEntry<bool> extinguishItems;
        private static ConfigEntry<int> coalAmount;
        public static ConfigEntry<string> extinguishString;
        public static ConfigEntry<string> burnItemString;
        public static ConfigEntry<string> keyBurnCodeString;
        public static ConfigEntry<string> keyBurnTextString;
        public static ConfigEntry<string> keyPOCodeString;
        public static ConfigEntry<string> keyPOTextString;

        public static List<string> notAllowed;
        public static KeyCode configBurnKey;
        public static KeyCode configPOKey;

        void Awake()
        {
            enableMod = Config.Bind("1 - Global", "Enable Mod", true, "Enable or disable this mod");

            burnItems = Config.Bind("2 - Toggles", "Burn Items In Fire", true, "Allows you to burn items in fires");
            extinguishItems = Config.Bind("2 - Toggles", "Extinguish Fires", true, "Allows you to turn fires off");
            torchUseCoal = Config.Bind("2 - Toggles", "Torch and Sconce Use Coal", true, "Makes the Wood/Iron Torch and Sconce use Coal as fuel instead of coal");

            torchBurn = Config.Bind("3 - Burn Items In Fire", "Burn In Torches", false, "Allows items to be burnt in ground torches, wall torches or braziers");
            giveCoal = Config.Bind("3 - Burn Items In Fire", "Give Coal", true, "Returns coal when burning an item");
            blacklistBurn = Config.Bind("3 - Burn Items In Fire", "Blacklist Items", "$item_wood", "Items that aren't allowed to be burned. Seperate items by a comma. Wood should remain as a default so that way it doesn't take your wood twice when lighting a fire, if you have a mod that allows other wood types to burn, put them on this list.");
            burnItemString = Config.Bind("3 - Burn Items In Fire", "Burn Item Text", "Burn item", "The text to show when hovering over the fire");
            coalAmount = Config.Bind("3 - Burn Items In Fire", "Coal Amount", 1, "Amount of coal to give when burning an item");
            keyBurnCodeString = Config.Bind("3 - Burn Items In Fire", "Burn Key", "LeftShift", "The key to use in combination with the hotkeys. KeyCodes can be found here https://docs.unity3d.com/ScriptReference/KeyCode.html");
            keyBurnTextString = Config.Bind("3 - Burn Items In Fire", "Burn Key Text", "LShift", "The custom text to show for the string, if you set it to \"none\" then it'll use what you have in the 'Key' config option.");

            giveLeftoverFuel = Config.Bind("4 - Extinguish Fires", "Give Back Fuel", true, "Returns the remaining fuel left back. Which is 1 less then the amount in the fire, since technically 1 is currently being burnt.");
            extinguishString = Config.Bind("4 - Extinguish Fires", "Extinguish Fire Text", "Extinguish fire", "The text to show when hovering over the fire");
            keyPOCodeString = Config.Bind("4 - Extinguish Fires", "Put Out Fire Key", "LeftAlt", "The key to use to put out a fire. KeyCodes can be found here https://docs.unity3d.com/ScriptReference/KeyCode.html");
            keyPOTextString = Config.Bind("4 - Extinguish Fires", "Put Out Fire Key Text", "LAlt", "The custom text to show for the string, if you set it to \"none\" then it'll use what you have in the 'Key' config option.");

            notAllowed = blacklistBurn.Value.Replace(" ", "").Split(',').ToList();
            configBurnKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyBurnCodeString.Value);
            configPOKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyPOCodeString.Value);

            if (!enableMod.Value) return;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        public static Fireplace GetAndCheckFireplace(Player player)
        {
            Fireplace fireplace = player.GetHoverObject()?.GetComponentInParent<Fireplace>();
            if (fireplace == null) return null;
            Fireplace fireNet = fireplace.GetComponent<ZNetView>().GetComponent<Fireplace>();
            if (fireNet == null) return null;
            if (!fireNet.IsBurning()) return null;
            if (fireNet.m_wet) return null;

            return fireNet;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Fireplace), "Awake")]
        public static void FireplaceAwake_Patch(Fireplace __instance)
        {
            if (!torchUseCoal.Value) return;

            GameObject coalPrefab = ZNetScene.instance.GetPrefab("Coal");
            if (__instance.name.Contains("groundtorch") && !__instance.name.Contains("green") || __instance.name.Contains("walltorch"))
            {
                __instance.m_fuelItem = coalPrefab.GetComponent<ItemDrop>();
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Fireplace), "GetHoverText")]
        public static string FireplaceGetHoverText_Patch(string __result, Fireplace __instance)
        {
            string result = __result;

            if (!__instance.IsBurning()) return result;
            if (__instance.m_wet) return result;

            if (extinguishItems.Value)
            {
                string keyExtinguishText = keyPOTextString.Value != "none" ? keyPOTextString.Value : keyPOCodeString.Value;
                result += $"\n[<color=yellow><b>{keyExtinguishText}</b></color>] {extinguishString.Value}";
            }

            if (burnItems.Value)
            {
                if (!torchBurn.Value)
                {
                    string name = __instance.name;
                    if (name.Contains("groundtorch") || name.Contains("walltorch") || name.Contains("brazier")) return result;
                }

                string keyBurnText = keyBurnTextString.Value != "none" ? keyBurnTextString.Value : keyBurnCodeString.Value;
                result += $"\n[<color=yellow><b>{keyBurnText} + 1-8</b></color>] {burnItemString.Value}";
            }

            return result;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "Update")]
        public static void PlayerUpdate_Patch(Player __instance)
        {
            var player = __instance;
            bool keyBurnCode = Input.GetKey(configBurnKey);
            bool keyPOCode = Input.GetKey(configPOKey);

            if (keyPOCode && extinguishItems.Value)
            {
                Fireplace fireNet = GetAndCheckFireplace(player);
                if (fireNet == null) return;

                float fuelAmount = Mathf.Floor(fireNet.m_nview.GetZDO().GetFloat("fuel"));
                GameObject fuelPrefab = ZNetScene.instance.GetPrefab(fireNet.m_fuelItem.name);
                fireNet.m_fuelAddedEffects.Create(fireNet.transform.position, fireNet.transform.rotation);
                fireNet.m_nview.GetZDO().Set("fuel", 0f);

                if (!giveLeftoverFuel.Value) return;
                for (int i = 0; i < (int)fuelAmount; i++)
                {
                    Instantiate(fuelPrefab, fireNet.transform.position + Vector3.up, Quaternion.identity).GetComponent<Character>();
                }
            }

            for (int i = 1; i < 9; i++)
            {
                if (keyBurnCode && Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i.ToString())) && burnItems.Value)
                {
                    Fireplace fireNet = GetAndCheckFireplace(player);
                    if (fireNet == null) return;

                    if (!torchBurn.Value)
                    {
                        string name = fireNet.name;
                        if (name.Contains("groundtorch") || name.Contains("walltorch") || name.Contains("brazier")) return;
                    }

                    Inventory inventory = __instance.GetInventory();
                    ItemDrop.ItemData item = inventory.GetItemAt(i - 1, 0);
                    if (item == null) return;

                    if (!notAllowed.Contains(item.m_shared.m_name))
                    {
                        inventory.RemoveOneItem(item);
                        fireNet.m_fuelAddedEffects.Create(fireNet.transform.position, fireNet.transform.rotation);
                        MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "");
                        if (item.IsEquipable()) __instance.ToggleEquiped(item);

                        if (!giveCoal.Value) return;
                        GameObject coalPrefab = ZNetScene.instance.GetPrefab("Coal");
                        for (int num = 0; num < coalAmount.Value; num++)
                        {
                            Instantiate(coalPrefab, fireNet.transform.position + Vector3.up, Quaternion.identity).GetComponent<Character>();
                        }
                    }
                }
            }
        }
    }
}