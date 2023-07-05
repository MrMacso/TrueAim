using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject btConnectUI;
    public GameObject ingameUI;
    private JoystickHandler jHandler;

    private void Start()
    {
        jHandler = this.GetComponent<JoystickHandler>();

        btConnectUI.SetActive(true);
        ingameUI.SetActive(false);
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if(jHandler.GetDevice().IsConnected)
        {
            btConnectUI.SetActive(false);
            ingameUI.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    public void SetTimeScale(float num)
    {
        Time.timeScale = num;
    }
}
