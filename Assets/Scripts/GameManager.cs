using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public EventHandler OnStateChange;

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


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (state == State.Playing)
        {
            playingTimer += Time.deltaTime;
        }
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
}
