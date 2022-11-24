using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    public List<GameObject> puzzleList = new List<GameObject>();
    public List<GameObject> decorList = new List<GameObject>();
    public GameObject enemyPrefab;

    public static ListManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
