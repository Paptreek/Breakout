using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    public GameObject player;
    public GameObject brick;
    public GameObject bricks;

    public GameObject brickSound;
    public GameObject paddleSound;
    public GameObject wallSound;

    public bool hasGoneBelowPaddle;

    private Rigidbody2D _rb;
    private float _moveSpeed = 250.0f;
    private int _bricksBroken;
    private Vector2 _maxVelocity;
    private bool _hasVelocityReachedThreshold;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _maxVelocity = _rb.linearVelocity;

        if (_maxVelocity.y > 7.5f)
        {
            _hasVelocityReachedThreshold = true;
        }

        Debug.Log(_maxVelocity);

        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.s_isWaitingToStart)
        {
            AddStartingForce();
            GameManager.s_isWaitingToStart = false;
        }
    }

    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? Random.Range(-0.75f, -0.25f) : Random.Range(0.25f, 0.75f);
        float y;

        if (!_hasVelocityReachedThreshold)
        {
            y = 1.0f;
        }
        else
        {
            y = 1.5f;
        }

        _rb.AddForce(new Vector2(x, y) * _moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;

        float contactPoint = collision.GetContact(0).point.x;
        float leftOfPaddle = player.GetComponent<BoxCollider2D>().bounds.min.x;
        float rightOfPaddle = player.GetComponent<BoxCollider2D>().bounds.max.x;

        if (collision.gameObject == player)
        {
            paddleSound.gameObject.GetComponent<AudioSource>().Play();

            if (contactPoint == leftOfPaddle)
            {
                // if ball collides with left edge of the paddle, direction changes to negative
                _rb.linearVelocity = new Vector2(-Mathf.Abs(-_rb.linearVelocity.x), _rb.linearVelocity.y);
                _rb.linearVelocityX -= 0.5f;
                Debug.Log($"Collided with LEFT edge! New direction: {_rb.linearVelocity.x}");
            }
            else if (contactPoint == rightOfPaddle)
            {
                // if ball collides with right edge, direction changes to positive
                _rb.linearVelocity = new Vector2(Mathf.Abs(_rb.linearVelocity.x), _rb.linearVelocity.y);
                _rb.linearVelocityX += 0.5f;
                Debug.Log($"Collided with RIGHT edge! New direction: {_rb.linearVelocity.x}");
            }
        }

        if (collision.gameObject.CompareTag("Brick"))
        {
            _rb.AddForce(normal * 3.5f);
            _bricksBroken++;

            brickSound.gameObject.GetComponent<AudioSource>().Play();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            wallSound.GetComponent<AudioSource>().Play();
        }

        if (collision.gameObject.name == "Ceiling")
        {
            Debug.Log($"Collided with ceiling!");
            player.GetComponent<PlayerController>().ShrinkPaddle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            hasGoneBelowPaddle = true;
            player.GetComponent<PlayerController>().GrowPaddle();
        }
    }

    public int GetNumberOfBricksBroken()
    {
        return _bricksBroken;
    }

    public bool AreAllBricksBroken()
    {
        if (_bricksBroken >= bricks.gameObject.GetComponent<CreateBricks>().GetCounter())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Restart()
    {
        transform.position = new Vector3(0, -3, 0);
        _rb.linearVelocity = new Vector2(0, 0);
    }
}
