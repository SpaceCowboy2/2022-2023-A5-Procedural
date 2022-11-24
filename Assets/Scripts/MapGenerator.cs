using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Object = System.Object;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public enum DoorPos
{
    Top,
    Bottom,
    Left,
    Right
}

public class MapGenerator : MonoBehaviour
{
    public Transform roomParent;

    public GameObject roomPrefab;
    public GameObject mirrorRoomPrefab;
    public GameObject secretRoomPrefab;
    public GameObject hubPrefab;

    [Header("DLA")] public float DLASpawnRadius;
    public float DLAWalkerStep;
    public float AggregationDistance;
    public float MaxRoomCount;

    private List<Vector3> _createdRoomPositions = new List<Vector3>();
    private Vector2 roomDimensions;

    private List<DLARoom> rooms = new List<DLARoom>();
    private List<DLARoom> mirrorRooms = new List<DLARoom>();

    void Start()
    {
        roomDimensions = new Vector3(20, 10, 0);
        mirrorRooms.Add(new DLARoom(Vector2Int.zero));

        RandomWalk();
        GenerateRooms();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    AddRoom(Camera.main.ScreenToWorldPoint(Input.mousePosition), firstRoom.position);
        //    Debug.DrawLine(transform.position, _createdRoomPositions[count], Color.green, Single.MaxValue);
        //    Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.blue, Single.MaxValue);
        //    Debug.Log(count);
        //    count++;
        //}



        //if (Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log(deadWalkers.Count);
        //    foreach (var walker in deadWalkers)
        //    {
        //        if (walker._positionHistory.Count > 5)   
        //            walker.Draw();
        //    }
        //}
    }

    void GenerateRooms()
    {
        int secretRoomIndex = Random.Range(1, _createdRoomPositions.Count + 1);

        for (int i = 0; i < _createdRoomPositions.Count; i++)
        {
            var room = Instantiate(roomPrefab, _createdRoomPositions[i], Quaternion.identity, roomParent);
            rooms[i].SetObject(room);



            Vector3 mirrorPos = new Vector3(-_createdRoomPositions[i].x, _createdRoomPositions[i].y, 0);
            var mirrorRoomObject = Instantiate(mirrorRoomPrefab, mirrorPos, Quaternion.identity, roomParent);

            DLARoom mirrorRoom = new DLARoom(Vector2Int.zero);
            mirrorRoom.SetObject(mirrorRoomObject);
            mirrorRoom.SetMirrorDoors(rooms[i].GetDoors());
            mirrorRooms.Add(mirrorRoom);

        }
    }

    void RandomWalk()
    {
        GameObject roomObject = Instantiate(hubPrefab, transform.position, Quaternion.identity, roomParent);
        Vector3 previousPos = transform.position;
        DLARoom previousRoom = new DLARoom(new Vector2Int(-1, 0));
        previousRoom.SetObject(roomObject);
        rooms.Add(previousRoom);

        for (int i = 0; i < MaxRoomCount; i++)
        {
            Vector2 randomWorldDir = Random.insideUnitCircle;
            Vector2Int randomDir;
            if (i == 0)
            {
                randomDir = new Vector2Int(1, 0);
            }
            else
            {
                randomDir = new Vector2Int(MathF.Sign(randomWorldDir.x) > 0 ? 1 : 0, MathF.Sign(randomWorldDir.y));
            }
            if (randomDir.x == 1) randomDir.y = 0;

            Vector3 roomWorldPos = previousPos + new Vector3(roomDimensions.x * randomDir.x, roomDimensions.y * randomDir.y);
            _createdRoomPositions.Add(roomWorldPos);

            previousPos = roomWorldPos;

            DLARoom room = new DLARoom(randomDir);
            rooms.Add(room);
            previousRoom?.UpdateDoors(randomDir);
            previousRoom = room;
        }
    }

    void DLA()
    {
        Transform firstRoom = Instantiate(roomPrefab, transform.position, Quaternion.identity, roomParent).transform;
        roomDimensions = new Vector2(firstRoom.localScale.x, firstRoom.localScale.y);

        _createdRoomPositions.Add(firstRoom.position);

        List<DLAWalker> walkers = new List<DLAWalker>();

        int steps = 0;
        while (_createdRoomPositions.Count < MaxRoomCount && steps < 100000)
        {
            steps++;

            DLAWalker newWalker = new DLAWalker(transform.position, DLASpawnRadius, DLAWalkerStep);
            walkers.Add(newWalker);

            int i = 0;
            while (i < walkers.Count)
            {
                i = ManageWalkers(walkers, i);
            }
        }
    }

    Vector2Int GetGridCoordsFromWorldPos(Vector3 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x / roomDimensions.x), Mathf.RoundToInt(pos.y / roomDimensions.y));
    }

    int ManageWalkers(List<DLAWalker> walkers, int step = 0)
    {
        for (int i = step; i < walkers.Count; i++)
        {
            DLAWalker walker = walkers[i];
            walker.Walk();
            Vector3 walkerPos = walker.GetPosition();

            if (Vector3.Distance(walkerPos, transform.position) > DLASpawnRadius)
            {
                walkers.Remove(walker);
                //deadWalkers.Add(walker);
                return i;
            }

            Vector3 closestRoomPos;
            if (Aggregates(walkerPos, out closestRoomPos))
            {
                AddRoom(walkerPos, closestRoomPos);
                walkers.Remove(walker);
                return i;
            }
        }

        return walkers.Count;
    }

    void AddRoom(Vector3 walkerPos, Vector3 closestRoomPos)
    {
        Vector3 newRoomPos = Vector3.zero;
        if (Mathf.Abs(walkerPos.x) / roomDimensions.x > Mathf.Abs(walkerPos.y) / roomDimensions.y)
        {
            newRoomPos.x = closestRoomPos.x + Mathf.Sign((walkerPos - closestRoomPos).x) * roomDimensions.x;
        }
        else
        {
            newRoomPos.y = closestRoomPos.y + Mathf.Sign((walkerPos - closestRoomPos).y) * roomDimensions.y;
        }

        Vector2Int gridCoords = GetGridCoordsFromWorldPos(newRoomPos);
        //grid[gridCoords.y][gridCoords.x] = 1;

        _createdRoomPositions.Add(newRoomPos);
    }

    bool Aggregates(Vector3 walkerPos, out Vector3 closestRoomPos)
    {
        for (int i = 0; i < _createdRoomPositions.Count; i++)
        {
            if (Vector3.Distance(walkerPos, _createdRoomPositions[i]) < AggregationDistance)
            {
                closestRoomPos = _createdRoomPositions[i];
                return true;
            }
        }

        closestRoomPos = Vector3.zero;
        return false;
    }
}

