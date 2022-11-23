using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField]
        float regenerationPercentage = 70;

        float healthPoints = -1f;

        bool isDead = false;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        private void RegenerateHealth()
        {
            float regenerateHealthPoints =
                GetComponent<BaseStats>().GetStat(Stat.Health) *
                (regenerationPercentage / 100);

            healthPoints = Mathf.Max(healthPoints, regenerateHealthPoints);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print($"{gameObject.name} took damage: {damage}");

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience (instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null)
            {
                return;
            }

            experience
                .GainExperience(GetComponent<BaseStats>()
                    .GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100 *
            (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead)
            {
                return;
            }

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            this.healthPoints = (float) state;

            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}
