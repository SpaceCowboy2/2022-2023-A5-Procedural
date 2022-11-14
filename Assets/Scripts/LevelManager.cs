using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public IReadOnlyList<RoomController> allRooms => Array.AsReadOnly(rooms);
    private RoomController activeRoom;
    private CinemachineBrain cinemachineBrain;
    private RoomController[] rooms;

    private void Awake()
    {
        rooms = GetComponentsInChildren<RoomController>();
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
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
        Vector2 roomToCheckPosition = roomToCheck.transform.position;
        return eachRoom =>
        {
            var offset = direction switch
            {
                Direction.Up => new Vector2(0, roomToCheck.roomSize.y / 2f + eachRoom.roomSize.y / 2f),
                Direction.Down => new Vector2(0, roomToCheck.roomSize.y / 2f + eachRoom.roomSize.y / 2f) * -1f,
                Direction.Left => new Vector2(roomToCheck.roomSize.x / 2f + eachRoom.roomSize.x / 2f, 0) * -1f,
                Direction.Right => new Vector2(roomToCheck.roomSize.x / 2f + eachRoom.roomSize.x / 2f, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            return Vector2.Distance(roomToCheckPosition - offset, eachRoom.transform.position) < Mathf.Epsilon;
        };
    }

    private void SetActiveRoom(RoomController newActiveRoom)
    {
        if (activeRoom != null)
            activeRoom.SetRoomActive(false);
        newActiveRoom.SetRoomActive(true);
        activeRoom = newActiveRoom;
    }

    public async void OnCameraBlend(ICinemachineCamera newCam, ICinemachineCamera oldCam)
    {
        if (cinemachineBrain.IsBlending)
        {
            var delay = Mathf.RoundToInt(cinemachineBrain.ActiveBlend.Duration * 1000);
            await Task.Delay(delay);
        }

        SetActiveRoom(newCam.VirtualCameraGameObject.GetComponentInParent<RoomController>());
    }
}
