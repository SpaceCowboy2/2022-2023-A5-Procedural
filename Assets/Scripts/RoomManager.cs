using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public IReadOnlyList<RoomController> allRooms => Array.AsReadOnly(rooms);
    private RoomController[] rooms;

    private void Awake()
    {
        rooms = GetComponentsInChildren<RoomController>();
    }

    public RoomController GetRoomAtPosition(Vector2 position)
    {
        foreach (var room in rooms)
        {
            if (Vector2.Distance(room.transform.position, position) < Mathf.Epsilon)
                return room;
        }

        return null;
    }

    public RoomController[] GetNeighbours(RoomController roomToCheck)
    {
        var roomPosition = roomToCheck.transform.position;
        var potentialNeighbours = new List<RoomController>
        {
            rooms.FirstOrDefault(room =>
                Vector2.Distance(roomPosition - new Vector3(roomToCheck.roomSize.x / 2f + room.roomSize.x / 2f, 0, 0),
                    room.transform.position) < Mathf.Epsilon),
            rooms.FirstOrDefault(room =>
                Vector2.Distance(roomPosition + new Vector3(roomToCheck.roomSize.x / 2f + room.roomSize.x / 2f, 0, 0),
                    room.transform.position) < Mathf.Epsilon),
            rooms.FirstOrDefault(room =>
                Vector2.Distance(roomPosition - new Vector3(0, roomToCheck.roomSize.y / 2f + room.roomSize.y / 2f, 0),
                    room.transform.position) < Mathf.Epsilon),
            rooms.FirstOrDefault(room =>
                Vector2.Distance(roomPosition + new Vector3(0, roomToCheck.roomSize.y / 2f + room.roomSize.y / 2f, 0),
                    room.transform.position) < Mathf.Epsilon)
        };
        return potentialNeighbours.Where(neighbour => neighbour != null).ToArray();
    }
}
