using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorPositions
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}

public abstract class Room : MonoBehaviour
{
    protected abstract void GenerateRoom();

    protected abstract void Randomize();

    protected List<Vector3> GetDoorPositions(List<DoorPositions> doorPositionsList)
    {
        List<Vector3> result = new List<Vector3>();

        for( int i = 0; i < doorPositionsList.Count; i++)
        {
            switch (doorPositionsList[i])
            {
                case DoorPositions.NORTH:
                    result.Add(new Vector3(-0.5f, 4.5f, 0));
                    break;
                case DoorPositions.SOUTH:
                    result.Add(new Vector3(-0.5f, -4.5f, 0));
                    break;
                case DoorPositions.EAST:
                    result.Add(new Vector3(9.5f, -0.5f, 0));
                    break;
                case DoorPositions.WEST:
                    result.Add(new Vector3(-9.5f, -0.5f, 0));
                    break;
                default:
                    Debug.LogError("There is no door in your room !");
                    return null;
            }
        }

        return result;
    }
}
