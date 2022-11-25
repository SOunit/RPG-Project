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

        public void Spawn(float damage)
        {
            DamageText instance =
                Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}
