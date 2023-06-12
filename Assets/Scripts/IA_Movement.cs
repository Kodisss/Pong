using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class IA_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private NavMeshAgent agent;

    private GameObject ball;
    private Rigidbody2D ballRb;

    private int difficulty;
    private float speed;
    private float smoothingSpeed = 10f;

    [SerializeField] private bool debug = false;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        }
        else if (difficulty == 1)
        {
            speed = 3f;
        }
        else if (difficulty == 2)
        {
            speed = 6f;
        }
    }

    private void AIGestion()
    {
        // this checks if the ball is moving
        if (ballRb.velocity.magnitude != 0)
        {
            if (ballRb.velocity.x > 0) // if not, move only when the ball comes towards you
            {
                if (difficulty == 0) // use the simple follow the ball algorythm for easy mode
                {
                    EasyAIMove();
                }
                else
                {
                    HardAIMove(); // else use the prediction algorythm for the rest
                }
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

    private void EasyAIMove()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, UpOrDown(ball.transform.position.y) * speed, smoothingSpeed * Time.deltaTime);
    }

    private Vector2 UpOrDown(float yPosition)
    {
        // if the ball is higher than the paddle make the paddle move smoothly towards the balls height
        if (yPosition > this.transform.position.y)
        {
            return Vector2.up;

        }
        // same but if the ball is lower
        else if (yPosition < this.transform.position.y)
        {
            return Vector2.down;
        }
        // else just stop
        else
        {
            return Vector2.zero;
        }
    }

    private void HardAIMove()
    {
        // Calculate the vertical input
        float destination = ImpactPrediction();

        if(transform.position.y != destination)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, UpOrDown(destination) * speed, smoothingSpeed * Time.deltaTime);
        }
    }

    private float ImpactPrediction()
    {
        float impact = 0f;
        Vector3 origin = ball.transform.position;
        Vector3 direction = ballRb.velocity.normalized;
        float maxDistance = 15f;

        LayerMask layerMaskHit = LayerMask.GetMask("BallHit");
        LayerMask layerMaskWall = LayerMask.GetMask("Wall");

        if (debug) Debug.DrawRay(origin, direction * maxDistance, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMaskWall);

        // Raycast hit a wall
        if (hit)
        {
            // Calculate the reflection direction
            Vector2 reflectionDirection = Vector2.Reflect(direction, hit.normal);
            if (debug) Debug.DrawRay(hit.point, reflectionDirection * maxDistance, Color.green);

            // Perform the reflection raycast off of the wall
            RaycastHit2D reflectionHit = Physics2D.Raycast(hit.point, reflectionDirection, maxDistance, layerMaskHit);

            if(reflectionHit) impact = reflectionHit.point.y; // if it hits something tell the paddle so it knows
        }
        else
        {
            hit = Physics2D.Raycast(origin, direction, maxDistance, layerMaskHit);
            impact = hit.point.y;
        }

        return impact;
    }
}
