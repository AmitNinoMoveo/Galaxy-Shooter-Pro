using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entities
{
    // Game OBJs
    private AudioSource _audioSource;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    // VARs
    [SerializeField]
    private float _speed = 4f;
    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0) return;
            _speed = value;
        }
    }
    //CORE Methods
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player.GetType() == null) Debug.Log("Enemy Component::Player is null");
        _animator = GetComponent<Animator>();
        if (_animator.GetType() == null) Debug.Log("Enemy Component::Animator is null");
        _boxCollider = GetComponent<BoxCollider2D>();
        if (_animator.GetType() == null) Debug.Log("Enemy Component::BoxCollider2D is null");
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource.GetType() == null) Debug.Log("Enemy Component::AudioSource is null");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == _player.transform.name)
        {
            if (_player.IsShieldUp)
            {
                decreaseLife(2);
            }
            else
            {
                decreaseLife(2);
            }
            _player.decreaseLife();
            return;
        }
        else if (other.tag == "Projectiles")
        {
            _player.addScore(Random.Range(8, 15));
            decreaseLife();
            Destroy(other.gameObject);
        };

    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        wrapOnBottomScreen();
    }
    // Damage Dealing Methods
    IEnumerator fireEveryFewSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(FireRate);
            Fire();
        }
    }
    // Health
    void decreaseLife(int amount = 1)
    {
        decreseCurrentLife(amount);
        if (!IsAlive) DestroyEnemy();
    }
    void DestroyEnemy()
    {
        Speed = 1f;
        _animator.SetTrigger("OnEnemyDeath");
        _boxCollider.enabled = false;
        _audioSource.Play();
        Destroy(this.gameObject, 2.8f);
    }
    //Movement Methods
    void wrapOnBottomScreen()
    {
        if (transform.position.y < -7f)
        {
            transform.position = getRandomVectorEnemy();
        }
    }
}
