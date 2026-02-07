using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject player;
    public GameObject brick;
    public GameObject wall;

    public GameObject brickSound;
    public GameObject paddleSound;
    public GameObject wallSound;

    private Rigidbody2D _rb;
    private float moveSpeed = 250.0f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        AddStartingForce();
    }

    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? Random.Range(-0.75f, -0.5f) : Random.Range(0.5f, 0.75f);
        Debug.Log(x);

        _rb.AddForce(new Vector2(x, 1) * moveSpeed);
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
                Debug.Log($"Collided with LEFT edge! New direction: {_rb.linearVelocity.x}");
            }
            else if (contactPoint == rightOfPaddle)
            {
                // if ball collides with right edge, direction changes to positive
                _rb.linearVelocity = new Vector2(Mathf.Abs(_rb.linearVelocity.x), _rb.linearVelocity.y);
                Debug.Log($"Collided with RIGHT edge! New direction: {_rb.linearVelocity.x}");
            }
        }

        if (collision.gameObject.CompareTag("Brick"))
        {
            _rb.AddForce(normal * 5);

            brickSound.gameObject.GetComponent<AudioSource>().Play();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            wallSound.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
