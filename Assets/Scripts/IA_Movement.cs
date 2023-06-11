using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class IA_Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    private GameObject ball;
    private Rigidbody2D ballRb;

    [Header("Moving Variables")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothingSpeed = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // check for the ball
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRb = ball.GetComponent<Rigidbody2D>();

        // uses the right difficulty AI
        EasyAI();
    }

    private void EasyAI()
    {
        // this checks if the ball is moving and especially towards the paddle, if not it stops
        if (ballRb.velocity.magnitude != 0 && ballRb.velocity.x > 0)
        {
            EasyAIMove();
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, smoothingSpeed * Time.deltaTime);
        }
    }

    private void EasyAIMove()
    {
        // if the ball is higher than the paddle make the paddle move smoothly towards the balls height
        if (ball.transform.position.y > this.transform.position.y)
        {
            if (rb.velocity.y < 0) rb.velocity = Vector2.zero;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.up * speed, smoothingSpeed * Time.deltaTime);
        }
        // same but if the ball is lower
        else if (ball.transform.position.y < this.transform.position.y)
        {
            if (rb.velocity.y > 0) rb.velocity = Vector2.zero;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.down * speed, smoothingSpeed * Time.deltaTime);
        }
        // else just stop
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, smoothingSpeed * Time.deltaTime);
        }
    }
}
