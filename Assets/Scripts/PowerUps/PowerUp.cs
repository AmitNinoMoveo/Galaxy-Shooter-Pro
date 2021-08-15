﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // VARs
    [SerializeField]
    private float _speed = 3f;
    public float Speed
    {
        get => _speed;
    }
    [SerializeField]
    private int PowerUpId;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.name == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.applyPowerUp(PowerUpId);
                Destroy(this.gameObject);
            };
        }
    }
    void Update()
    {
       calculateMovement(); 
    }
    void calculateMovement()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        if(transform.position.y < (-1 * GlobalInfo.Instance.YScreenBorder))
        {
            Destroy(this.gameObject);
        };
    }
}
