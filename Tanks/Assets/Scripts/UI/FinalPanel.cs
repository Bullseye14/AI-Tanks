using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalPanel : MonoBehaviour
{
    public GameObject redPanel;
    public GameObject bluePanel;
    public GameObject BlueKD;
    public GameObject RedKD;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenFinalBluePanel()
    {
        bluePanel.SetActive(true);
        BlueKD.SetActive(true);
    }

    public void OpenFinalRedPanel()
    {
        redPanel.SetActive(true);
        RedKD.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
