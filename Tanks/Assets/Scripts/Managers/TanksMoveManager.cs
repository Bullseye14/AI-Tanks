using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TanksMoveManager : MonoBehaviour
{
    public Transform RedTank;
    public Transform BlueTank;
    public Vector3 Blue_distanceBetween;
    public Vector3 Red_distanceBetween;
    public float detectDistance = 20f;
    public bool BPatrol, BChase;
    public bool RWander, RFlee;
    //public float TimeDuration;
    //private float stopTime;

    public Text distance;
    

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
            Debug.Log("Not detecting - Patrol & Wander");

            BPatrol = true;
            RWander = true;

            BChase = false;
            RFlee = false;
        }

        if (Blue_distanceBetween.magnitude < detectDistance || Red_distanceBetween.magnitude < detectDistance) // Detected
        {
            //Debug.Log(TimeDuration);

            BChase = true;
            RFlee = true;

            BPatrol = false;
            RWander = false;

            //BPatrol = false;
            //RWander = false;

            //if (Time.time > stopTime) // if the red tank hadn't had time to escape, they will fight
            //{
            //    stopTime = Time.time + TimeDuration;
            //    BChase = true;
            //    RFlee = true;
            //}
            //else
            //{
            //    BChase = false;
            //    RFlee = false;
            //}

        }

        distance.text = Blue_distanceBetween.magnitude.ToString();
    }
}
