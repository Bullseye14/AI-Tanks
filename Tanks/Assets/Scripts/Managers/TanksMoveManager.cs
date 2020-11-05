using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksMoveManager : MonoBehaviour
{
    public Transform RedTank;
    public Transform BlueTank;
    public Vector3 Blue_distanceBetween;
    public Vector3 Red_distanceBetween;
    public float detectDistance = 20f;
    public bool BPatrol, BChase;
    public bool RWander, RFlee;
    public float TimeDuration = 3f;
    private float stopTime;
    

    private void Awake()
    {
        BlueTank = GameObject.Find("Tank1").transform;
        RedTank = GameObject.Find("Tank2").transform;
    }
    void Start()
    {
        
    }


    void Update()
    {
        Blue_distanceBetween = RedTank.position - BlueTank.position;
        Red_distanceBetween = BlueTank.position - RedTank.position;

        if (Blue_distanceBetween.magnitude >= detectDistance || Red_distanceBetween.magnitude >= detectDistance) // Not detected
        {
            BPatrol = true;
            RWander = true;

            BChase = false;
            RFlee = false;
        }

        else if (Blue_distanceBetween.magnitude < detectDistance || Red_distanceBetween.magnitude < detectDistance) // Detected
        {
            if (Time.time > stopTime)
            {
                stopTime = Time.time + TimeDuration;
                BChase = true;
                RFlee = true;
            }
            else
            {
                BChase = false;
                RFlee = false;
            }

            BPatrol = false;
            RWander = false;
        }

        Debug.Log(Blue_distanceBetween.magnitude);
    }
}
