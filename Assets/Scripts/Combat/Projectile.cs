using RPG.Attributes;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        float speed = 1f;

        [SerializeField]
        bool isHoming = false;

        [SerializeField]
        GameObject hitEffect = null;

        [SerializeField]
        float maxLifeTime = 10f;

        [SerializeField]
        GameObject[] destroyOnHit = null;

        [SerializeField]
        float lifeAfterImpact = 2f;

        Health target = null;

        GameObject instigator = null;

        float damage = 0f;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null)
            {
                return;
            }

            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target)
            {
                return;
            }

            // to fix arrow disappear when collide with dead enemy
            if (target.IsDead())
            {
                return;
            }

            target.TakeDamage (instigator, damage);

            speed = 0;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy (toDestroy);
            }

            Destroy(this.gameObject, lifeAfterImpact);
        }

        public void SetTarget(
            Health target,
            GameObject instigator,
            float damage
        )
        {
            this.target = target;
            this.instigator = instigator;
            this.damage = damage;

            Destroy (gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule =
                target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position +
            Vector3.up * targetCapsule.height / 2;
        }
    }
}
