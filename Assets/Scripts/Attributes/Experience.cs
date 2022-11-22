using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField]
    float experiencePoints = 0;

    public void GainExperience(float experience)
    {
        experiencePoints += experience;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
