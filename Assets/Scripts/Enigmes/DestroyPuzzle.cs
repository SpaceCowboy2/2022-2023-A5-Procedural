using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPuzzle : MonoBehaviour
{
    public GameObject LinkedCollider;
    private void OnDestroy()
    {
        Destroy(LinkedCollider);
    }
}
