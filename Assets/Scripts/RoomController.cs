using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Vector2Int roomSize;
    private RoomController[] neighbours;

    private RoomEnemiesManager enemiesManager;
    private LevelManager levelManager;

    private void Awake()
    {
        enemiesManager = GetComponentInChildren<RoomEnemiesManager>();
        levelManager = GetComponentInParent<LevelManager>();
    }

    private void Start()
    {
        neighbours = levelManager.GetNeighbours(this);
    }

    public void SetRoomActive(bool isActive)
    {
        enemiesManager.SetAllEnemiesInRoomActive(isActive);
    }
}
