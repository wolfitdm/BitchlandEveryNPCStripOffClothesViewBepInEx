using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using HarmonyLib;
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
        private static ConfigEntry<bool> configUltimativeSexMonsterBitch;

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

		public static bool enableMe = false;
        public static bool ultimativeSexMonsterBitch = false;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            configEnableMe = Config.Bind(pluginKey,
                                              "EnableMe",
                                              true,
                                             "Whether or not you want enable this mod (default true also yes, you want it, and false = no)");
            

            configUltimativeSexMonsterBitch = Config.Bind(pluginKey,
                                                          "UltimativeSexMonsterBitch",
                                                          true,
                                                          "Whether or not you want make every npc to the ultimative sex monster bitch (default true also yes, you want it, and false = no)");


            enableMe = configEnableMe.Value;
            ultimativeSexMonsterBitch = configUltimativeSexMonsterBitch.Value;
			
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
            // Create a new Harmony instance with a unique ID
            var harmony = new Harmony("com.wolfitdm.BitchlandEveryNPCStripOffClothesView");

            if (originalClass == null)
            {
                Logger.LogInfo($"GetType originalClass == null");
                return;
            }

            // Or apply patches manually
            MethodInfo original = null;

            if (parameters == null)
            {
                original = AccessTools.Method(originalClass, originalMethodName);
            } else
            {
                original = AccessTools.Method(originalClass, originalMethodName, parameters);
            }

            if (original == null)
            {
                Logger.LogInfo($"AccessTool.Method original {originalMethodName} == null");
                return;
            }

            MethodInfo patched = AccessTools.Method(typeof(BitchlandEveryNPCStripOffClothesView), patchedMethodName);

            if (patched == null)
            {
                Logger.LogInfo($"AccessTool.Method patched {patchedMethodName} == null");
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

            PersonGenerated.CreatePersonRelationship();
            PersonGenerated.Favor = 100000000;
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
                    int_Person obj = hitInfo.transform.GetComponent<int_Person>();
                    int_Person obj2 = hitInfo.transform.root.GetComponent<int_Person>();

                    if (obj != null)
                    {
                        Person person = obj.ThisPerson;

                        if (person != null)
                        {
                            StripPerson(person.gameObject);
                            if (ultimativeSexMonsterBitch)
                                setPersonaltyToNympho(person.gameObject);
                        }
                    }

                    if (obj2  != null)
                    {
                       Person person2 = obj2.ThisPerson;

                       if (person2 != null)
                       {
                            StripPerson(person2.gameObject);
                            if (ultimativeSexMonsterBitch)
                                setPersonaltyToNympho(person2.gameObject);
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