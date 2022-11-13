using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Vector2Int roomSize;
    private RoomController[] neighbours;

    private RoomManager roomManager;

    private void Awake()
    {
        roomManager = GetComponentInParent<RoomManager>();
    }

    private void Start()
    {
        neighbours = roomManager.GetNeighbours(this);
    }
}
