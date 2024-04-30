using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedUI : MonoBehaviour
{
    [SerializeField] private Button nextButton;


    void Awake()
    {
        nextButton.onClick.AddListener(Next);
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

    private void Next()
    {
        GameManager.Instance.LoadNextLevel();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.GetState() == GameManager.State.Finished)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
