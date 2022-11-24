using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClassicRoom : Room
{
    private int _nbMinEnemy = 1, _nbMaxEnemy = 5;
    private int _nbMinDecor = 0, _nbMaxDecor = 3;

    private RoomController _controller;

    [SerializeField] private Tilemap _tilemap = null;
    [SerializeField] private TileBase _wallTile = null;
    [SerializeField] private GameObject _enemyParent = null;

    private void Start()
    {
        _controller = GetComponent<RoomController>();
        InstantiateObjectsAtPos(transform.position, GetPuzzleList());
        InstantiateObjectsAtPos(transform.position, GetDecorList());
        InstantiateObjectsAtPos(transform.position, GetEnemyList(), true);

        if (_controller.objectToActiveOnFirstVisit.Count > 0)
        {
            foreach (GameObject _object in _controller.objectToActiveOnFirstVisit)
            {
                _object.SetActive(false);
            }
        }
    }

    protected override void GenerateRoom()
    {


    }

    protected override void Randomize()
    {
        
    }

    private List<GameObject> GetPuzzleList()
    {
        int index = Random.Range(0, ListManager.Instance.puzzleList.Count);
        List<GameObject> puzzleList = new List<GameObject>();
        puzzleList.Add(ListManager.Instance.puzzleList[index]);

        return puzzleList;
    }

    private List<GameObject> GetDecorList()
    {
        int nbrDecor = Random.Range(_nbMinDecor, _nbMaxDecor);
        List<GameObject> decorList = new List<GameObject>();
        for (int i = 0; i < nbrDecor; i++)
        {
            int index = Random.Range(0, ListManager.Instance.decorList.Count);
            decorList.Add(ListManager.Instance.decorList[index]);
        }
        return decorList;
    }

    private List<GameObject> GetEnemyList()
    {
        int nbrEnemy = Random.Range(_nbMaxEnemy, _nbMinEnemy);
        List<GameObject> enemyList = new List<GameObject>();
        for (int i = 0; i < nbrEnemy; i++)
        {
            enemyList.Add(ListManager.Instance.enemyPrefab);
        }
        return enemyList;
    }

    public void InstantiateObjectsAtPos(Vector3 roomPos, List<GameObject> objectsToInstantiate, bool isEnemy = false)
    {
        Vector3Int tilePosition = _tilemap.WorldToCell(roomPos);

        for (int i = 0; i < objectsToInstantiate.Count; i++)
        {
            Vector3Int gameobjectPos = new Vector3Int(Random.Range(-(_tilemap.size.x / 2) + tilePosition.x + 1, (_tilemap.size.x / 2) + tilePosition.x), 
                Random.Range(-(_tilemap.size.y / 2) + tilePosition.y + 1, (_tilemap.size.y / 2) + tilePosition.y));
            
            //Si une door se trouve à côté
            while (CheckBoundaries(gameobjectPos, 2))
            {
                gameobjectPos = new Vector3Int(Random.Range(-(_tilemap.size.x / 2) + tilePosition.x + 1, (_tilemap.size.x / 2) + tilePosition.x), 
                    Random.Range(-(_tilemap.size.y / 2) + tilePosition.y + 1, (_tilemap.size.y / 2) + tilePosition.y));
            }

            if (!isEnemy)
                Instantiate(objectsToInstantiate[i], _tilemap.CellToWorld(gameobjectPos), Quaternion.identity);
            else
            {
                Instantiate(objectsToInstantiate[i], _tilemap.CellToWorld(gameobjectPos), Quaternion.identity, _enemyParent.transform);
               
            }
        }
    }

    public bool CheckBoundaries(Vector3 pos, int radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, radius);

        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.CompareTag("Door") || hit.gameObject.CompareTag("Puzzle") || hit.gameObject.CompareTag("Decor") || hit.gameObject.CompareTag("Enemy") || hit.gameObject.CompareTag("Props"))
                return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3Int gameobjectPos = new Vector3Int(_tilemap.size.x, _tilemap.size.y, 0);

        //Gizmos.DrawCube(transform.position, gameobjectPos);
    }
}
