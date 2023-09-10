using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = .4f;
    Vector2 touchPosition;
    private Rigidbody2D rb2d;
    private bool isTouching = false;
    float movementHorizontal;
    float speed = 5;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleTouchInput();

        movementHorizontal= Input.GetAxis("Horizontal");
        transform.position += Vector3.right* movementHorizontal*speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchPosition = touch.deltaPosition;
                    isTouching = true;
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    touchPosition = Vector2.zero;
                    rb2d.velocity = Vector2.zero;
                    isTouching = false;
                    break;
            }
        }
        else if (!isTouching)
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    void MovePlayer()
    {
        if (isTouching)
        {
            rb2d.MovePosition(rb2d.position + touchPosition * movementSpeed * Time.deltaTime);
        }
    }
}