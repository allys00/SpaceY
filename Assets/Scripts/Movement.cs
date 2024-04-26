using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 300f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem forwardBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;

    Rigidbody myRigidBody;
    AudioSource myAudioSource;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void PlayThrustAudio()
    {
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(mainEngine);
        }
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W)) { StartThrusting(); }
        else { StopThrusting(); }
    }

    void StartThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        PlayThrustAudio();

        if (!forwardBoosterParticle.isPlaying)
        {
            forwardBoosterParticle.Play();
        }
    }

    void StopThrusting()
    {
        myAudioSource.Stop();
        forwardBoosterParticle.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            leftBoosterParticle.Stop();
            rightBoosterParticle.Stop();

        }
    }

    private void RotateRight()
    {
        ApplyRotation(Vector3.back);
        PlayThrustAudio();
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(Vector3.forward);
        PlayThrustAudio();

        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }

    private void ApplyRotation(Vector3 vector)
    {
        myRigidBody.freezeRotation = true;
        myRigidBody.transform.Rotate(vector * rotateThrust * Time.deltaTime);
        myRigidBody.freezeRotation = false;
    }
}
