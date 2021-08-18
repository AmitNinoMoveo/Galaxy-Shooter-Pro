using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : DamageDealing
{
    [SerializeField]
    private Vector3 _direction;
    public Vector3 Direction
    {
        get => _direction;
        private set => _direction = value;
    }
    [SerializeField]
    private bool _isSpacePressed;
    public bool isSpacePressed
    {
        get => _isSpacePressed;
        private set => _isSpacePressed = value;
    }
    public virtual void Update()
    {
        Direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        isSpacePressed = Input.GetKeyDown(KeyCode.Space);
    }
}
