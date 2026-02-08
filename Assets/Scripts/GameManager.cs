using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text livesText;

    public GameObject ball;
    public GameObject player;
    public GameObject gameOver;

    private int _score;
    private int _lives = 0; // set to 0 for testing
    private bool _isGameOver;

    public static bool s_isWaitingToStart = true;

    void Update()
    {
        scoreText.text = _score.ToString();
        livesText.text = _lives.ToString();

        _score = ball.GetComponent<Ball>().GetNumberOfBricksBroken() * 100;

        ResetScreen();
        EndGame();
    }

    private void ResetScreen()
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
            _isGameOver = true;
            s_isWaitingToStart = false;
            gameOver.gameObject.SetActive(true);
            player.GetComponent<PlayerController>().canMove = false;
            _lives = 0;
        }
    }

    public void StartNewGame()
    {
        if (_isGameOver == true)
        {
            SceneManager.LoadScene($"Breakout");
            player.GetComponent<PlayerController>().canMove = true;
            s_isWaitingToStart = true;
        }
    }
}
