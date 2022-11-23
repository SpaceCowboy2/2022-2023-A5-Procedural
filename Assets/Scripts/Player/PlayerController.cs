﻿using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 lastDirection;
    private Life life;
    private Rigidbody2DMovement movement;
    private Shooter[] shooters;
    private MeleeAttack meleeAttack;
    public GameObject PrefBomb;
    private float CooldownBomb = 0;
    public float Cooldown = 1;


    private void Awake()
    {
        shooters = GetComponentsInChildren<Shooter>();
        life = GetComponent<Life>();
        movement = GetComponent<Rigidbody2DMovement>();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    private void Start()
    {
        life.onDie = OnDie();
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        var inputDirection = input.ReadValue<Vector2>();
        movement.SetDirection(inputDirection);

        if (input.performed)
        {
            movement.SetRotation(inputDirection);
        }
    }

    public void Bomb()
    {
       
            Instantiate(PrefBomb, transform.position, Quaternion.identity);
        
    }

    public void OnMeleeAttack(InputAction.CallbackContext input)
    {
        /*if (input.performed)
        {
            meleeAttack.Attack();
        }*/
    }

    public void OnAutoDie(InputAction.CallbackContext input)
    {
        /*if (!input.performed)
            return;

        life.TakeDamage(life.currentLife);*/
    }

    private IEnumerator OnDie()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        movement.SetDirection(Vector2.zero);
        Debug.Log("Start dying");
        yield return new WaitForSeconds(1);
        Debug.Log("Dead");
    }

    public void ChangeColor(uint lifePoint)
    {
        StartCoroutine(ChangeColorCoroutine());

        IEnumerator ChangeColorCoroutine()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var oldColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return null;
            while (life.isInvincible && life.currentLife > 0)
                yield return null;
            spriteRenderer.color = oldColor;
        }
    }
    private void Update()
    {
        CooldownBomb -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && CooldownBomb <= 0)
        {
            Bomb();
            CooldownBomb = PrefBomb.GetComponent<Bomb>().Countdown;
        }
    }
}
