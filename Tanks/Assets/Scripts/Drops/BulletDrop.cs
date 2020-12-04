using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour
{
    private float lifeTime = 12f;
    private float life;

    public GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        life = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < lifeTime)
        {
            life += Time.deltaTime;
        }
        else
        {
            GameObject.Destroy(self);
        }
    }
}