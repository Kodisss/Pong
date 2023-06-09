using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    private GameObject currentBall;

    private float scorePlayer1 = 0;
    private float scorePlayer2 = 0;

    // Start is called before the first frame update
    private void Start()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        // Instantiate a new ball at the spawn point
        currentBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }

    public void Score(int playerNumber)
    {
        if (playerNumber == 1)
        {
            scorePlayer1++;
            Debug.Log("Player 1 Scored!");
        }
        else if (playerNumber == 2)
        {
            scorePlayer2++;
            Debug.Log("Player 2 Scored!");
        }

        Debug.Log("Player 1: " + scorePlayer1 + " - Player 2: " + scorePlayer2);

        // Check if a player has reached the winning score
        if (scorePlayer1 >= 5 || scorePlayer2 >= 5)
        {
            Debug.Log("Game Over!");

            // Reset the scores
            scorePlayer1 = 0;
            scorePlayer2 = 0;
        }

        // Destroy the current ball
        Destroy(currentBall);

        // Spawn a new ball
        SpawnBall();
    }
}
