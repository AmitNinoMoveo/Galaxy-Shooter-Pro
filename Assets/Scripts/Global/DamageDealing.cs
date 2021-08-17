using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealing : Entities
{
    [SerializeField]
    private GameObject _laserPrefab;
    public GameObject LaserPrefab
    {
        get => _laserPrefab;
    }
    [SerializeField]
    private GameObject _tripleShotPrefab;
    public GameObject TripleShotPrefab
    {
        get => _tripleShotPrefab;
    }
    [SerializeField]
    private float _fireRate;
    public float FireRate
    {
        get => _fireRate;
    }
    private float _cooldownTime;
    public float CooldownTime
    {
        get => _cooldownTime;
        set
        {
            if (value > 0)
            {
                _cooldownTime = value;
            }
        }
    }
    private bool _isTripleShot = false;
    public bool IsTripleShot
    {
        get => _isTripleShot;
        set
        {
            _isTripleShot = value;
        }
    }
    public void Fire()
    {
        if (IsTripleShot)
        {
            FireTripleShot();
        }
        else
        {
            FireDefaultProjectile();
        }
        CooldownTime = Time.time + _fireRate;
    }
    void FireDefaultProjectile()
    {
        Instantiate(_laserPrefab, transform.position, Quaternion.identity);
    }
    void FireTripleShot()
    {
        Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    }
}
