using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Puzzle
{
    public string PuzzleType;
    public List<GameObject> gameobjectsToInstantiate = new List<GameObject>();
}
