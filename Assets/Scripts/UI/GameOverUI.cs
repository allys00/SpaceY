using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    void Awake()
    {
        retryButton.onClick.AddListener(Retry);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        gameObject.SetActive(false);
    }

    private void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    private void Retry()
    {
        GameManager.Instance.RetryCurrentLevel();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.GetState() == GameManager.State.GameOver)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }



}
