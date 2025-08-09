using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] private float thrustStrength;
    [SerializeField] private float rotationStrength;

    private void OnEnable() {
        thrust.Enable();
        rotation.Enable();
    }

    private void Start() {
        // current object's rigid body
        rb = GetComponent<Rigidbody>();
        // current object's audio source component
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {

        Thrust();
        Rotation();
    }

    private void Rotation() {
        float rotationInput = rotation.ReadValue<float>();
        Debug.Log("Rotation value: " + rotationInput);
        if (rotationInput < 0) {
            ApplyRotation(rotationStrength);
        } // to the right 
        else if (rotationInput > 0) {
            ApplyRotation(-rotationStrength);
        } // to the left

    }

    private void ApplyRotation(float rotationStrength) {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationStrength);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void Thrust() {
        if (thrust.IsPressed()) {
            rb.AddRelativeForce(thrustStrength * Time.fixedDeltaTime * Vector3.up);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            } else {
                audioSource.Stop();
            }
        } else {
            audioSource.Stop();
        }
    }
}
