using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerInput
{
    [SerializeField]
    public float SpeedUpMultiplier { get; private set; }
    [SerializeField]
    public bool IsSpeedUp;
    private void Update()
    {
        ifKeyPressedMoveObject();
        wrapIfOutOfScreen();
    }
    void ifKeyPressedMoveObject()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (System.Convert.ToBoolean(horizontalInput) || System.Convert.ToBoolean(verticalInput))
        {
            Vector3 vector = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(vector * calculateSpeed() * Time.deltaTime);
        };
    }
    float calculateSpeed()
    {
        return IsSpeedUp ? Speed * SpeedUpMultiplier : Speed;
    }
    void wrapIfOutOfScreen()
    {
        float yPosition = transform.position.y;
        float xPosition = transform.position.x;
        // Y Border Block
        transform.position = new Vector3(xPosition, Mathf.Clamp(yPosition, -1 * YScreenBorder, YScreenBorder), 0);
        // X Border Wrap
        if (xPosition >= (XScreenBorder + 2))
        {
            transform.position = new Vector3(-1 * (XScreenBorder + 1), yPosition, 0);
        }
        else if (xPosition <= -1 * (XScreenBorder + 2))
        {
            transform.position = new Vector3((XScreenBorder + 1), yPosition, 0);
        }
    }
}
