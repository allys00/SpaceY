using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;


    AudioSource myAudioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Instance.LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(crashAudio, .5f);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        GameManager.Instance.SetToGameOver();
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        successParticles.Play();
        myAudioSource.PlayOneShot(successAudio);
        GetComponent<Movement>().enabled = false;
        GameManager.Instance.SetToFinished();
    }
}
