using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float disappearTime;
    public GameObject openDoor;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.TryGetComponent(out Inventory inventory))
            return;
        var lastKey = inventory.allPickups.FirstOrDefault(pickup => pickup.type == PickupType.Key);
        if (lastKey == null)
            return;
        inventory.RemovePickup(lastKey);
        Disappear(lastKey);
    }

    private async void Disappear(Pickup usedKey)
    {
        var renderersToFade = new List<SpriteRenderer>
        {
            usedKey.GetComponent<SpriteRenderer>(), 
            GetComponent<SpriteRenderer>()
        };
        var startTime = Time.time;
        while (renderersToFade[0].color.a > 0)
        {
            foreach (var fadingRenderer in renderersToFade)
            {
                var elapsedTime = Time.time - startTime;
                var completedPercent = elapsedTime / disappearTime;
                var newColor = fadingRenderer.color;
                newColor.a = 1 - completedPercent;
                fadingRenderer.color = newColor;
                await Task.Delay(20);
            }
        }

        var myTransform = transform;
        Instantiate(openDoor, myTransform.position, myTransform.rotation, myTransform.parent);

        foreach (var spriteRenderer in renderersToFade)
        {
            Destroy(spriteRenderer.gameObject);
        }
    }
}
