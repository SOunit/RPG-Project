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

        [SerializeField]
        float fadeOutTime = 1f;

        [SerializeField]
        float fadeInTime = 2f;

        [SerializeField]
        float fadeWaitTime = 0.5f;

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

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            wrapper.Load();

            // run in next scene
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer (otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

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
