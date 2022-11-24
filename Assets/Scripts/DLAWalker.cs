using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DLAWalker : MonoBehaviour
{
    private Vector3 _position = Vector3.zero;
    private float _stepLength = 0f;

    private float _radius = 0f;
    private Vector3 _basePos;

    public List<Vector3> _positionHistory = new List<Vector3>();

    public DLAWalker(Vector3 basePos, float radius, float stepLength)
    {
        _position = basePos + RandomUnitDirection() * radius;
        _stepLength = stepLength;
        _radius = radius;
        _basePos = basePos;
    }

    public void Walk()
    {
        _positionHistory.Add(_position);
        Vector3 randomDir = RandomUnitDirection() * _stepLength;

        if (Vector3.Distance(_basePos, _position + randomDir) > _radius)
        {
            randomDir = (_basePos - _position).normalized;
        }

        _position += randomDir;
    }

    Vector3 RandomUnitDirection()
    {
        Vector2 unitDir2D = Random.insideUnitCircle.normalized;
        return new Vector3(unitDir2D.x, unitDir2D.y, 0);
    }

    public Vector3 GetPosition()
    {
        return _position;
    }

    public void Draw()
    {
        Vector3 lastPos = _positionHistory[0];
        for (int i = 1; i < _positionHistory.Count; i++)
        {
            Debug.DrawLine(_positionHistory[i], lastPos, Color.red, Single.MaxValue);
            lastPos = _positionHistory[i];
        }
    }
}