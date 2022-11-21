using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField]
        int level = 1;

        [SerializeField]
        characterClass characterClass;

        [SerializeField]
        Progression progression = null;

        public float GetHealth()
        {
            return 0;
        }
    }
}
