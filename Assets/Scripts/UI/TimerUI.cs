using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;


    void Update()
    {
        timerText.text = GameManager.Instance.GetPlayingTime().ToString("F3");
    }
}
