using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : GlobalInfo
{
    [SerializeField]
    private float _speed;
    public float Speed
    {
        get => _speed;
    }
    [SerializeField]
    private bool _isEnemy = false;
    public bool IsEnemy
    {
        get => _isEnemy;
        set => _isEnemy = value;
    }
    public Vector3 direction;
    private void Start()
    {
        direction = IsEnemy ? Vector3.down : Vector3.up;
    }
    void Update()
    {
        transform.Translate(direction * this.Speed * Time.deltaTime);
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
            Destroy(gameObject);
        }
    }
}
