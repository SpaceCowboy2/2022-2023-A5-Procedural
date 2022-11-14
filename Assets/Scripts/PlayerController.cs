using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 lastDirection;
    private Life life;
    private Rigidbody2DMovement movement;
    private Shooter[] shooters;

    private void Awake()
    {
        shooters = GetComponentsInChildren<Shooter>();
        life = GetComponent<Life>();
        movement = GetComponent<Rigidbody2DMovement>();
    }

    private void Start()
    {
        life.onDie = OnDie;
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        movement.SetDirection(input.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            var inputDirection = input.ReadValue<Vector2>();
            if (inputDirection.sqrMagnitude <= 1)
                transform.up = inputDirection;
            foreach (var shooter in shooters)
            {
                shooter.StartShooting();
            }
        }
        else if (input.canceled)
        {
            foreach (var shooter in shooters)
            {
                shooter.StopShooting();
            }
        }
    }

    public void OnAutoDie(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        life.TakeDamage(life.currentLife);
    }

    private async Task OnDie()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        movement.SetDirection(Vector2.zero);
        Debug.Log("Start dying");
        await Task.Delay(1000);
        Debug.Log("Dead");
    }

    public async void ChangeColor(uint lifePoint)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var oldColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        await Task.Delay(16);
        while (life.isInvincible && life.currentLife > 0)
            await Task.Delay(16);
        spriteRenderer.color = oldColor;

    }
}
