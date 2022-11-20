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

    protected override void GenerateRoom()
    {

    }

    protected override void Randomize()
    {
        _nbEnemy = Random.Range(_nbMinEnemy, _nbMaxEnemy);
    }

    /*private Vector3 SetEnemySpawnPosition()
    {
        bool isGoodPosition = true;
        Vector3 spawnerPos;
        while(isGoodPosition)
        {
            spawnerPos = new Vector3(Random.Range(0, _tilemap.size.x), Random.Range(0, _tilemap.size.y), 0);

            for(int i = 0; i < _listDoorPositions.Count; i++)
            {
                if (Vector3.Distance(_listDoorPositions[i],spawnerPos) < 1)
                {
                    isGoodPosition = false;
                }
            }
            // Question : Comment get le tag ou le layer d'une tile (si c'est possible) ?
            if(_tilemap.GetTile(spawnerPos).)
        }
    }*/
}
