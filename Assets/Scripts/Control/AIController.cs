using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        float chaseDistance = 5f;

        [SerializeField]
        float suspicionTime = 3f;

        [SerializeField]
        PatrolPath patrolPath;

        [SerializeField]
        float waypointTolerance = 1f;

        int currentWaypointIndex = 0;

        Fighter fighter;

        GameObject player;

        Health health;

        Mover mover;

        Vector3 guardPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack (player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath)
            {
                if (AtWayPoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction (nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex =
                patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint =
                Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer =
                Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        // called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
