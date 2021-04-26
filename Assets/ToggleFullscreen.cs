using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFullscreen : MonoBehaviour
{
    public void Toggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
