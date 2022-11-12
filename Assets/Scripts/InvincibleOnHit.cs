using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Life))]
public class InvincibleOnHit : MonoBehaviour
{
    public float invincibilityTime;

    private Life lifeComponent;

    private void Awake()
    {
        lifeComponent = GetComponent<Life>();
    }

    private void Start()
    {
       lifeComponent.onDamageTaken.AddListener(OnDamageTaken); 
    }

    private async void OnDamageTaken(uint lifePoint)
    {
        lifeComponent.isInvincible = true;
        var timeInMilliseconds = Mathf.RoundToInt(invincibilityTime * 1000); 
        await Task.Delay(timeInMilliseconds);
        lifeComponent.isInvincible = false;
    }
}
