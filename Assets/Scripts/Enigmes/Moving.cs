using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Moving : MonoBehaviour
{
    public Vector3 InitPos;
    public UnityEvent PuzzleResolved;

    private void Start()
    {
        InitPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Resolved");
            PuzzleResolved.Invoke();
        }
    }

    public void Reset()
    {
        transform.position = InitPos;
    }
}
