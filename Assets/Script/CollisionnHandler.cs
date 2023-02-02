using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionnHandler : MonoBehaviour
{
    [SerializeField] float leveLoadDelay = 2f;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Death;

    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem DeathParticle;

    AudioSource audioSource;


    bool isTransitioning = false;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendy":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            case "Fuel":
                Debug.Log("you picked up fuel");
                break;

            default:
                StartCrashSequence();
                Debug.Log("you GG!!!");
                break;
        }

    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        SuccessParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", leveLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        DeathParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", leveLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        //SceneManager.LoadScene(0);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
