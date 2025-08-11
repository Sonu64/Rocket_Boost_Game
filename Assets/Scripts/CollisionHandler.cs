using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        string objectTag = collision.gameObject.tag;
        switch(objectTag) {
            case "Finish":
                Debug.Log("Finished");
                StartSuccessSequence();
                break;
            case "Ground":
                Debug.Log("Ground hit");
                StartCrashSequence();
                break;
            case "Obstacle":
                Debug.Log("Obstacle Hit");
                StartHitSequence();
                break;
            default:
                Debug.Log("Nothing yet");
                break;
        }
    }

    //Method called when Obstacles are Hit, Todo add PFX diff. from StartCrashSequence()
    private void StartHitSequence() {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSFX);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    // Method called when landed succcessfully
    void StartSuccessSequence() {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSFX);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    // Method called when Rocket falls on ground, currently same as StartHitSequence() Todo add PFX
    void StartCrashSequence() {
        // disable movement script once crashed
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSFX);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() { 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(0);
            currentSceneIndex = 0;
        } else { 
            SceneManager.LoadScene(currentSceneIndex + 1);
            currentSceneIndex++;
        }
            
    }

}
