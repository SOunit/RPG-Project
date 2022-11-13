using System.Collections;
using System.Collections.Generic;
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
                if (timeSinceLastAttack > timeBetweenAttacks)
                {
                    AttackBehaviour();
                    timeSinceLastAttack = 0f;
                }
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
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

        // Animation Event
        void Hit()
        {
        }
    }
}
