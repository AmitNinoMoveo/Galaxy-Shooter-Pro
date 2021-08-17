using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : GlobalInfo
{
    [SerializeField]
    private float _speed = 10f;
    public float Speed
    {
        get => _speed;
    }
    void Update()
    {
        transform.Translate(Vector3.up * this.Speed * Time.deltaTime);
        destroyIfOutOfScreen();
    }
    void destroyIfOutOfScreen()
    {
        if (transform.position.y >= YScreenBorder || transform.position.y <= -1 * YScreenBorder)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            DestroyImmediate(this.gameObject);
        }
    }
}
