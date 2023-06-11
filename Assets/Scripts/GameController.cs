using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject explosionVFX;

    private GameObject currentBall;

    [Header("Score UI")]
    [SerializeField] private GameObject player1Text;
    [SerializeField] private GameObject player2Text;

    [SerializeField] private bool debug;

    private float player1Score = 0;
    private float player2Score = 0;

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
            player1Score++;
            player1Text.GetComponent<TextMeshProUGUI>().text = player1Score.ToString();
            if(debug) Debug.Log("Player 1 Scored!");
        }
        else if (playerNumber == 2)
        {
            player2Score++;
            player2Text.GetComponent<TextMeshProUGUI>().text = player2Score.ToString();
            if (debug) Debug.Log("Player 2 Scored!");
        }

        if (debug) Debug.Log("Player 1: " + player1Score + " - Player 2: " + player2Score);

        // Check if a player has reached the winning score
        if (player1Score >= 5 || player2Score >= 5)
        {
            if (debug) Debug.Log("Game Over!");
            EndGame();
        }

        // Destroy the current ball
        GameObject explosion = Instantiate(explosionVFX, currentBall.transform.position, currentBall.transform.rotation);
        Destroy(currentBall);
        Destroy(explosion, 0.5f);

        // Spawn a new ball
        SpawnBall();
    }

    private void EndGame()
    {
        if(player1Score < player2Score)
        {
            SceneManager.LoadScene("LoseScreen");
        }
        else
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
