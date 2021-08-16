using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile, _powerUp, _explosion;

    public GameObject Projectile
    {
        get => _projectile;
    }
    public GameObject PowerUp
    {
        get => _powerUp;
    }
    public GameObject Explosion
    {
        get => _explosion;
    }
}
