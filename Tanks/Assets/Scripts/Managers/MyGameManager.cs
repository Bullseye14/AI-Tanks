﻿using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{
    private float endTimer;
    private bool restartingGame = false;
    private Vector3 redInitialPos, blueInitialPos;
    private bool gameFinished = false;

    public GameObject redTank;
    public GameObject blueTank;
    public TankHealth tankHealth1, tankHealth2;

    public GameObject blueBulletsGO;
    public GameObject redBulletsGO;

    public float endDelay = 5f;
    public int redWins = 0;
    public int blueWins = 0;
    public int redKills = 0;
    public int blueKills = 0;
    public int redDeaths = 0;
    public int blueDeaths = 0;
    public int redBullets = 3;
    public int blueBullets = 3;
    public Text redK_text;
    public Text blueK_text;
    public Text redD_text;
    public Text blueD_text;
    public Text BWins_text;
    public Text RWins_text;

    //public TanksMoveManager TanksMove;
    public FinalPanel finalPanel;

    void Start()
    {
        redInitialPos = redTank.transform.position;
        blueInitialPos = blueTank.transform.position;

        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBullets(blueBulletsGO, redBulletsGO);

        if (!redTank.activeSelf || !blueTank.activeSelf)
        {
            //AppearSpriteRed.SpriteRedInstance.visible1 = false;
            //AppearSpriteRed.SpriteRedInstance.visible2 = false;
            //AppearSpriteBlue.SpriteBlueInstance.visible1 = false;
            //AppearSpriteBlue.SpriteBlueInstance.visible2 = false;

            if (!gameFinished)
            {
                if (blueTank.activeSelf)
                {
                    blueWins += 1;
                    blueKills += 1;
                    redDeaths += 1;
                    //AppearSpriteBlue.SpriteBlueInstance.visible3 = true;
                }
                else if (redTank.activeSelf)
                {
                    redWins += 1;
                    redKills += 1;
                    blueDeaths += 1;
                    //AppearSpriteRed.SpriteRedInstance.visible3 = true;
                }

                Timer.instance.timerActive = false;
                gameFinished = true;
            }

            // Wins UI
            BWins_text.text = blueWins.ToString();
            RWins_text.text = redWins.ToString();

            // KD UI
            redK_text.text = redKills.ToString();
            blueK_text.text = blueKills.ToString();
            redD_text.text = redDeaths.ToString();
            blueD_text.text = blueDeaths.ToString();

            RestartGame();
        }
    }

    public void BeginGame()
    {
        restartingGame = false;
        gameFinished = false;
        endTimer = 0;

        blueBullets = 3;
        redBullets = 3;

        //AppearSpriteBlue.SpriteBlueInstance.visible3 = false;
        //AppearSpriteRed.SpriteRedInstance.visible3 = false;

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
            if (blueWins == 3 || redWins == 3)
                GameIsOver();

            else
            {
                tankHealth1.m_Dead = false; tankHealth2.m_Dead = false;
                blueTank.SetActive(true); redTank.SetActive(true);
                //TanksMove.BPatrol = true; TanksMove.RWander = true;

                tankHealth1.m_Slider.value = tankHealth1.m_CurrentHealth = tankHealth1.m_StartingHealth;
                tankHealth2.m_Slider.value = tankHealth2.m_CurrentHealth = tankHealth2.m_StartingHealth;
                tankHealth1.m_FillImage.color = tankHealth2.m_FillImage.color = Color.green;

                redTank.transform.position = redInitialPos;
                blueTank.transform.position = blueInitialPos;

                BeginGame();
            }            
        }
    }

    public void GameIsOver()
    {
        if (blueWins == 3)
            finalPanel.OpenFinalBluePanel();

        if (redWins == 3)
            finalPanel.OpenFinalRedPanel();
    }

    private void UpdateBullets(GameObject blue, GameObject red)
    {
        GameObject[] blueBullets_ = new GameObject[blue.transform.childCount];
        GameObject[] redBullets_ = new GameObject[red.transform.childCount];

        for (int i = 0; i < blue.transform.childCount; i++)
            blueBullets_[i] = blue.transform.GetChild(i).gameObject;

        for (int i = 0; i < red.transform.childCount; i++)
            redBullets_[i] = red.transform.GetChild(i).gameObject;

        // Blue Bullets
        if(blueBullets >=3) // All 3 sprites
        {
            blueBullets_[0].SetActive(true);
            blueBullets_[1].SetActive(true);
            blueBullets_[2].SetActive(true);
        }
        else if(blueBullets ==2) // 0 & 1
        {
            blueBullets_[0].SetActive(true);
            blueBullets_[1].SetActive(true);
            blueBullets_[2].SetActive(false);
        }
        else if(blueBullets == 1) // 0
        {
            blueBullets_[0].SetActive(true);
            blueBullets_[1].SetActive(false);
            blueBullets_[2].SetActive(false);

        }
        else if(blueBullets == 0) // none
        {
            blueBullets_[0].SetActive(false);
            blueBullets_[1].SetActive(false);
            blueBullets_[2].SetActive(false);
        }

        // Red Bullets
        if (redBullets >= 3) // All 3 sprites
        {
            redBullets_[0].SetActive(true);
            redBullets_[1].SetActive(true);
            redBullets_[2].SetActive(true);
        }
        else if (redBullets == 2) // 0 & 1
        {
            redBullets_[0].SetActive(true);
            redBullets_[1].SetActive(true);
            redBullets_[2].SetActive(false);
        }
        else if (redBullets == 1) // 0
        {
            redBullets_[0].SetActive(true);
            redBullets_[1].SetActive(false);
            redBullets_[2].SetActive(false);

        }
        else if (redBullets == 0) // none
        {
            redBullets_[0].SetActive(false);
            redBullets_[1].SetActive(false);
            redBullets_[2].SetActive(false);
        }
    }
}
