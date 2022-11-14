using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float disappearTime;
    public GameObject openDoor;

    private SpriteRenderer connectedDoor;

    private void Start()
    {
        var grid = GetComponentInParent<Grid>();
        var allDoors = GameObject.FindGameObjectsWithTag("Door");
        var allDistances = allDoors.Select(DistanceSelector).ToArray();
        for (var i = 0; i < allDoors.Length; i++)
        {
            if (allDistances[i] < grid.cellSize.magnitude)
                connectedDoor = allDoors[i].GetComponent<SpriteRenderer>();
        }
    }

    private float DistanceSelector(GameObject doorObject)
    {
        return Vector2.Distance(doorObject.transform.position, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.TryGetComponent(out Inventory inventory))
            return;
        TryOpen(inventory);
    }

    private void TryOpen(Inventory inventory)
    {
        var lastKey = inventory.allPickups.FirstOrDefault(pickup => pickup.type == PickupType.Key);
        if (lastKey == null)
            return;
        inventory.RemovePickup(lastKey);
        Open(lastKey);
    }

    private void Open(Pickup usedKey)
    {
        var myTransform = transform;
        Instantiate(openDoor, myTransform.position, myTransform.rotation, myTransform.parent);
        Disappear(usedKey.GetComponent<SpriteRenderer>());
        Disappear(GetComponent<SpriteRenderer>());

        if (connectedDoor == null)
            return;

        var connectedTransform = connectedDoor.transform;
        Instantiate(openDoor, connectedTransform.position, connectedTransform.rotation, connectedTransform.parent);
        Disappear(connectedDoor);
    }

    private async void Disappear(SpriteRenderer toDisappear)
    {
        var startTime = Time.time;

        while (toDisappear.color.a > 0)
        {
            var elapsedTime = Time.time - startTime;
            var completedPercent = elapsedTime / disappearTime;
            var newColor = toDisappear.color;
            newColor.a = 1 - completedPercent;
            toDisappear.color = newColor;
            await Task.Delay(20);
        }

        Destroy(toDisappear.gameObject);
    }
}
