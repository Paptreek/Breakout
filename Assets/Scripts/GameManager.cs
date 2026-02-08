using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text livesText;
    public TMP_Text clickToStartText;

    public GameObject ball;
    public GameObject player;
    public GameObject bricks;
    public GameObject gameOver;
    public GameObject gameOverWin;
    public GameObject highScore;

    private int _score;
    private int _lives = 3;
    private bool _isGameOver;
    private bool _isMuted;
    private float _timeLeft = 300;

    public static bool s_isWaitingToStart = true;

    void Update()
    {
        CheckForToggleMute();
        ShowClickToStart();
        ShowUIText();

        if (ball != null)
        {
            _score = ball.GetComponent<Ball>().GetNumberOfBricksBroken() * 100;
            CheckForScreenReset();
            CheckForGameEnd();
        }

        if (_score > GetHighScore("HighScore"))
        {
            SetHighScore("HighScore", _score);
        }
    }

    private void CheckForScreenReset()
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

    private void CheckForGameEnd()
    {
        if (_lives < 0)
        {
            ball.gameObject.SetActive(false);
            player.gameObject.SetActive(false);

            _lives = 0;
            _isGameOver = true;
            s_isWaitingToStart = false;
            
            gameOver.gameObject.SetActive(true);
            highScore.gameObject.SetActive(true);
            
            player.GetComponent<PlayerController>().canMove = false;
        }
        
        if (ball.GetComponent<Ball>().AreAllBricksBroken())
        {
            Destroy(ball);

            _isGameOver = true;
            s_isWaitingToStart = false;
            _score += (Convert.ToInt32(_timeLeft) * 10) + (_lives * 2500);
            
            gameOverWin.gameObject.SetActive(true);
            highScore.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
            
            player.GetComponent<PlayerController>().canMove = false;
        }
    }

    private void ShowUIText()
    {
        scoreText.text = _score.ToString("0000");
        highScoreText.text = GetHighScore("HighScore").ToString("0000");
        livesText.text = _lives.ToString();
    }

    private void ShowClickToStart()
    {
        if (s_isWaitingToStart == true && gameOver.gameObject.activeInHierarchy == false && gameOverWin.gameObject.activeInHierarchy == false)
        {
            clickToStartText.gameObject.SetActive(true);
        }
        else
        {
            clickToStartText.gameObject.SetActive(false);
            StartTimer();
        }
    }

    private void CheckForToggleMute()
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

    private void StartTimer()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
        }

        Debug.Log(_timeLeft.ToString("0.0"));
    }

    private void SetHighScore(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    private int GetHighScore(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    private void StartNewGame()
    {
        // gets called when player clicks "PLAY AGAIN" button
        if (_isGameOver == true)
        {
            SceneManager.LoadScene($"Breakout");
            player.GetComponent<PlayerController>().canMove = true;
            s_isWaitingToStart = true;
        }
    }
}
