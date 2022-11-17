using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            Sandbox_End_Sandbox2_Start,
            Sandbox_Start_Sandbox2_End
        }

        [SerializeField]
        int sceneToLoad = -1;

        [SerializeField]
        Transform spawnPoint;

        [SerializeField]
        DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }

            DontDestroyOnLoad (gameObject);
            SceneManager.LoadScene (sceneToLoad);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            Debug.Log (otherPortal);

            UpdatePlayer (otherPortal);

            // run in next scene
            print("Scene Loaded");
            Destroy (gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");

            player
                .GetComponent<NavMeshAgent>()
                .Warp(otherPortal.spawnPoint.position);

            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (var portal in FindObjectsOfType<Portal>())
            {
                if (portal == this)
                {
                    continue;
                }
                if (portal.destination != destination)
                {
                    continue;
                }

                return portal;
            }

            return null;
        }
    }
}
