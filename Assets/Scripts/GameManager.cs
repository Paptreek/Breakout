using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public GameObject ball;
    public GameObject player;

    private int _score;
    private int _lives = 3;

    public static bool s_isWaitingToStart = true;

    void Update()
    {
        scoreText.text = _score.ToString();
        livesText.text = _lives.ToString();

        ResetGame();
        EndGame();
    }

    private void ResetGame()
    {
        if (ball.GetComponent<Ball>().hasGoneBelowPaddle == true)
        {
            ball.GetComponent<Ball>().hasGoneBelowPaddle = false;
            ball.GetComponent<Ball>().Restart();
            player.GetComponent<PlayerController>().Restart();
            s_isWaitingToStart = true;
            _lives--;
            Debug.Log($"Ball go bye bye");
        }
        // This triggers when some bool from Ball is true, then sets that bool back to false

        // When game is reset:
        // - Ball goes back to starting position
        // - Paddle goes back to starting position
        // - s_isWaitingToStart is set to true
        // - a life is lost
    }

    private void EndGame()
    {
        if (_lives < 0)
        {
            Debug.Log($"Game Over!");
        }
    }
}
