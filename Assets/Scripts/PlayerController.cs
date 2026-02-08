using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject paddleSound;

    public bool canMove = true;
    private bool _hasShrunk;

    private Vector3 _mousePosition;

    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        _mousePosition.y = transform.position.y;
        _mousePosition.z = transform.position.z;

        if (!GameManager.s_isWaitingToStart)
        {
            MovePaddleWithinScreen();
        }

        if (_hasShrunk)
        {
            transform.localScale = new Vector3(1.0f, 0.25f, 1.0f);
        }
    }

    private void MovePaddleWithinScreen()
    {
        float cursor = _mousePosition.x;
        float halfOfPaddle = GetComponent<Renderer>().bounds.size.x;
        float rightEdgeOfLeftWall = leftWall.GetComponent<Renderer>().bounds.max.x;
        float leftEdgeOfRightWall = rightWall.GetComponent<Renderer>().bounds.min.x;

        if (cursor - halfOfPaddle / 2 >= rightEdgeOfLeftWall && cursor + halfOfPaddle / 2 <= leftEdgeOfRightWall && canMove)
        {
            transform.position = _mousePosition;
        }
    }

    public void Restart()
    {
        transform.position = new Vector3(0, -3.5f, 0);
    }

    public void ShrinkPaddle()
    {
        if (!_hasShrunk)
        {
            transform.localScale = new Vector3(1.0f, 0.25f, 1.0f);
            _hasShrunk = true;
        }
    }

    public void GrowPaddle()
    {
        if (_hasShrunk)
        {
            transform.localScale = new Vector3(1.25f, 0.25f, 1.0f);
            _hasShrunk = false;
        }
    }
}
