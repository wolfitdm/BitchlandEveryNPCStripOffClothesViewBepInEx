using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using Den.Tools;
using HarmonyLib;
using SemanticVersioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BitchlandEveryNPCStripOffClothesView
{
    [BepInPlugin("com.wolfitdm.BitchlandEveryNPCStripOffClothesView", "BitchlandEveryNPCStripOffClothesView Plugin", "1.0.0.0")]
    public class BitchlandEveryNPCStripOffClothesView : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private static ConfigEntry<bool> configEnableMe;
        private static ConfigEntry<bool> configMaxRelationShipIfYouHaveSex;
        private static ConfigEntry<bool> configUltimativeSexMonsterBitch;
        private static ConfigEntry<bool> configEveryNPCstripsNudeIfYouLookAtThem;
        private static ConfigEntry<KeyCode> configKeyCodeStripModeStartSex;

        public BitchlandEveryNPCStripOffClothesView()
        {
        }

        public static Type MyGetType(string originalClassName)
        {
            return Type.GetType(originalClassName + ",Assembly-CSharp");
        }

        public static Type MyGetTypeUnityEngine(string originalClassName)
        {
            return Type.GetType(originalClassName + ",UnityEngine");
        }

        private static string pluginKey = "General.Toggles";
        private static string pluginKeyControlsStripMode = "StripMode.KeyControls";

        public static bool enableMe = false;
        public static bool maxRelationShipIfYouHaveSex = false;
        public static bool ultimativeSexMonsterBitch = false;
        public static bool everyNPCstripsNudeIfYouLookAtThem = false;
        public static KeyCode stripModeSexKey = 0;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            configEnableMe = Config.Bind(pluginKey,
                                              "EnableMe",
                                              true,
                                             "Whether or not you want enable this mod (default true also yes, you want it, and false = no)");

            configMaxRelationShipIfYouHaveSex = Config.Bind(pluginKey,
                   "MaxRelationShipIfYouHaveSex",
                   true,
                  "Whether or not you want max relationship if you have sex (default true also yes, you want it, and false = no)");


            configEveryNPCstripsNudeIfYouLookAtThem = Config.Bind(pluginKey,
                                              "EveryNPCstripsNudeIfYouLookAtThem",
                                              true,
                                              "Whether or not you want that every npc strips nude if you look at them (default true also yes, you want it, and false = no)");

            configUltimativeSexMonsterBitch = Config.Bind(pluginKey,
                                                          "UltimativeSexMonsterBitch",
                                                          true,
                                                          "Whether or not you want make every npc to the ultimative sex monster bitch (default true also yes, you want it, and false = no)");

            configKeyCodeStripModeStartSex = Config.Bind(pluginKeyControlsStripMode,
                     "KeyCodeStripModeStartSex",
                      KeyCode.F6,
                     "Key to start sex, default F6");

            enableMe = configEnableMe.Value;
            maxRelationShipIfYouHaveSex = configMaxRelationShipIfYouHaveSex.Value;
            everyNPCstripsNudeIfYouLookAtThem = configEveryNPCstripsNudeIfYouLookAtThem.Value;
            ultimativeSexMonsterBitch = configUltimativeSexMonsterBitch.Value;
            stripModeSexKey = configKeyCodeStripModeStartSex.Value;

            PatchAllHarmonyMethods();

            Logger.LogInfo($"Plugin BitchlandEveryNPCStripOffClothesView BepInEx is loaded!");
        }
		
		public static void PatchAllHarmonyMethods()
        {
			if (!enableMe)
            {
                return;
            }
			
            try
            {
                PatchHarmonyMethodUnity(typeof(WeaponSystem), "Update", "WeaponSystem_Update", true, false);
            } catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }
        }
        public static void PatchHarmonyMethodUnity(Type originalClass, string originalMethodName, string patchedMethodName, bool usePrefix, bool usePostfix, Type[] parameters = null)
        {
            string uniqueId = "com.wolfitdm.BitchlandEveryNPCStripOffClothesView";
            Type uniqueType = typeof(BitchlandEveryNPCStripOffClothesView);

            // Create a new Harmony instance with a unique ID
            var harmony = new Harmony(uniqueId);

            if (originalClass == null)
            {
                Logger.LogInfo($"GetType originalClass == null");
                return;
            }

            MethodInfo patched = null;

            try
            {
                patched = AccessTools.Method(uniqueType, patchedMethodName);
            }
            catch (Exception ex)
            {
                patched = null;
            }

            if (patched == null)
            {
                Logger.LogInfo($"AccessTool.Method patched {patchedMethodName} == null");
                return;

            }

            // Or apply patches manually
            MethodInfo original = null;

            try
            {
                if (parameters == null)
                {
                    original = AccessTools.Method(originalClass, originalMethodName);
                }
                else
                {
                    original = AccessTools.Method(originalClass, originalMethodName, parameters);
                }
            }
            catch (AmbiguousMatchException ex)
            {
                Type[] nullParameters = new Type[] { };
                try
                {
                    if (patched == null)
                    {
                        parameters = nullParameters;
                    }

                    ParameterInfo[] parameterInfos = patched.GetParameters();

                    if (parameterInfos == null || parameterInfos.Length == 0)
                    {
                        parameters = nullParameters;
                    }

                    List<Type> parametersN = new List<Type>();

                    for (int i = 0; i < parameterInfos.Length; i++)
                    {
                        ParameterInfo parameterInfo = parameterInfos[i];

                        if (parameterInfo == null)
                        {
                            continue;
                        }

                        if (parameterInfo.Name == null)
                        {
                            continue;
                        }

                        if (parameterInfo.Name.StartsWith("__"))
                        {
                            continue;
                        }

                        Type type = parameterInfos[i].ParameterType;

                        if (type == null)
                        {
                            continue;
                        }

                        parametersN.Add(type);
                    }

                    parameters = parametersN.ToArray();
                }
                catch (Exception ex2)
                {
                    parameters = nullParameters;
                }

                try
                {
                    original = AccessTools.Method(originalClass, originalMethodName, parameters);
                }
                catch (Exception ex2)
                {
                    original = null;
                }
            }
            catch (Exception ex)
            {
                original = null;
            }

            if (original == null)
            {
                Logger.LogInfo($"AccessTool.Method original {originalMethodName} == null");
                return;
            }

            HarmonyMethod patchedMethod = new HarmonyMethod(patched);
            var prefixMethod = usePrefix ? patchedMethod : null;
            var postfixMethod = usePostfix ? patchedMethod : null;

            harmony.Patch(original,
                prefix: prefixMethod,
                postfix: postfixMethod);
        }

        public static void StripPerson(GameObject personGa)
        {
            if (personGa == null) return;

            Person person = personGa.GetComponent<Person>();

            if (person == null) return;

            if (person.CurrentTop != null)
            {
                person.UndressClothe(person.CurrentTop);
            }

            if (person.CurrentUnderwearTop != null)
            {
                person.UndressClothe(person.CurrentUnderwearTop);
            }

            if (person.CurrentUnderwearLower != null)
            {
                person.UndressClothe(person.CurrentUnderwearLower);
            }

            if (person.CurrentPants != null)
            {
                person.UndressClothe(person.CurrentPants);
            }

            if (person.CurrentShoes != null)
            {
                person.UndressClothe(person.CurrentShoes);
            }

            if (person.CurrentSocks != null)
            {
                person.UndressClothe(person.CurrentSocks);
            }

            if (person.CurrentHat != null)
            {
                person.UndressClothe(person.CurrentHat);
            }

            if (person.CurrentGarter != null)
            {
                person.UndressClothe(person.CurrentGarter);
            }

            if (person.CurrentAnys != null)
            {
                List<Dressable> anys = new List<Dressable>();
                for (int i = 0; i < person.CurrentAnys.Count; i++)
                {
                    anys.Add(person.CurrentAnys[i]);
                }

                for (int i = 0; i < anys.Count; i++)
                {
                    person.UndressClothe(anys[i]);
                }
            }

            if (person.CurrentBeard != null)
            {
                person.UndressClothe(person.CurrentBeard);
            }
        }

        public static void addallperkstoperson(GameObject personGa)
        {
            if (personGa == null)
            {
                return;
            }

            Person person = personGa.GetComponent<Person>();

            if (person == null)
            {
                return;
            }

            if (!person.Perks.Contains("Gaping"))
            {
                person.Perks.Add("Gaping");
            }

            if (!person.Perks.Contains("Smell"))
            {
                person.Perks.Add("Smell");
            }

            if (!person.Perks.Contains("Vaginal Storage"))
            {
                person.Perks.Add("Vaginal Storage");
            }

            if (!person.Perks.Contains("Anal Storage"))
            {
                person.Perks.Add("Anal Storage");
            }

            if (!person.Perks.Contains("Mining Skill lvl 2"))
            {
                person.Perks.Add("Mining Skill lvl 2");
            }

            if (!person.Perks.Contains("Sensetivity"))
            {
                person.Perks.Add("Sensetivity");
            }

            if (!person.Perks.Contains("Longer Orgasm"))
            {
                person.Perks.Add("Longer Orgasm");
            }

            if (!person.Perks.Contains("Prostitution skill lvl 1"))
            {
                person.Perks.Add("Prostitution skill lvl 1");
            }

            if (!person.Perks.Contains("Prostitution skill lvl 2"))
            {
                person.Perks.Add("Prostitution skill lvl 2");
            }

            if (!person.Perks.Contains("Prostitution skill lvl 3"))
            {
                person.Perks.Add("Prostitution skill lvl 3");
            }

            if (!person.Perks.Contains("Prostitution skill lvl 4"))
            {
                person.Perks.Add("Prostitution skill lvl 4");
            }

            if (!person.Perks.Contains("Fluid Gather"))
            {
                person.Perks.Add("Fluid Gather");
            }

            if (!person.Perks.Contains("Trash3"))
            {
                person.Perks.Add("Trash3");
            }

            misc_Perk[] objectsOfType = UnityEngine.Object.FindObjectsOfType<misc_Perk>(true);
            for (int index = 0; index < objectsOfType.Length; ++index)
            {
                string perk = objectsOfType[index].PerkID;
                if (!person.Perks.Contains(perk))
                    person.Perks.Add(perk);
            }
        }
        public static void setPersonaltyToNympho(GameObject personGa)
        {
            if (personGa == null)
            {
                return;
            }

            Person PersonGenerated = personGa.GetComponent<Person>();

            if (PersonGenerated == null)
            {
                return;
            }

            addallperkstoperson(personGa);

            //PersonGenerated.Personality = Personality_Type.Nympho;
            addAllFetishesToPerson(personGa);
            setmaxallskills(personGa, 300);

            if (maxRelationShipIfYouHaveSex)
            {
                PersonGenerated.CreatePersonRelationship();
                PersonGenerated.Favor = 100000000;
                PersonGenerated.SexMultiplier = 1.5f;
                PersonGenerated.SexMAddictionultiplier = 2.0f;
            }
        }

        public static void setmaxarousal(GameObject personGa, int level)
        {
            if (personGa == null)
            {
                return;
            }

            Person person = personGa.GetComponent<Person>();

            if (person == null)
            {
                return;
            }

            int add = 300;
            level = level > 0 && level <= add ? level : add;
            add = level;
            person.Arousal = add;
        }
        public static void setmaxsexskills(GameObject personGa, int level)
        {
            if (personGa == null)
            {
                return;
            }

            Person person = personGa.GetComponent<Person>();

            if (person == null)
            {
                return;
            }

            int add = 300;
            level = level > 0 && level <= add ? level : add;
            add = level;
            person.SexSkills = add;
            int sexMax = person.SexXpThisLvlMax;
            sexMax = sexMax >= 0 ? sexMax : add;
            person.SexXpThisLvl = sexMax;
        }

        public static void setmaxworkskills(GameObject personGa, int level)
        {
            if (personGa == null)
            {
                return;
            }

            Person person = personGa.GetComponent<Person>();

            if (person == null)
            {
                return;
            }

            int add = 300;
            level = level > 0 && level <= add ? level : add;
            add = level;
            person.WorkSkills = add;
            int workMax = person.WorkXpThisLvlMax;
            workMax = workMax >= 0 ? workMax : add;
            person.WorkXpThisLvl = workMax;
        }

        public static void setmaxarmyskills(GameObject personGa, int level)
        {
            if (personGa == null)
            {
                return;
            }

            Person person = personGa.GetComponent<Person>();

            if (person == null)
            {
                return;
            }

            int add = 300;
            level = level > 0 && level <= add ? level : add;
            add = level;
            person.ArmySkills = add;
            int armyMax = person.ArmyXpThisLvlMax;
            armyMax = armyMax >= 0 ? armyMax : add;
            person.ArmyXpThisLvl = armyMax;
        }

        public static void setmaxallskills(GameObject personGa, int level)
        {
            if (personGa == null)
            {
                return;
            }

            Person person = personGa.GetComponent<Person>();

            if (person == null)
            {
                return;
            }

            setmaxsexskills(person.gameObject, level);
            setmaxworkskills(person.gameObject, level);
            setmaxarmyskills(person.gameObject, level);
            setmaxarousal(person.gameObject, level);

            person.AnalTraining = level;
            person.VaginalTraining = level;
            person.NippleTraining = level;
            person.ClitTraining = level;
            person.BodyTraining = level;

            person.SexMultiplier = 1.5f;
            person.SexMAddictionultiplier = 2.0f;
        }


        public static void addAllFetishesToPerson(GameObject personGa, bool cleanOrDirt = true)
        {
            if (personGa == null)
            {
                return;
            }

            Person PersonGenerated = personGa.GetComponent<Person>();

            if (PersonGenerated == null)
            {
                return;
            }

            if (PersonGenerated.Fetishes == null)
            {
                PersonGenerated.Fetishes = new List<e_Fetish>();
            }

            PersonGenerated.Fetishes.Clear();
            PersonGenerated.Fetishes.Add(e_Fetish.Dildo);
            PersonGenerated.Fetishes.Add(e_Fetish.Pregnant);
            PersonGenerated.Fetishes.Add(e_Fetish.Anal);
            PersonGenerated.Fetishes.Add(e_Fetish.Scat);
            PersonGenerated.Fetishes.Add(e_Fetish.Masochist);
            PersonGenerated.Fetishes.Add(e_Fetish.Clean);
            PersonGenerated.Fetishes.Add(e_Fetish.Futa);
            PersonGenerated.Fetishes.Add(e_Fetish.Machine);
            PersonGenerated.Fetishes.Add(e_Fetish.Sadist);
            PersonGenerated.Fetishes.Add(e_Fetish.Oral);
            PersonGenerated.Fetishes.Add(e_Fetish.Outdoors);

            List<e_Fetish> fetishes = Enum.GetValues(typeof(e_Fetish)).Cast<e_Fetish>().ToList();

            for (int i = 0; i < fetishes.Count; i++)
            {
                if (fetishes[i] == e_Fetish.Clean || fetishes[i] == e_Fetish.Dirty || fetishes[i] == e_Fetish.MAX)
                {
                    continue;
                }

                if (!PersonGenerated.Fetishes.Contains(fetishes[i]))
                {
                    PersonGenerated.Fetishes.Add(fetishes[i]);
                }
            }

            if (cleanOrDirt)
            {
                if (!PersonGenerated.Fetishes.Contains(e_Fetish.Clean))
                {
                    PersonGenerated.Fetishes.Add(e_Fetish.Clean);
                }
            }
            else
            {
                if (!PersonGenerated.Fetishes.Contains(e_Fetish.Dirty))
                {
                    PersonGenerated.Fetishes.Add(e_Fetish.Dirty);
                }
            }
        }

        private static List<GameObject> getPrefabsByName(string prefab)
        {
            List<GameObject> Prefabs = null;

            try
            {
                if (prefab == null)
                {
                    Prefabs = Main.Instance.AllPrefabs;
                    return Prefabs;
                }

                switch (prefab)
                {
                    case "Any":
                        {
                            Prefabs = Main.Instance.Prefabs_Any;
                        }
                        break;

                    case "Shoes":
                        {
                            Prefabs = Main.Instance.Prefabs_Shoes;
                        }
                        break;

                    case "Pants":
                        {
                            Prefabs = Main.Instance.Prefabs_Pants;
                        }
                        break;

                    case "Top":
                        {
                            Prefabs = Main.Instance.Prefabs_Top;
                        }
                        break;

                    case "UnderwearTop":
                        {
                            Prefabs = Main.Instance.Prefabs_UnderwearTop;
                        }
                        break;

                    case "UnderwearLower":
                        {
                            Prefabs = Main.Instance.Prefabs_UnderwearLower;
                        }
                        break;

                    case "Garter":
                        {
                            Prefabs = Main.Instance.Prefabs_Garter;
                        }
                        break;

                    case "Socks":
                        {
                            Prefabs = Main.Instance.Prefabs_Socks;
                        }
                        break;

                    case "Hat":
                        {
                            Prefabs = Main.Instance.Prefabs_Hat;
                        }
                        break;

                    case "Hair":
                        {
                            Prefabs = Main.Instance.Prefabs_Hair;
                        }
                        break;

                    case "MaleHair":
                        {
                            Prefabs = Main.Instance.Prefabs_MaleHair;
                        }
                        break;

                    case "Bodies":
                        {
                            Prefabs = Main.Instance.Prefabs_Bodies;
                        }
                        break;

                    case "Heads":
                        {
                            Prefabs = Main.Instance.Prefabs_Heads;
                        }
                        break;

                    case "Beards":
                        {
                            Prefabs = Main.Instance.Prefabs_Beards;
                        }
                        break;

                    case "ProstSuit1":
                        {
                            Prefabs = Main.Instance.Prefabs_ProstSuit1;
                        }
                        break;

                    case "ProstSuit2":
                        {
                            Prefabs = Main.Instance.Prefabs_ProstSuit2;
                        }
                        break;

                    case "Weapons":
                        {
                            Prefabs = null;
                        }
                        break;

                    default:
                        {
                            Prefabs = Main.Instance.AllPrefabs;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                Prefabs = new List<GameObject>();
            }

            return Prefabs;
        }

        private static GameObject getItemByName(string prefab, string name)
        {
            List<GameObject> Prefabs = getPrefabsByName(prefab);

            if (Prefabs == null || Prefabs.Count == 0)
            {
                return null;
            }

            if (name == null || name.Length == 0)
            {
                return null;
            }

            name = name.ToLower();

            int length = Prefabs.Count;
            for (int i = 0; i < length; i++)
            {
                if (Prefabs[i].IsNull())
                {
                    continue;
                }

                string iname = Prefabs[i].name;
                iname = iname.ToLower().Replace(" ", "_");

                if (iname == name)
                {
                    return Prefabs[i];
                }
            }
            return null;
        }

        public static GameObject SafeSpawn(GameObject go)
        {
            if (go == null)
            {
                return null;
            }

            Int_Storage storage_hands = Main.Instance.Player.Storage_Hands;
            Transform storage_hands_dropspot = storage_hands.DropSpot;
            if (storage_hands_dropspot != null)
            {
                return SafeSpawnFromStorage(go, storage_hands);
            }

            Int_Storage storage_anal = Main.Instance.Player.Storage_Anal;
            Transform storage_anal_dropspot = storage_anal.DropSpot;
            if (storage_anal_dropspot != null)
            {
                return SafeSpawnFromStorage(go, storage_anal);
            }

            Int_Storage storage_vaginal = Main.Instance.Player.Storage_Vag;
            Transform storage_vaginal_dropspot = storage_vaginal.DropSpot;
            if (storage_vaginal_dropspot != null)
            {
                return SafeSpawnFromStorage(go, storage_vaginal);
            }

            Int_Storage backpack = Main.Instance.Player.CurrentBackpack != null && Main.Instance.Player.CurrentBackpack.ThisStorage != null ? Main.Instance.Player.CurrentBackpack.ThisStorage : null;
            Transform backpack_dropspot = backpack != null ? backpack.DropSpot : null;
            if (backpack_dropspot != null)
            {
                return SafeSpawnFromStorage(go, backpack);
            }

            return SafeSpawnFromStorage(go, null);
        }

        public static GameObject SafeSpawnFromStorage(GameObject go, Int_Storage storage)
        {
            if (go == null)
            {
                return null;
            }

            Transform storageSpot = storage == null ? null : storage.DropSpot;
            GameObject item = Main.Spawn(go);

            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            if (storageSpot != null)
            {
                position = storageSpot.position;
                rotation = storageSpot.rotation;
                item.transform.SetPositionAndRotation(position, rotation);
            }
            else
            {
                item.transform.SetLocalPositionAndRotation(position, rotation);
            }

            item.transform.SetParent((Transform)null, true);
            Rigidbody componentInChildren1 = item.GetComponentInChildren<Rigidbody>(false);
            Collider componentInChildren2 = item.GetComponentInChildren<Collider>(false);
            if (componentInChildren1 != null)
                componentInChildren1.isKinematic = false;
            if (componentInChildren2 != null)
                componentInChildren2.enabled = true;
            item.SetActive(true);
            return item;
        }

        public static void putdildoonhand(GameObject personGa, string item)
        {
            if (personGa == null)
            {
                return;
            }

            Person person1Ex = personGa.GetComponent<Person>();

            if (person1Ex == null)
            {
                return;
            }

            if (!person1Ex.Perks.Contains("Gaping"))
            {
                person1Ex.Perks.Add("Gaping");
            }

            if (item == null)
            {
                return;
            }

            if (!item.StartsWith("dildo"))
            {
                return;
            }

            GameObject itemSpawned = getItemByName(null, item);

            if (itemSpawned == null)
            {
                return;
            }

            GameObject dildo = SafeSpawn(itemSpawned);

            if (dildo == null)
            {
                return;
            }

            int_dildo dildoX = dildo.GetComponentInChildren<int_dildo>(true);

            if (dildoX == null)
            {
                return;
            }

            person1Ex.Anim.Play("pickup_10");
            person1Ex.PutOnHand(dildoX.RootObj, dildoX.BackPos, dildoX.BackRot);
        }
        public static void npcspawnsexsceneex(bool havePlayer, GameObject person1, GameObject person2, GameObject person3, int sextype = 2, int pose = 0, bool force = false)
        {
            Person person1Ex = null;
            Person person2Ex = null;
            Person person3Ex = null;

            if (person1 != null)
            {
                person1Ex = person1.GetComponent<Person>();
                if (person1Ex == null)
                {
                    return;
                }
                person1Ex.transform.position = Main.Instance.Player.transform.position;

                if (maxRelationShipIfYouHaveSex)
                {
                    if (!person1Ex.IsPlayer)
                    {
                        person1Ex.CreatePersonRelationship();
                        person1Ex.Favor = 100000000;
                    }
                }

                if (sextype == 1)
                {
                    putdildoonhand(person1Ex.gameObject, "dildo8large");
                }
            }

            if (person2 != null)
            {
                person2Ex = person2.GetComponent<Person>();
                if (person2Ex == null)
                {
                    return;
                }
                person2Ex.transform.position = Main.Instance.Player.transform.position;
                if (maxRelationShipIfYouHaveSex)
                {
                    if (!person2Ex.IsPlayer)
                    {
                        person2Ex.CreatePersonRelationship();
                        person2Ex.Favor = 100000000;
                    }
                }
                if (sextype == 1)
                {
                    putdildoonhand(person2Ex.gameObject, "dildo8large");
                }
            }

            if (person3 != null)
            {
                person3Ex = person3.GetComponent<Person>();
                if (person3Ex == null)
                {
                    return;
                }
                person3Ex.transform.position = Main.Instance.Player.transform.position;
                if (maxRelationShipIfYouHaveSex)
                {
                    if (!person3Ex.IsPlayer)
                    {
                        person3Ex.CreatePersonRelationship();
                        person3Ex.Favor = 100000000;
                    }
                }
                if (sextype == 1)
                {
                    putdildoonhand(person3Ex.gameObject, "dildo8large");
                }
            }

            Person player = person1Ex;
            Person person = person2Ex;

            SpawnedSexScene scene = null;

            Main.Instance.GameplayMenu.AllowCursor();
            bool canControl = havePlayer;
            if (player != null && player.HasPenis)
            {
                scene = Main.Instance.SexScene.SpawnSexScene(sextype, pose, player, person, person3Ex, false, canControl, force);
            }
            else if (person != null && person.HasPenis)
            {
                scene = Main.Instance.SexScene.SpawnSexScene(sextype, pose, person, player, person3Ex, false, canControl, force);
            }
            else
            {
                scene = Main.Instance.SexScene.SpawnSexScene(sextype, pose, player, person, person3Ex, false, canControl, force);
            }

            if (scene != null)
            {
                Main.Instance.GameplayMenu.AllowCursor();
                if (!canControl)
                {
                    scene.TimerForRandomPoseChange = true;
                    scene.TimerMax = UnityEngine.Random.Range(10f, 20f);
                    scene.TimerPoseChange = scene.TimerMax;
                    scene.TimerForRandomSexEnd = true;
                    scene.TimerSexEnd = UnityEngine.Random.Range(60f, 120f);
                }
            }
        }

        public static bool WeaponSystem_Update(object __instance)
        {
            if (!enableMe)
            {
                return true;
            }

            WeaponSystem _this = (WeaponSystem)__instance;

			try
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(_this.transform.position, _this.transform.TransformDirection(Vector3.forward), out hitInfo, _this.RayDistance, (int)_this.PromptLayers))
                {
                    Interactible[] obj__ = hitInfo.transform.GetComponents<Interactible>();
                    Interactible[] obj2__ = hitInfo.transform.root.GetComponents<Interactible>();

                    if (obj__ != null)
                    {
                        for (int i = 0; i < obj__.Length; i++)
                        {
                            Interactible obj_ = obj__[i];

                            if (obj_ == null)
                            {
                                continue;
                            }

                            if (!(obj_ is int_Person))
                            {
                                continue;
                            }

                            int_Person obj = (int_Person)obj_;

                            if (obj != null)
                            {
                                Person person = obj.ThisPerson;

                                if (person != null)
                                {
                                    if (everyNPCstripsNudeIfYouLookAtThem)
                                    {
                                        StripPerson(person.gameObject);
                                    }
                                    
                                    if (ultimativeSexMonsterBitch)
                                    {
                                        setPersonaltyToNympho(person.gameObject);
                                    }

                                    if (Input.GetKeyUp(stripModeSexKey))
                                    {
                                        npcspawnsexsceneex(true, Main.Instance.Player.gameObject, person.gameObject, null);
                                    }
                                }
                            }
                        }
                    }

                    if (obj2__ != null)
                    {
                        for (int i = 0; i < obj2__.Length; i++)
                        {
                            Interactible obj2_ = obj2__[i];

                            if (obj2_ == null)
                            {
                                continue;
                            }

                            if (!(obj2_ is int_Person))
                            {
                                continue;
                            }

                            int_Person obj2 = (int_Person)obj2_;

                            if (obj2 != null)
                            {
                                Person person = obj2.ThisPerson;

                                if (person != null)
                                {
                                    if (everyNPCstripsNudeIfYouLookAtThem)
                                    {
                                        StripPerson(person.gameObject);
                                    }

                                    if (ultimativeSexMonsterBitch)
                                    {
                                        setPersonaltyToNympho(person.gameObject);
                                    }

                                    if (Input.GetKeyUp(stripModeSexKey))
                                    {
                                        npcspawnsexsceneex(true, Main.Instance.Player.gameObject, person.gameObject, null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return true;
        }
    }
}