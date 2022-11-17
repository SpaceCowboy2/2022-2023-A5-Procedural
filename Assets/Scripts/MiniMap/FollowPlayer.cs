using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject Player = null;
    [SerializeField]
    [Range(40.0f, 80.0f)]
    private float DistanceMap = 80f;

    private Vector3 pos;

    private void Update()
    {
        if (Player == null)
            return;
        pos.x = Player.transform.position.x;
        pos.y = Player.transform.position.y;
        pos.z = -DistanceMap;
        transform.position = pos;
    }
}
