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

    readonly private float rotationFuel = 0.3f;

    Rigidbody myRigidBody;
    AudioSource myAudioSource;

    int rotation;
    bool booster;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
        rotation = 0;
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
        if (Input.GetKey(KeyCode.W) || booster)
        {
            bool hasFuel = GameManager.Instance.TryUseFuel(Time.deltaTime);
            if (hasFuel)
            {
                StartThrusting();
            }
            else
            {
                StopThrusting();
            }
        }
        else { StopThrusting(); }
    }

    void StartThrusting()
    {
        // Start's the timer as soon as the player makes a move.
        if (GameManager.Instance.GetState() == GameManager.State.Ready)
        {
            GameManager.Instance.SetToPlaying();
        }
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
        if (Input.GetKey(KeyCode.A) || rotation == -1)
        {
            bool hasFuel = GameManager.Instance.TryUseFuel(Time.deltaTime * rotationFuel);
            if (hasFuel)
            {
                RotateLeft();
            }
            else
            {
                leftBoosterParticle.Stop();
            }
        }
        else if (Input.GetKey(KeyCode.D) || rotation == 1)
        {
            bool hasFuel = GameManager.Instance.TryUseFuel(Time.deltaTime * rotationFuel);
            if (hasFuel)
            {
                RotateRight();
            }
            else
            {
                rightBoosterParticle.Stop();
            }
        }
        else
        {
            leftBoosterParticle.Stop();
            rightBoosterParticle.Stop();
        }
    }

    public void RotateRight()
    {
        ApplyRotation(Vector3.back);
        PlayThrustAudio();
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    }

    public void RotateLeft()
    {
        ApplyRotation(Vector3.forward);
        PlayThrustAudio();

        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }

    public void ChangeRotation(int rotationDirection)
    {
        rotation = rotationDirection;
    }


    public void ChangeBooster(bool value)
    {
        booster = value;
    }
    private void ApplyRotation(Vector3 vector)
    {
        myRigidBody.freezeRotation = true;
        myRigidBody.transform.Rotate(vector * rotateThrust * Time.deltaTime);
        myRigidBody.freezeRotation = false;
    }
}
