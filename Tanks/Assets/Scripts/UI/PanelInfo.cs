using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelInfo : MonoBehaviour
{
    public GameObject Panel;
    private bool isActive;

    private void Start()
    {
        isActive = true;
    }
    public void OpenPanel()
    {
        if (isActive)
        {
            Time.timeScale = 0f;
            Panel.SetActive(isActive);
            isActive = !isActive;
        }
        else
        {
            Time.timeScale = 1f;
            Panel.SetActive(isActive);
            isActive = !isActive;
        }
    }
}
