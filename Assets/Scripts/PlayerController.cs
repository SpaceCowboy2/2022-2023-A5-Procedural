using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform bullet;
    public float fireDelay;

    private bool isShooting;
    private Vector2 lastDirection;
    private Transform bulletsParent;
    private float lastShootTime;
    private Rigidbody2DMovement movement;

    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
        bulletsParent = new GameObject("Bullets").transform;
    }

    private void Start()
    {
        GetComponent<Life>().onDie = OnDie;
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        movement.SetDirection(input.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext input)
    {
        if (input.canceled)
        {
            isShooting = false;
            return;
        }

        if (!input.performed) return;
        var inputDirection = input.ReadValue<Vector2>();
        if(inputDirection.sqrMagnitude <= 1)
            transform.up = inputDirection;
        isShooting = true;
    }

    public void OnAutoDie(InputAction.CallbackContext input)
    {
        if(!input.performed)
            return;
        
        GetComponent<Life>().TakeDamage(GetComponent<Life>().currentLife);
    }

    private void Update()
    {
        if (isShooting)
            TryToShoot();
    }

    private void TryToShoot()
    {
        if (!(Time.time > lastShootTime + fireDelay)) return;
        var myTransform = transform;
        var bulletPosition = myTransform.position + myTransform.up;
        var newBullet = Instantiate(bullet, bulletPosition, Quaternion.identity, bulletsParent);
        newBullet.up = transform.up;
        lastShootTime = Time.time;
    }

    private async Task OnDie()
    {
        Debug.Log("Start dying");
        await Task.Delay(1000);
        Debug.Log("Stopped dying");
    }
}
