using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    public uint startLife;

    public uint currentLife { get; private set; }

    public UnityEvent<float> onDamageTaken;

    public Func<Task> onDie;

    private void Start()
    {
        currentLife = startLife;
    }

    public void TakeDamage(uint damage)
    {
        currentLife -= damage;
        onDamageTaken.Invoke(currentLife);
        if (currentLife == 0)
            Die();
    }

    private async void Die()
    {
        if(onDie != null)
            await onDie();
        Destroy(gameObject);
    }
}
