using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubRoom : Room
{
    private List<DoorPositions> doors = new List<DoorPositions>();
    private List<Vector3> doorsPos= new List<Vector3>();
    public GameObject LockedDoor;
    public GameObject OpenDoor;
    
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
}
