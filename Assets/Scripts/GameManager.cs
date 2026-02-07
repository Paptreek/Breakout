using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public GameObject ball;

    private int _score;
    private int _lives = 3;

    void Update()
    {
        scoreText.text = _score.ToString();
        livesText.text = _lives.ToString();

        _score = ball.GetComponent<Ball>().GetNumberOfBricksBroken() * 100;
    }
}
