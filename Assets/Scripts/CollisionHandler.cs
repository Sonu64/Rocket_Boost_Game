using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnCollisionEnter(Collision collision) {
        string objectTag = collision.gameObject.tag;
        switch(objectTag) {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel taken");
                break;
            case "Finish":
                Debug.Log("Finished");
                LoadNextLevel();
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

    void StartCrashSequence() {
        // disable movement script once crashed
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 2f);
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
