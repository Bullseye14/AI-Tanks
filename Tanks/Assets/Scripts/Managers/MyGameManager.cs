using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private bool gamePlaying;

    void Start()
    {
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginGame()
    {
        gamePlaying = true;

        Timer.instance.BeginTime();
    }
}
