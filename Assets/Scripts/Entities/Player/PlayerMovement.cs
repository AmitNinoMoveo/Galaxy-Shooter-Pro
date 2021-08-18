using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerInput
{
    [SerializeField]
    public float _speedUpMultiplier;
    public float SpeedUpMultiplier
    {
        get => _speedUpMultiplier;
        private set => _speedUpMultiplier = value;
    }
    [SerializeField]
    private bool _isSpeedUp;
    public bool IsSpeedUp
    {
        get => _isSpeedUp;
        set => _isSpeedUp = value;
    }
    public override void Update()
    {
        base.Update();
        transform.Translate(Direction * calculateSpeed() * Time.deltaTime);
        wrapIfOutOfScreen();
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
