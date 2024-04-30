using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileControlsUI : MonoBehaviour
{

    [SerializeField] private Image currentFuelBar;
    [SerializeField] private GameObject fuelBar;

    void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        GameManager.Instance.OnFuelChange += GameManager_OnFuelChange;

        if (GameManager.Instance.HasFuelLimit())
        {
            fuelBar.SetActive(true);
        }
        else
        {
            fuelBar.SetActive(false);
        }
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.GetState() == GameManager.State.Playing || GameManager.Instance.GetState() == GameManager.State.Ready)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void GameManager_OnFuelChange(object sender, System.EventArgs e)
    {

        currentFuelBar.fillAmount = GameManager.Instance.GetCurrentFuelPercentage();
    }
}
