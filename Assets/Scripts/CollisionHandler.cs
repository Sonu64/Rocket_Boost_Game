using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;

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
            default:
                Debug.Log("Nothing yet");
                break;
        }
    }

    void StartSuccessSequence() {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence() {
        // disable movement script once crashed
        GetComponent<Movement>().enabled = false;
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
