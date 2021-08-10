using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    //comment
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    private void moveObject()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (System.Convert.ToBoolean(horizontalInput) || System.Convert.ToBoolean(verticalInput))
        {
            UnityEngine.Vector3 moveVector = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(moveVector * _speed * Time.deltaTime);
        };
    }
    void Update()
    {
        moveObject();
    }
};
