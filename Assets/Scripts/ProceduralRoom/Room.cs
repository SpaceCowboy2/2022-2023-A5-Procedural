using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    protected abstract void GenerateRoom();

    protected abstract void Randomize();
}
