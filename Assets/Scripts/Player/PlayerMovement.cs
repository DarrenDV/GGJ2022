using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float diagonalSlowDown = 0.7f;

    [SerializeField] private Rigidbody2D rb;

    private Vector2 movement;

    private bool canRotate = true;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Player")
            {
                canRotate = false;
            }
            else
            {
                canRotate = true;
            }
        }
        else
        {
            canRotate = true;
        }
    }

    private void FixedUpdate()
    {
        if (canRotate)
        {
            //Rotate player towards mouse
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }

        if (movement.x != 0 && movement.y != 0)
        {
            movement *= diagonalSlowDown;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
