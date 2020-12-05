using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DropObjects : MonoBehaviour
{
    public GameObject bullet;
    public GameObject health;

    public TankHealth TankHealth;

    private Vector3 dropPosition;
    private float delay = 7f;
    private float delayTimer;

    private bool appear = false;

    // Update is called once per frame
    void Update()
    {
        if(!appear)
        {
            delayTimer += Time.deltaTime;

            if(delayTimer > delay)
            {
                appear = true;
            }
        }

        else
        {
            InstantiateBullet(Random.Range(0, 100));
        }
    }

    private void InstantiateBullet(float num)
    {
        dropPosition.y = 0.5f;
        dropPosition.x = Random.Range(-29, 27);
        dropPosition.z = Random.Range(-25, 22);

        //dropPosition.y = 0.5f;
        //dropPosition.x = RandomNavmeshLocation(40).x;
        //dropPosition.z = RandomNavmeshLocation(40).z;

        if (num <= 50)
        {
            bullet.transform.position = dropPosition;
            bullet.transform.rotation = Quaternion.identity;

            GameObject bulletInstance = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
        }

        else if (num > 50)
        {
            health.transform.position = dropPosition;
            health.transform.rotation = Quaternion.identity;

            GameObject healthInstance = Instantiate(health, health.transform.position, health.transform.rotation) as GameObject;
        }

        delayTimer = 0f;
        appear = false;
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
