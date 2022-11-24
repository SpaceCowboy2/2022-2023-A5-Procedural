using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyTarget : MonoBehaviour
{
    public UnityEvent DestroySucced;

    private void OnDestroy()
    {
        DestroySucced.Invoke();
    }

}
