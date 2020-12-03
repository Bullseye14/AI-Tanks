using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

namespace BBUnity.Actions
{
    [Action("MyActions/ShootBlueTank")]
    [Help("Clone a 'bullet' and shoots it throught the Forward axis with the " +
           "specified velocity.")]
    public class ShootBlueTank : GOAction
    {
        private float m_CurrentLaunchForce;
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
            if (shootPoint == null)
            {
                Transform fireTransf;

                fireTransf = gameObject.transform.Find("TankRenderers");
                fireTransf = fireTransf.transform.Find("TankTurret");
                shootPoint = fireTransf.transform.Find("FireTransform");
            }

            delayTimer = 0f;

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

            if (canFire)
            {
                Fire();
            }

            return TaskStatus.COMPLETED;
        }

        public void Fire()
        {
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

            m_CurrentLaunchForce = 20f;

            GameObject newBullet = GameObject.Instantiate(bullet, shootPos, shootRot * bullet.transform.rotation) as GameObject;

            if (newBullet.GetComponent<Rigidbody>() == null)
                newBullet.AddComponent<Rigidbody>();

            newBullet.GetComponent<Rigidbody>().velocity = m_CurrentLaunchForce * shootPoint.forward;

            delayTimer = 0;

            //m_ShootingAudio.clip = m_FireClip;
            //m_ShootingAudio.Play();

        }
    } 
}

