using RPG.Saving;
using UnityEngine;

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
}
