using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    private Movement _movement;
    private Vector2 _prevDirection = Vector2.right;
    private Transform _child; 
    
    void Start()
    {
        _movement = GetComponent<Movement>();
        _child = gameObject.transform.GetChild(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _movement.SetDirection(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S))
            _movement.SetDirection(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A))
            _movement.SetDirection(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D))
            _movement.SetDirection(Vector2.right);
        
        // var angel = Mathf.Atan2(_movement.d)
    }

    private void FixedUpdate()
    {
        
    }

    private void SetRotateSprite(float angel)
    {
        _child.Rotate(0f , 0f ,angel);
    }
}

enum NextDirection {Same, Up, Down}
