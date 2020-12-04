using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjects : MonoBehaviour
{
    public GameObject bullet;
    public GameObject health;

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
            InstantiateBullet(Random.Range(0, 2));
        }
    }

    private void InstantiateBullet(float num)
    {
        dropPosition.y = 1f;
        dropPosition.x = Random.Range(-29, 27);
        dropPosition.z = Random.Range(-25, 22);

        if (num <= 1)
        {
            bullet.transform.position = dropPosition;
            bullet.transform.rotation = Quaternion.identity;

            GameObject bulletInstance = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
        }

        else if (num > 1)
        {
            health.transform.position = dropPosition;
            health.transform.rotation = Quaternion.identity;

            GameObject healthInstance = Instantiate(health, health.transform.position, health.transform.rotation) as GameObject;
        }

        delayTimer = 0f;
        appear = false;
    }
}
