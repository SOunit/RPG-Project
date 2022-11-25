using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField]
        DamageText damageTextPrefab = null;

        private void Start()
        {
            Spawn(11f);
        }

        public void Spawn(float damageAmount)
        {
            DamageText instance =
                Instantiate<DamageText>(damageTextPrefab, transform);
            instance.SetValue (damageAmount);
        }
    }
}
