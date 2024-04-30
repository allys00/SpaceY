using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishedUI : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI bestTimerText;
    [SerializeField] private Image secondStarActive;
    [SerializeField] private Image thirdStarActive;


    void Awake()
    {
        nextButton.onClick.AddListener(Next);
        retryButton.onClick.AddListener(Retry);
    }

    void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
    }

    private void Next()
    {
        GameManager.Instance.LoadNextLevel();
    }

    private void Retry()
    {
        GameManager.Instance.RetryCurrentLevel();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.GetState() == GameManager.State.Finished)
        {
            timerText.text = GameManager.Instance.GetPlayingTime().ToString("F3");
            bestTimerText.text = GameManager.Instance.GetBestTime().ToString("F3");
            if (GameManager.Instance.HasTwoStars())
            {
                secondStarActive.gameObject.SetActive(true);
                if (GameManager.Instance.HasThreeStars())
                {
                    thirdStarActive.gameObject.SetActive(true);
                }
                else
                {
                    thirdStarActive.gameObject.SetActive(false);
                }
            }
            else
            {
                secondStarActive.gameObject.SetActive(false);
                thirdStarActive.gameObject.SetActive(false);
            }
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
