using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLARoom : MonoBehaviour
{
    private List<DoorPos> doorPositions = new List<DoorPos>();

    public DLARoom(Vector2Int randomDir)
    {
        if (randomDir.x == 1) doorPositions.Add(DoorPos.Left);
        if (randomDir.x == -1) doorPositions.Add(DoorPos.Right);
        if (randomDir.y == 1) doorPositions.Add(DoorPos.Bottom);
        if (randomDir.y == -1) doorPositions.Add(DoorPos.Top);
    }

    public void UpdateDoors(Vector2Int randomDir)
    {
        if (randomDir.x == 1) doorPositions.Add(DoorPos.Right);
        if (randomDir.y == 1) doorPositions.Add(DoorPos.Top);
        if (randomDir.y == -1) doorPositions.Add(DoorPos.Bottom);
    }
}
