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
        [SerializeField]
        int sceneToLoad = -1;

        [SerializeField]
        Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad (gameObject);
            SceneManager.LoadScene (sceneToLoad);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
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

                return portal;
            }

            return null;
        }
    }
}
