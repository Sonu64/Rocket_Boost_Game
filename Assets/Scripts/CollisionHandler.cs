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
                break;
            case "Ground":
                Debug.Log("Ground hit");
                ReloadLevel();
                break;
            default:
                Debug.Log("Nothing yet");
                break;
        }
    }

    private static void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
