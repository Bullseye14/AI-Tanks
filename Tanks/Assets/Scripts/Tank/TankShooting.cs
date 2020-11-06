using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber;
    public GameObject enemy;
    public GameObject direction;
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;
    public float delay1 = 3f;
    public float delay2 = 2f;
    public float minDistance = 10;
    public float midDistance = 15;
    public float maxDistance = 20;

    private float m_CurrentLaunchForce;
    private bool canFire = true;
    private float delayTimer1;
    private float delayTimer2;
    private NavMeshHit hit;
    private bool blocked = false;

    private void Update()
    {
        if (this.m_PlayerNumber == 1)
            if (!canFire)
            {
                delayTimer1 += Time.deltaTime;
                if (delayTimer1 >= delay1)
                    canFire = true;
            }
        
        if (this.m_PlayerNumber == 2)
            if (!canFire)
            {
                delayTimer2 += Time.deltaTime;
                if (delayTimer2 >= delay2)
                    canFire = true;
            }

        Vector3 origin = m_FireTransform.transform.position;
        Vector3 destination = enemy.transform.position;

        Vector3 tanksDistance = AbsoluteValue(destination - origin);

        blocked = NavMesh.Raycast(origin, destination, out hit, NavMesh.AllAreas);
        Debug.DrawLine(transform.position, enemy.transform.position, blocked ? Color.red : Color.green);

        if (blocked)
            Debug.DrawRay(hit.position, Vector3.up, Color.red);

        else
            if (canFire)
            Fire(EnemyClose(tanksDistance));
    }

    public void Fire(int charge)
    {
        if (charge != -1)
        {
            if (charge == 0)
                m_CurrentLaunchForce = 15f;

            else if (charge == 1)
                m_CurrentLaunchForce = 17f;

            else if (charge == 2)
                m_CurrentLaunchForce = 20f;

            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.transform.position, m_FireTransform.transform.rotation) as Rigidbody;
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            canFire = false;

            if (this.m_PlayerNumber == 1) delayTimer1 = 0;
            if (this.m_PlayerNumber == 2) delayTimer2 = 0;

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            m_CurrentLaunchForce = 15f;
        }
    }

    private int EnemyClose(Vector3 distance)
    {
        int ret;

        if (Mathf.Sqrt((distance.x * distance.x) + (distance.z * distance.z)) < minDistance) ret = 0;
        else if (Mathf.Sqrt((distance.x * distance.x) + (distance.z * distance.z)) < midDistance) ret = 1;
        else if (Mathf.Sqrt((distance.x * distance.x) + (distance.z * distance.z)) < maxDistance) ret = 2;
        else ret = -1;

        //if (distance.x < minDistance && distance.z < minDistance) ret = 0;
        //else if (distance.x < midDistance && distance.z < midDistance) ret = 1;
        //else if (distance.x < maxDistance && distance.z < maxDistance) ret = 2;
        //else ret = -1;

        if ((ret == 2) && this.m_PlayerNumber == 2) ret = -1;

        return ret;
    }

    private Vector3 AbsoluteValue(Vector3 vector)
    {
        if (vector.x < 0) vector.x = -vector.x;

        if (vector.y < 0) vector.y = -vector.y;

        if (vector.z < 0) vector.z = -vector.z;

        return vector;
    }
}