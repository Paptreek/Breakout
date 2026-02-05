using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    private Vector3 _mousePosition;

    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        _mousePosition.y = transform.position.y;
        _mousePosition.z = transform.position.z;

        MovePaddleWithinScreen();
    }

    private void MovePaddleWithinScreen()
    {
        float cursor = _mousePosition.x;
        float halfOfPaddle = GetComponent<Renderer>().bounds.size.x;
        float rightEdgeOfLeftWall = leftWall.GetComponent<Renderer>().bounds.max.x;
        float leftEdgeOfRightWall = rightWall.GetComponent<Renderer>().bounds.min.x;

        if (cursor - halfOfPaddle / 2 >= rightEdgeOfLeftWall && cursor + halfOfPaddle / 2 <= leftEdgeOfRightWall)
        {
            transform.position = _mousePosition;
        }
    }
}
