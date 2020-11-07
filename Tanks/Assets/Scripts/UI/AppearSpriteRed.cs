using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearSpriteRed : MonoBehaviour
{
    public Image sprite1;
    public Image sprite2;
    public Image spriteWin;
    public static AppearSpriteRed SpriteRedInstance;
    public bool visible1;
    public bool visible2;
    public bool visible3;

    private Vector3 initialPos;
    private float appearTime = 4f;
    private float appearing1;
    private float appearing2;

    private void Awake()
    {
        SpriteRedInstance = this;
    }

    private void Start()
    {
        visible1 = true;
        visible2 = false;
        visible3 = false;
        initialPos.x = initialPos.y = initialPos.z = -1000;
    }

    void Update()
    {
        if (visible1)
        {
            Vector3 spritePos = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(3.0f, 0.0f, 0.0f));
            sprite1.transform.position = spritePos;

            appearing1 += Time.deltaTime;
            if (appearing1 >= appearTime)
                visible1 = false;
        }
        else
        {
            appearing1 = 0f;
            sprite1.transform.position = initialPos;
        }

        if (visible2)
        {
            Vector3 spritePos = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(3.0f, 0.0f, 0.0f));
            sprite2.transform.position = spritePos;

            appearing2 += Time.deltaTime;
            if (appearing2 >= appearTime)
                visible2 = false;
        }
        else
        {
            appearing2 = 0f;
            sprite2.transform.position = initialPos;
        }

        if (visible3)
        {
            Vector3 spritePos = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(3.0f, 0.0f, 0.0f));
            spriteWin.transform.position = spritePos;
        }
        else spriteWin.transform.position = initialPos;
    }
}
