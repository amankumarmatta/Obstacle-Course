using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

        [SerializeField] ParticleSystem successParticles;
        [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //Toggle Collision
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled){return;}

        switch(other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is the starting point");
                break;
            
            case "Finish":
                StartSuccessSequence();
                break;
            
            case "Friendly":
                Debug.Log("This is a friendly object");
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Mover>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }

    void StartCrashSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crash);
            crashParticles.Play();
            GetComponent<Mover>().enabled = false;
            Invoke("ReloadLevel",levelLoadDelay);
        }

        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }
}
