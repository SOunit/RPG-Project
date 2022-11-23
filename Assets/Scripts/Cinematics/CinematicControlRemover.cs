using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        // event + observer pattern in C#
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    private void OnDisable()
    {
        GetComponent<PlayableDirector>().played -= DisableControl;
        GetComponent<PlayableDirector>().stopped -= EnableControl;
    }

    void DisableControl(PlayableDirector pd)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }

    void EnableControl(PlayableDirector pd)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }
}
