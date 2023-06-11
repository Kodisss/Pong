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
    [SerializeField] private float speedIncrement = 1.1f;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(StartMoving), 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathRight"))
        {
            game.Score(1);
        }
        else if (collision.gameObject.CompareTag("DeathLeft"))
        {
            game.Score(2);
        }
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb.velocity *= speedIncrement;
        }
    }

    private void StartMoving()
    {
        // Generate a random angle between -45 and 45 degre either towards the player or the opponent
        float randomAngle = Random.Range(-45f, 45f);
        Vector2 leftOrRight = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;

        // Convert the angle to a direction vector
        Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * leftOrRight;

        // Apply the velocity to the object
        rb.velocity = direction * speed;
    }
}
