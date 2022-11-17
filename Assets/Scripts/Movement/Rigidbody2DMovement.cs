using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rigidbody2DMovement : MonoBehaviour
{
    public float speed;

    [HideInInspector]
    public bool fixRotation = false;
    
    private Rigidbody2D myRigidbody;
    private Vector2 _direction = Vector2.zero;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!fixRotation && _direction.sqrMagnitude <= 1)
            transform.up = _direction;
    }

    public void SetDirection(Vector2 newDirection)
    {
        newDirection.Normalize();
        myRigidbody.velocity = newDirection * speed;
    }

    public void SetRotation(Vector2 newDirection)
    {
        newDirection.Normalize();
        _direction = newDirection;
    }
}
