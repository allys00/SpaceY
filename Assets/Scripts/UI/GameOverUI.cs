using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;

    void Awake()
    {
        retryButton.onClick.AddListener(Retry);
    }

    void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnStateChange -= GameManager_OnStateChange;
    }

    private void Retry()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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
