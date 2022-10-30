using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float speed = 2f;
    [SerializeField] private  Vector2 initialDirection;
    
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private Vector2 _nextDirection;
    private Vector2 _startPosition;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPosition = this.transform.position;
        _nextDirection = Vector2.zero;
    }

    private void Start()
    {
        _rigidbody.isKinematic = false;
        _direction = initialDirection;
    }

    private void Update()
    {
        if(_nextDirection != Vector2.zero)
            SetDirection(_nextDirection);
    }

    private void FixedUpdate()
    {
        var translation = _direction * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + translation);
    }

    public void SetDirection(Vector2 direction)
    {
        if (!CheckDirection(direction))
        {
            this._direction = direction;
            _nextDirection = Vector2.zero;
        }
        else
        {
            _nextDirection = direction;
        }
    }

    private bool CheckDirection(Vector2 direction)
    {
        var hit = Physics2D.BoxCast(_rigidbody.position, Vector2.one * 0.75f, 0f, direction, 1.5f,obstacleLayer);
        return hit.collider != null;
    }
}
