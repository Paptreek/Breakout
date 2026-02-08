using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text clickToStartText;

    public GameObject ball;
    public GameObject player;
    public GameObject gameOver;

    private int _score;
    private int _lives = 3;
    private bool _isGameOver;
    private bool _isMuted;

    public static bool s_isWaitingToStart = true;

    void Update()
    {
        CheckForToggleMute();

        scoreText.text = _score.ToString();
        livesText.text = _lives.ToString();

        _score = ball.GetComponent<Ball>().GetNumberOfBricksBroken() * 100;

        ResetScreen();
        EndGame();
        ShowClickToStart();
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

    private void ShowClickToStart()
    {
        if (s_isWaitingToStart == true && gameOver.gameObject.activeInHierarchy == false)
        {
            clickToStartText.gameObject.SetActive(true);
        }
        else
        {
            clickToStartText.gameObject.SetActive(false);
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

    void CheckForToggleMute()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame && !_isMuted)
        {
            AudioListener.volume = 0;
            _isMuted = true;
        }
        else if (Keyboard.current.mKey.wasPressedThisFrame && _isMuted)
        {
            AudioListener.volume = 1;
            _isMuted = false;
        }
    }
}
