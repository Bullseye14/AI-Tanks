using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;

    
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;

    // Patrolling Movement Variables
    private NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;


    [SerializeField]
    bool _patrolWaiting; // if agents is waiting or not in a waypoint

    [SerializeField]
    float _totalWaitTime = 3f; // time that agent waits in eaach waypoint

    [SerializeField]
    float _switchProbability = 0.2f; // probability to change direction

    [SerializeField]
    List<WayPoints> _patrolPoints; // List of all waypoints


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        //m_MovementAxisName = "Vertical" + m_PlayerNumber;
        //m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        if(this.m_PlayerNumber == 1)
        {
            //code for tank1 -- Start()
            if (_navMeshAgent == null)
                Debug.LogError("Nav Mesh Agent is not attached to " + gameObject.name);
            else
            {
                if (_patrolPoints != null && _patrolPoints.Count >= 2)
                {
                    _currentPatrolIndex = 0;
                    SetDestination();
                }
                else
                {
                    Debug.Log("Not enough waypoints");
                }
            }
        }
        else if(this.m_PlayerNumber == 2)
        {
            // code for tank2 -- Start()
        }

        m_OriginalPitch = m_MovementAudio.pitch;
    }
    

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        //m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        //m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
        
        
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.

        if(Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if(m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        //Turn();
    }


    private void Move()
    {
        if(this.m_PlayerNumber == 1)
        {
            //code for moving tank1
            if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
            {
                _travelling = false;

                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }

            if (_waiting)
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;

                    ChangePatrolPoint();
                    SetDestination();
                }
            }
        }
        else if(this.m_PlayerNumber == 2)
        {
            //code for moving tank2
        }


        // Adjust the position of the tank based on the player's input.
        //Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    //private void Turn()
    //{
    //    // Adjust the rotation of the tank based on the player's input.

    //    float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

    //    Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

    //    m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    //}

    private void SetDestination()
    {
        if(_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
            _patrolForward = !_patrolForward;

        if (_patrolForward)
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        else
        {
            if (--_currentPatrolIndex < 0)
                _currentPatrolIndex = _patrolPoints.Count - 1;
        }
    }
}