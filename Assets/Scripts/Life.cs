using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    public bool isInvincible;
    public uint startLife;
    public UnityEvent<uint> onDamageTaken;

    public uint currentLife { get; private set; }
    public Func<Task> onDie;
    
    private bool isAlive = true;

    private void Start()
    {
        currentLife = startLife;
    }

    public void TakeDamage(uint damage)
    {
        if(!isAlive || isInvincible) return;
        if (damage > currentLife)
            damage = currentLife;
        currentLife -= damage;
        onDamageTaken.Invoke(currentLife);
        if (currentLife == 0)
            Die();
    }

    private async void Die()
    {
        isAlive = false;
        if(onDie != null)
            await onDie();
        Destroy(gameObject);
    }
}
