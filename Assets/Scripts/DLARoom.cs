using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLARoom : MonoBehaviour
{
    private List<DoorPos> doorPositions = new List<DoorPos>();
    public GameObject sceneObject;

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

    public void SetObject(GameObject obj)
    {
        sceneObject = obj;
    }

    public void SetMirrorDoors(List<DoorPos> doors)
    {
        foreach (DoorPos door in doors)
        {
            if (door == DoorPos.Left)
            {
                doorPositions.Add(DoorPos.Right);
            } else if (door == DoorPos.Right)
            {
                doorPositions.Add(DoorPos.Left);
            }
            else
            {
                doorPositions.Add(door);
            }
        }
    }

    public List<DoorPos> GetDoors()
    {
        return doorPositions;
    }
}
