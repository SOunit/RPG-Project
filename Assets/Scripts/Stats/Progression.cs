using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [
        CreateAssetMenu(
            fileName = "Progression",
            menuName = "Stats/New Progression",
            order = 0)
    ]
    public class Progression : ScriptableObject
    {
        [SerializeField]
        ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>>
            lookupTable = null;

        public float
        GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            // foreach (ProgressionCharacterClass
            //     progressionClass
            //     in
            //     characterClasses
            // )
            // {
            //     if (progressionClass.characterClass != characterClass)
            //     {
            //         continue;
            //     }
            //     foreach (ProgressionStat
            //         progressionStat
            //         in
            //         progressionClass.stats
            //     )
            //     {
            //         if (progressionStat.stat != stat)
            //         {
            //             continue;
            //         }
            //         if (progressionStat.levels.Length < level)
            //         {
            //             continue;
            //         }
            //         return progressionStat.levels[level - 1];
            //     }
            // }
            return 0;
        }

        private void BuildLookup()
        {
            if (lookupTable != null)
            {
                return;
            }

            lookupTable =
                new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass
                progressionClass
                in
                characterClasses
            )
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat
                    progressionStat
                    in
                    progressionClass.stats
                )
                {
                    statLookupTable[progressionStat.stat] =
                        progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        [Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;

            public ProgressionStat[] stats;

            // public float[] health;
        }

        [Serializable]
        class ProgressionStat
        {
            public Stat stat;

            public float[] levels;
        }
    }
}
