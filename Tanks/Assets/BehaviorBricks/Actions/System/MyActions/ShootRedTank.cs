using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

namespace BBUnity.Actions
{
    [Action("MyActions/ShootRedTank")]
    [Help("Clone a 'bullet' and shoots it throught the Forward axis with the " +
           "specified velocity.")]
    public class ShootRedTank : GOAction
    {
        private float launchForce_long = 7f;
        private float launchForce_short = 7f;
        public GameObject game;
        public int bullets;
        private bool canFire = true;
        private float delayTimer;
        [InParam("delay")]
        public float delay;

        //public AudioSource m_ShootingAudio;
        //public AudioClip m_ChargingClip;
        //public AudioClip m_FireClip;

        //CanShootBlue condition;
        private Transform shootPoint;

        [InParam("bullet")]
        public GameObject bullet;

        //[InParam("velocity", DefaultValue = 30f)]
        //public float velocity;

        public override void OnStart()
        {
            game = GameObject.Find("GameManager");

            bullets = game.GetComponent<MyGameManager>().redBullets;

            if (shootPoint == null)
            {
                Transform fireTransf;

                fireTransf = gameObject.transform.Find("TankRenderers");
                fireTransf = fireTransf.transform.Find("TankTurret");
                shootPoint = fireTransf.transform.Find("FireTransform");
            }

            delayTimer = 0f;
            canFire = true;

            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (!canFire)
            {
                delayTimer += Time.deltaTime;

                if (delayTimer >= delay)
                    canFire = true;
            }

            if (canFire && bullets > 0)
            {
                Fire();
                canFire = false;
            }

            return TaskStatus.RUNNING;
        }

        public void Fire()
        {
            GameObject tank2 = GameObject.Find("Tank1");

            Vector3 shootPos;
            Quaternion shootRot;

            if (shootPoint == null)
            {
                shootPos = Vector3.zero;
                shootRot = Quaternion.identity;
            }
            else
            {
                shootPos = shootPoint.position;
                shootRot = shootPoint.rotation;
            }

            GameObject newBullet = GameObject.Instantiate(bullet, shootPos, shootRot * bullet.transform.rotation) as GameObject;

            if (newBullet.GetComponent<Rigidbody>() == null)
                newBullet.AddComponent<Rigidbody>();

            if (CloseEnough(shootPos, tank2))
                newBullet.GetComponent<Rigidbody>().velocity = launchForce_short * shootPoint.forward;

            else
                newBullet.GetComponent<Rigidbody>().velocity = launchForce_long * shootPoint.forward;


            delayTimer = 0;
            bullets--;
            game.GetComponent<MyGameManager>().redBullets--;

            //m_ShootingAudio.clip = m_FireClip;
            //m_ShootingAudio.Play();

        }
        public bool CloseEnough(Vector3 origin, GameObject enemy)
        {
            Vector3 enemyPos = enemy.transform.position;

            Vector3 distance;
            distance.x = Mathf.Abs(enemyPos.x - origin.x);
            distance.y = 0;
            distance.z = Mathf.Abs(enemyPos.z - origin.z);

            if (distance.x < 15 && distance.z < 15) return true;
            else return false;
        }
    }
}

