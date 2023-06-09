using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BallMovement : MonoBehaviour
{
    private GameController game; // communicate with game

    private Rigidbody2D rb;

    [Header("Moving Variables")]
    [SerializeField] private float speed = 5f;
    private Vector2 direction;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        StartMoving();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I bumped");

        if (collision.gameObject.CompareTag("Wall"))
        {
            direction.y = -direction.y;
        }else if (collision.gameObject.CompareTag("Paddle"))
        {
            speed += Random.Range(0.1f, 0.3f);
            direction.x = -direction.x;
        }
        else if (collision.gameObject.CompareTag("DeathRight"))
        {
            game.Score(1);
        }
        else if (collision.gameObject.CompareTag("DeathLeft"))
        {
            game.Score(2);
        }
    }

    private void StartMoving()
    {
        // Generate a random direction for the ball to start moving
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }
}
