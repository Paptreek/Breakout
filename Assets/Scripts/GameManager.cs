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

        _score = ball.GetComponent<Ball>().GetNumberOfBricksBroken() * 100;

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
        }
    }

    private void EndGame()
    {
        if (_lives < 0)
        {
            Debug.Log($"Game Over!");
        }
    }
}
