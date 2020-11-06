using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearSpriteRed : MonoBehaviour
{
    public Image spriteImg;

    void Update()
    {
        Vector3 spritePos = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(3.0f, 2.0f, 0.0f));
        spriteImg.transform.position = spritePos;
    }
}
