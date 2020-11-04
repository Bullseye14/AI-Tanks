using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;
    private bool canFire = true;
    public float delayTime = 2.5f;
    private float delayTimer;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    private void Update()
    {
        //if(!canFire)
        //{
        //    delayTimer += Time.deltaTime;
        //    if (delayTimer > delayTime)
        //        canFire = true;
        //}

        //Vector3 distance = enemy.transform.position - transform.position;

        //distance = AbsoluteValue(distance);

        //if (distance.x < 25f && distance.z < 25f && canFire)
        //    Fire();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        // if (EnemyClose(distance)) Fire(Quaternion.Euler(0, enemyAngle.y, 0));

        //// Track the current state of the fire button and make decisions based on the current launch force.
        //m_AimSlider.value = m_MinLaunchForce;

        //// Max Charge, not fired
        //if(m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        //{
        //    m_CurrentLaunchForce = m_MaxLaunchForce;
        //    Fire();
        //}
        //// Pressed for the first time
        //else if(Input.GetButtonDown(m_FireButton))
        //{
        //    m_Fired = false;
        //    m_CurrentLaunchForce = m_MinLaunchForce;

        //    m_ShootingAudio.clip = m_ChargingClip;
        //    m_ShootingAudio.Play();
        //}
        //// Holding the fire
        //else if(Input.GetButton(m_FireButton) && !m_Fired)
        //{
        //    m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

        //    m_AimSlider.value = m_CurrentLaunchForce;
        //}
        //// Release button
        //else if(Input.GetButtonUp(m_FireButton) && !m_Fired)
        //{
        //    Fire();
        //}
    }


    private void Fire()
    {
        canFire = false;

        // Instantiate and launch the shell.
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.transform.position, m_FireTransform.transform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        m_CurrentLaunchForce = m_MinLaunchForce;
    }

    private bool EnemyClose(Vector3 distance)
    {
        return false;
    }
    private float CalculateAngle(Vector3 objectivePos)
    {
        float distanceX = objectivePos.x - transform.position.x;
        float distanceZ = objectivePos.z - transform.position.z;

        float angleDesired = Mathf.Atan2(distanceX, distanceZ) * Mathf.Rad2Deg;

        return angleDesired;
    }

    private void FireNew(Vector3 enemyPos)
    {

    }

    private Vector3 AbsoluteValue(Vector3 vector)
    {
        if (vector.x < 0) vector.x = -vector.x;

        if (vector.y < 0) vector.y = -vector.y;

        if (vector.z < 0) vector.z = -vector.z;

        return vector;
    }
}