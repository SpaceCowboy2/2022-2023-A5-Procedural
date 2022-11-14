using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private Rigidbody2DMovement movement;

    private Vector2 spawnPosition;
    private RoomEnemiesManager enemiesManager;

    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
        player = FindObjectOfType<PlayerController>().transform;
        spawnPosition = transform.position;
        enemiesManager = GetComponentInParent<RoomEnemiesManager>();
        enemiesManager.AddEnemyToRoom(this);
    }

    private void Start()
    {
        enabled = false;
    }

    private void OnDisable()
    {
        movement.SetDirection(Vector2.zero);
        GetComponent<Rigidbody2D>().position = spawnPosition;
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            enabled = false;
            return;
        }

        movement.SetDirection(player.position - transform.position);
    }

    private void OnDestroy()
    {
        enemiesManager.RemoveEnemyFromRoom(this);
    }
}
