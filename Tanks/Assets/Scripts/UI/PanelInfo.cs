using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelInfo : MonoBehaviour
{
    public GameObject Panel;
    private bool isActive;

    public void OpenPanel()
    {
        isActive = !isActive;
        Panel.gameObject.SetActive(isActive);
    }

}
