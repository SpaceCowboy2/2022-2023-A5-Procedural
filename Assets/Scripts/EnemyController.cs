using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private Rigidbody2DMovement movement;

    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        if (player != null)
            movement.SetDirection(player.position - transform.position);
        else
            movement.SetDirection(Vector2.zero);
    }


}
