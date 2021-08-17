using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : DamageDealing
{
    [SerializeField]
    public Vector3 Direction;
    [SerializeField]
    public bool isSpacePressed;
    private void Update()
    {
        Direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        isSpacePressed = Input.GetKeyDown(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("PlayerInput::Space Pressed");
        }
    }
}
