using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private Transform[] _detectionPoints;
    [SerializeField] private float _detectionLength = 0.1f;
    [SerializeField] private LayerMask _groundLayerMask;

    public bool DetectGroundNearBy()
    {
        foreach (Transform detectionPoint in _detectionPoints)
        {
            RaycastHit2D hitresult = Physics2D.Raycast(
                detectionPoint.position,
                Vector2.down,
                _detectionLength,
                _groundLayerMask
                );

            if (hitresult.collider != null)
            {
                return true;
            }
        }
        return false;
    }
}