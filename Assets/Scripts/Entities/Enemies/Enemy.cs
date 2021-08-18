using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Enemy : DamageDealing
{
    // Game OBJs
    private AudioSource _audioSource;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    //CORE Methods
    public override void Start()
    {
        base.Start();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player.GetType() == null) Debug.Log("Enemy Component::Player is null");
        _animator = GetComponent<Animator>();
        if (_animator.GetType() == null) Debug.Log("Enemy Component::Animator is null");
        _boxCollider = GetComponent<BoxCollider2D>();
        if (_boxCollider.GetType() == null) Debug.Log("Enemy Component::BoxCollider2D is null");
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource.GetType() == null) Debug.Log("Enemy Component::AudioSource is null");

        StartCoroutine(fireEveryFewSeconds());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == _player.transform.name)
        {
            _player.decreaseLife();
            decreaseLife(2);
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
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        wrapOnBottomScreen();
    }
    // Damage Dealing Methods
    IEnumerator fireEveryFewSeconds()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(FireRate);
        }
    }
    // Health
    void decreaseLife(int amount = 1)
    {
        decreseCurrentLife(amount);
        _audioSource.Play();
        if (!IsAlive) DestroyEnemy();
    }
    void DestroyEnemy()
    {
        Speed = 1f;
        _animator.SetTrigger("OnEnemyDeath");
        _boxCollider.enabled = false;
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
