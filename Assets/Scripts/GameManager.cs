using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float initialFuel;
    [SerializeField] private bool hasFuelLimit;

    [SerializeField] private float threeStarTime;
    [SerializeField] private float twoStarTime;

    public static GameManager Instance { get; private set; }
    public EventHandler OnStateChange;
    public EventHandler OnFuelChange;

    public enum State
    {
        Ready,
        Playing,
        Paused,
        GameOver,
        Finished,
    }

    private State state = State.Ready;
    private float playingTimer = 0;
    private float currentFuel;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        currentFuel = initialFuel;
    }

    void Update()
    {
        if (state == State.Playing)
        {
            playingTimer += Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public float GetPlayingTime()
    {
        return playingTimer;
    }

    public void SetToPlaying()
    {
        state = State.Playing;
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public void SetToGameOver()
    {
        state = State.GameOver;
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public void SetToFinished()
    {
        SaveBestTime();
        state = State.Finished;
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public State GetState()
    {
        return state;
    }

    public void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public bool TryUseFuel(float fuel)
    {
        if (hasFuelLimit)
        {
            currentFuel -= fuel;
            if (currentFuel > 0)
            {
                OnFuelChange?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }
        return true;
    }

    public float GetCurrentFuelPercentage()
    {
        if (hasFuelLimit && currentFuel >= 0)
            return currentFuel / initialFuel;
        else
            return 0;
    }

    public bool HasFuelLimit()
    {
        return hasFuelLimit;
    }

    public float GetBestTime()
    {
        string key = SceneManager.GetActiveScene().name + "_bestTime";
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        return 0;
    }

    public float SaveBestTime()
    {
        string key = SceneManager.GetActiveScene().name + "_bestTime";
        float bestTime = GetBestTime();
        if (bestTime == 0 || playingTimer < bestTime)
        {
            PlayerPrefs.SetFloat(key, playingTimer);
            return playingTimer;
        }
        return bestTime;
    }

    public bool HasThreeStars()
    {
        return playingTimer <= threeStarTime;
    }

    public bool HasTwoStars()
    {
        return playingTimer <= twoStarTime;
    }
}
