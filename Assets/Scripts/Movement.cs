using System;
using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] 
    private float maxRadius = 0.5f;
    
    [SerializeField] private float speed = 1f;
    
    private Vector2 _originPoint;
    private Quaternion _rotation;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rotation = Quaternion.identity.normalized;
    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _rotation = quaternion.identity;
            return;
        }
        if (!Input.GetMouseButton(0))
            return;

        var current = (Vector2)(Input.mousePosition / Screen.height);
        if (Input.GetMouseButtonDown(0))
        {
            _originPoint = current;
            return;
        }

        _rotation = FindRotation(current);
    }

    private Quaternion FindRotation(Vector2 current)
    {
        var dis = current - _originPoint;
        var dir = dis.normalized;
        var mag = dis.magnitude;
        mag = Mathf.Clamp(mag, 0, maxRadius);

        var rot = mag * dir * Time.deltaTime * speed;

        return Quaternion.Euler(rot.y, 0, -rot.x).normalized;
    }

    private void FixedUpdate()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * _rotation);
    }
}
