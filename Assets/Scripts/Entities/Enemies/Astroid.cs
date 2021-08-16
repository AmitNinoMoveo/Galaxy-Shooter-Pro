using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 20f;
    public float RotationSpeed
    {
        get => _rotationSpeed;
    }
    [SerializeField]
    private float _movementSpeed = 1.8f;
    public float MovementSpeed
    {
        get => _movementSpeed;
    }
    void Start()
    {
        transform.position = new Vector3(0, GlobalInfo.Instance.YScreenBorder, 0);
    }
    void Update()
    {
        if (transform.position.y >= -2f)
        {
            transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime, Space.World);
        }
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
    }
}
