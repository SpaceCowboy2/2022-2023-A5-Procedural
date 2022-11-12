using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Vector2Int roomSize;
    private List<RoomController> neighbours;

    private RoomManager roomManager;

    private void Awake()
    {
        roomManager = GetComponentInParent<RoomManager>();
    }

    private void Start()
    {
        var rooms = roomManager.GetNeighbours(this);
        foreach (var room in rooms)
        {
            Debug.Log($"I’m {name} : {room.transform.position}");
        }
    }
}
