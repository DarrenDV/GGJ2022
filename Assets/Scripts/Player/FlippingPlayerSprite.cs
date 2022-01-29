using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingPlayerSprite : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        FlipSprite();
    }

    void FlipSprite()
    {
        float playerX = gameObject.transform.position.x;
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        if (playerX <= mouseX)
        {
            //gameObject.transform.localScale = new Vector3(1, 1, 1);
            spriteR.flipY = false;
        } 
        else if (playerX > mouseX)
        {
            //gameObject.transform.localScale = new Vector3(1, -1, 1);
            spriteR.flipY = true;
        }
    }
}
