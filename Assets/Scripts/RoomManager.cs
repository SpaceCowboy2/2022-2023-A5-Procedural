using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public IReadOnlyList<RoomController> allRooms => Array.AsReadOnly(rooms);
    private RoomController[] rooms;

    private void Awake()
    {
        rooms = GetComponentsInChildren<RoomController>();
    }

    public RoomController[] GetNeighbours(RoomController roomToCheck)
    {
        var potentialNeighbours = new List<RoomController>
        {
            rooms.FirstOrDefault(RoomInDirectionPredicate(roomToCheck, Direction.Down)),
            rooms.FirstOrDefault(RoomInDirectionPredicate(roomToCheck, Direction.Up)),
            rooms.FirstOrDefault(RoomInDirectionPredicate(roomToCheck, Direction.Left)),
            rooms.FirstOrDefault(RoomInDirectionPredicate(roomToCheck, Direction.Right)),
        };
        return potentialNeighbours.Where(neighbour => neighbour != null).ToArray();
    }

    private static Func<RoomController, bool> RoomInDirectionPredicate(RoomController roomToCheck, Direction direction)
    {
        Vector2 roomPosition = roomToCheck.transform.position;
        return room =>
        {
            var offset = direction switch
            {
                Direction.Up => new Vector2(0, roomToCheck.roomSize.y / 2f + room.roomSize.y / 2f),
                Direction.Down => new Vector2(0, roomToCheck.roomSize.y / 2f + room.roomSize.y / 2f) * -1f,
                Direction.Left => new Vector2(roomToCheck.roomSize.x / 2f + room.roomSize.x / 2f, 0) * -1f,
                Direction.Right => new Vector2(roomToCheck.roomSize.x / 2f + room.roomSize.x / 2f, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            return Vector2.Distance(roomPosition - offset, room.transform.position) < Mathf.Epsilon;
        };
    }
}
