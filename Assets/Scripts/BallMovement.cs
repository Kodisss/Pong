using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BallMovement : MonoBehaviour
{
    private GameController game; // communicate with game

    private Rigidbody2D rb;

    [Header("FX")]
    [SerializeField] private GameObject bounceVFX;
    [SerializeField] private GameObject bounceSFX;
    [SerializeField] private GameObject explosionSFX;

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
            PlayExplosionSound();
            game.Score(1);
        }
        else if (collision.gameObject.CompareTag("DeathLeft"))
        {
            PlayExplosionSound();
            game.Score(2);
        }
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb.velocity *= speedIncrement;
        }

        
        if(collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("Wall"))
        {
            // VFX stuff
            Vector3 normal = collision.contacts[0].normal;

            // Gets rotation to the normal of the collision
            Quaternion targetRotation = Quaternion.LookRotation(normal);

            GameObject bounce = Instantiate(bounceVFX, transform.position, targetRotation);
            Destroy(bounce, 0.5f);

            // SFX stuff
            GameObject hitSound = Instantiate(bounceSFX);
            Destroy(hitSound, 0.5f);
        }
    }

    private void PlayExplosionSound()
    {
        GameObject explosionSound = Instantiate(explosionSFX);
        Destroy(explosionSound, 0.5f);
    }

    private void StartMoving()
    {
        // Generate a random angle between -45 and 45 degre either towards the player or the opponent
        float randomAngle = Random.Range(30f, 60f);
        Vector2 leftOrRight = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;

        // Convert the angle to a direction vector
        Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * leftOrRight;

        // Apply the velocity to the object
        rb.velocity = direction * speed;
    }
}
