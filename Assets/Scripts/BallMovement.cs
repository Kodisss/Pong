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

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        StartMoving();
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
    }

    private void StartMoving()
    {
        // Generate a random direction for the ball to start moving
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }
}
