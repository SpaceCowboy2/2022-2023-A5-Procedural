using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HubRoom : Room
{
    private List<DoorPositions> doors = new List<DoorPositions>();
    private List<Vector3> doorsPos= new List<Vector3>();
    public GameObject LockedDoor;
    public GameObject OpenDoor;
    [SerializeField] private Tilemap _tilemap = null;

    protected override void GenerateRoom()
    {
        
    }

    protected override void Randomize()
    {
        
    }

    private void Start()
    {
        doorsPos = GetDoorPositions(doors);
    }
    private void SetDoor() 
    {
        for (int i = 0; i < doors.Count; i++)
        {
            Instantiate(OpenDoor, doorsPos[i], Quaternion.identity);
        }
    
    }

    private void OnDrawGizmos()
    {
        Vector3Int gameobjectPos = new Vector3Int(_tilemap.size.x, _tilemap.size.y, 0);

        //Gizmos.DrawCube(transform.position, gameobjectPos);
    }
}
