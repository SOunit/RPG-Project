using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        float weaponRange = 2f;

        [SerializeField]
        float timeBetweenAttacks = 1f;

        [SerializeField]
        float weaponDamage = 5f;

        Transform target;

        float timeSinceLastAttack = 0f;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            // do nothing if there's no combat target
            if (target == null)
            {
                return;
            }

            // if target exist, do them
            if (!GetIsInRange())
            {
                // follow target if exist
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                // stop with distance
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // this will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        // Animation Event
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage (weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <
            weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
