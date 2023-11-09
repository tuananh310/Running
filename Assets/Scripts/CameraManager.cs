using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 _offset;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;
    
    void Awake()
    {
        _offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}
