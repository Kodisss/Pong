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

    private int difficulty;
    private float speed;
    private float smoothingSpeed;

    [SerializeField] private bool debug = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializeAIDifficulty();
    }

    // Update is called once per frame
    private void Update()
    {
        // check for the ball
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRb = ball.GetComponent<Rigidbody2D>();

        AIGestion();
    }

    private void InitializeAIDifficulty()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");

        if (debug) Debug.Log(difficulty);

        if (difficulty == 0)
        {
            speed = 5f;
            smoothingSpeed = 5f;
        }else if (difficulty == 1)
        {
            speed = 7f;
            smoothingSpeed = 12f;
        }else if (difficulty == 2)
        {
            smoothingSpeed = 10f;
            speed = 10f;
        }
    }

    private void AIGestion()
    {
        // this checks if the ball is moving
        if (ballRb.velocity.magnitude != 0)
        {
            if (difficulty == 2) // if the game is setup to hard difficulty move
            {
                AIMove();
            }
            else if (ballRb.velocity.x > 0) // if not, move only when the ball comes towards you
            {
                AIMove();
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, smoothingSpeed * Time.deltaTime);
            }
        }
        // else just stop moving
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, smoothingSpeed * Time.deltaTime);
        }
    }

    private void AIMove()
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
