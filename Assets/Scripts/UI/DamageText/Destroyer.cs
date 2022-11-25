using UnityEngine;

namespace RPG.UI.DamageText
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField]
        GameObject targetToDestroy = null;

        public void DestroyTarget()
        {
            Destroy (targetToDestroy);
        }
    }
}
