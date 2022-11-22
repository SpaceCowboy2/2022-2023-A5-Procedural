using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ClassicRoom : Room
{
    private int _nbEnemy = 0;
    private int _nbMinEnemy = 1, _nbMaxEnemy = 11;
    private GameObject[] _arrayEnemy = null;
    [SerializeField] private GameObject _enemyPrefab = null;

    private Vector3[] _arraySpawnPosEnemy = null;
    private List<Vector3> _listDoorPositions;

    [SerializeField] private Tilemap _tilemap = null;
    [SerializeField] private TileBase _tileBase = null;

    private void Start()
    {
        Vector3Int spawnerPos = new Vector3Int(0, 0, 0);
        Debug.Log(spawnerPos);
        _tilemap.SetTile(spawnerPos, _tileBase);
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        SetPuzzle();
    }
    protected override void GenerateRoom()
    {


    }

    protected override void Randomize()
    {
        _nbEnemy = Random.Range(_nbMinEnemy, _nbMaxEnemy);
    }

    public void SetPuzzle()
    {
        int index = Random.Range(0, PuzzleManager.Instance.puzzleList.Count);
        List<GameObject> listPuzzle = PuzzleManager.Instance.puzzleList[index].gameobjectsToInstantiate;

        for (int i = 0; i < listPuzzle.Count; i++)
        {
            Vector3 gameobjectPos = new Vector3(Random.Range(-(_tilemap.size.x / 2), _tilemap.size.x / 2), Random.Range(-(_tilemap.size.y / 2), _tilemap.size.y / 2));

            //Si une door se trouve à côté
            while (CheckBoundaries(gameobjectPos, 2))
            {
                gameobjectPos = new Vector3(Random.Range(-(_tilemap.size.x / 2), _tilemap.size.x / 2), Random.Range(-(_tilemap.size.y / 2), _tilemap.size.y / 2));
            }

            Instantiate(listPuzzle[i], gameobjectPos, Quaternion.identity);
        }
    }

    public bool CheckBoundaries(Vector3 pos, int radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, radius);

        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.CompareTag("Door") || hit.gameObject.CompareTag("Puzzle"))
            {
                return true;
            }
        }

        return false;
    }

}
