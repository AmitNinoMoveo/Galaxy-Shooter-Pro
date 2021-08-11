using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 10f;
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        destroyIfOutOfScreen();
    }
    void destroyIfOutOfScreen()
    {
        if (transform.position.y >= 6f)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
