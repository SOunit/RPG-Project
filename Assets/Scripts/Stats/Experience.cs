using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{ }

public class Experience : MonoBehaviour, ISaveable
{
    [SerializeField]
    float experiencePoints = 0;

    public void GainExperience(float experience)
    {
        experiencePoints += experience;
    }

    public object CaptureState()
    {
        return experiencePoints;
    }

    public void RestoreState(object state)
    {
        this.experiencePoints = (float) state;
    }

    public float GetPoint()
    {
        return experiencePoints;
    }
}
