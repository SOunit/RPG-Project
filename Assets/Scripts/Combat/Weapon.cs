using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    [
        CreateAssetMenu(
            fileName = "Weapon",
            menuName = "Weapons/Make New Weapon",
            order = 0)
    ]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        AnimatorOverrideController animatorOverride = null;

        [SerializeField]
        GameObject equippedPrefab = null;

        [SerializeField]
        float weaponDamage = 5f;

        [SerializeField]
        float percentageBonus = 0;

        [SerializeField]
        float weaponRange = 2f;

        [SerializeField]
        bool isRightHanded = true;

        [SerializeField]
        Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(
            Transform rightHand,
            Transform leftHand,
            Animator animator
        )
        {
            DestroyOldWeapon (rightHand, leftHand);
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);

                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }

            var overrideController =
                animator.runtimeAnimatorController as
                AnimatorOverrideController;

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController)
            {
                // 1. prev animator, do nothing
                // 2. already been overwritten, then have to update to default
                animator.runtimeAnimatorController =
                    overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            // find old weapon
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }

            // not found
            if (oldWeapon == null)
            {
                return;
            }

            // destroy old weapon
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }

            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(
            Transform rightHand,
            Transform leftHand,
            Health target,
            GameObject instigator,
            float calculatedDamage
        )
        {
            Projectile projectileInstance =
                Instantiate(projectile,
                GetTransform(rightHand, leftHand).position,
                Quaternion.identity);

            projectileInstance.SetTarget (target, instigator, calculatedDamage);
        }

        public float GetRange()
        {
            return weaponRange;
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        public float GetDamage()
        {
            return weaponDamage;
        }
    }
}
