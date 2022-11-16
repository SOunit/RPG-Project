using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    private void Start()
    {
        // event + observer pattern in C#
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    void DisableControl(PlayableDirector pd)
    {
        Debug.Log("DisableControl");
    }

    void EnableControl(PlayableDirector pd)
    {
        Debug.Log("EnableControl");
    }
}
