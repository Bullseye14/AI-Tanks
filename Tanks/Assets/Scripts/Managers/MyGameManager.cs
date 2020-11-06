using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private float endTimer;
    private bool restartingGame = false;
    private Vector3 redInitialPos, blueInitialPos;
    private bool gameFinished = false;

    public GameObject redTank;
    public GameObject blueTank;
    public TankHealth tankHealth1, tankHealth2;

    public float endDelay = 5f;
    public int redWins = 0;
    public int blueWins = 0;

    void Start()
    {
        redInitialPos = redTank.transform.position;
        blueInitialPos = blueTank.transform.position;

        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!redTank.activeSelf || !blueTank.activeSelf)
        {
            if (!gameFinished)
            {
                if (blueTank.activeSelf) blueWins += 1;
                else if (redTank.activeSelf) redWins += 1;

                Timer.instance.timerActive = false;
                gameFinished = true;
            }

            RestartGame();
        }
    }

    public void BeginGame()
    {
        restartingGame = false;
        gameFinished = false;
        endTimer = 0;

        Timer.instance.timeStart = 0;
        Timer.instance.timerActive = true;
    }

    public void RestartGame()
    {
        if(!restartingGame)
        {
            endTimer += Time.deltaTime;
            if (endTimer >= endDelay)
                restartingGame = true;
        }

        // Restarting Game
        else
        {
            tankHealth1.m_Dead = false; tankHealth2.m_Dead = false;
            blueTank.SetActive(true); redTank.SetActive(true);

            tankHealth1.m_Slider.value = tankHealth1.m_CurrentHealth = tankHealth1.m_StartingHealth;
            tankHealth2.m_Slider.value = tankHealth2.m_CurrentHealth = tankHealth2.m_StartingHealth;
            tankHealth1.m_FillImage.color = tankHealth2.m_FillImage.color = Color.green;

            redTank.transform.position = redInitialPos;
            blueTank.transform.position = blueInitialPos;

            BeginGame();
        }
    }
}
