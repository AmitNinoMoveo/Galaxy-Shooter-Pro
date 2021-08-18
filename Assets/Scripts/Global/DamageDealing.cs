using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Laser))]
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
        set => _cooldownTime = value > 0 ? value : 0;
    }
    [SerializeField]
    private bool _isTripleShot;
    public bool IsTripleShot
    {
        get => _isTripleShot;
        set => _isTripleShot = value;
    }
    private bool _isEnemy;
    public virtual void Start()
    {
        _isEnemy = tag == "Enemy";
    }
    public void Fire()
    {
        if (!IsAlive) return;
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
        Vector3 v = transform.position;
        if (_isEnemy)
        {
            v += new Vector3(-0.25f, -2f, 0);
        }
        else
        {
            v += new Vector3(0, 1f, 0);
        }
        GameObject laserObj = Instantiate(_laserPrefab, v, Quaternion.identity);
        Laser laser = laserObj.GetComponent<Laser>();
        laser.IsEnemy = _isEnemy;
    }
    void FireTripleShot()
    {
        Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    }
}
