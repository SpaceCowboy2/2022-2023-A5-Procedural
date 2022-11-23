using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeButton : MonoBehaviour
{
    public UnityEvent buttonPush;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buttonPush.Invoke();
        }
    }
}
