using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2DMovement movement;
    private Life _life;

    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
        _life = GetComponent<Life>();
    }

    private void Start()
    {
       movement.SetDirection(transform.up); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _life.TakeDamage(1);
        }
    }
}
