using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    private RoomController[] neighbours;

    private RoomEnemiesManager enemiesManager;
    private LevelManager levelManager;

    public List<GameObject> objectToActiveOnFirstVisit = new List<GameObject>();
    [SerializeField]
    private List<GameObject> objectToApplyWallsOnFirstVisit = new List<GameObject>();

    [SerializeField]
    private bool skipFirstDraw = false;

    private bool visited = false;
    

    private void Awake()
    {
        enemiesManager = GetComponentInChildren<RoomEnemiesManager>();
        levelManager = GetComponentInParent<LevelManager>();
        
        if (objectToApplyWallsOnFirstVisit.Count > 0)
        {
            foreach (GameObject _object in objectToApplyWallsOnFirstVisit)
            {
                int childCount = _object.transform.childCount;
                if (childCount > 0)
                {
                    for (int i = 0; i < childCount; i++)
                    {
                        _object.transform.GetChild(i).gameObject.layer = 14;
                    }
                }
                _object.layer = 14;
            }
        }
    }

    private void Start()
    {
        neighbours = levelManager.GetNeighbours(this);
    }

    public void SetRoomActive(bool isActive)
    {
        Debug.Log(transform.name);
        enemiesManager.SetAllEnemiesInRoomActive(isActive);
    }

    public void DrawRoom()
    {
        if (!visited && !skipFirstDraw)
        {
            visited = true;
            if (objectToActiveOnFirstVisit.Count > 0)
            {
                foreach (GameObject _object in objectToActiveOnFirstVisit)
                {
                    _object.SetActive(true);
                }
            }

            if (objectToApplyWallsOnFirstVisit.Count > 0)
            {
                foreach (GameObject _object in objectToApplyWallsOnFirstVisit)
                {
                    int childCount = _object.transform.childCount;
                    if (childCount > 0)
                    {
                        for (int i = 0; i < childCount; i++)
                        {
                            _object.transform.GetChild(i).gameObject.layer = 7;
                        }
                    }
                    _object.layer = 7;
                }
            }

        }
        
        if (skipFirstDraw)
        {
            skipFirstDraw = false;
        }
    }
}
