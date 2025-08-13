using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;


    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision collision) {
        string objectTag = collision.gameObject.tag;

        if (!isControllable || !isCollidable) 
            return;

        else
            switch (objectTag) {
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
        // disable movement script once crashed
        isControllable = false;
        // stop the engine thruster audio before starting any new audio
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSFX);
        // Playing Particle Effect for Success
        crashParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    // Method called when landed succcessfully
    void StartSuccessSequence() {
        isControllable = false;
        // stop the engine thruster audio before starting any new audio
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSFX);
        // Playing Particle Effect for Success
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    // Method called when Rocket falls on ground, currently same as StartHitSequence() Todo add PFX
    void StartCrashSequence() {
        // disable movement script once crashed
        isControllable = false;
        // stop the engine thruster audio before starting any new audio
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSFX);
        // Playing Particle Effect for Success
        crashParticles.Play();
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

    void RespondToDebugKeys() {
        if (Keyboard.current.lKey.wasPressedThisFrame) {
            LoadNextLevel();
        }else if (Keyboard.current.cKey.wasPressedThisFrame) {
            isCollidable = !isCollidable;
        }
    }
}
