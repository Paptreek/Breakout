using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float moveSpeed = 250.0f;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        AddStartingForce();
    }

    private void AddStartingForce()
    {
        float x = Random.Range(-1.0f, 1.0f);

        _rb.AddForce(new Vector2(x, 1) * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        //_rb.AddForce(normal * 25);
    }
}
