using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _mousePosition;

    void Start()
    {
        _mousePosition = Mouse.current.position.value;
    }

    void Update()
    {
        Debug.Log(_mousePosition);
    }
}
