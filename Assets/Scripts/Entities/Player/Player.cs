using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerMovement
{
    // OTHER COMPONENTS
    private UIManager _uiManager;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _projectileAudioClip;
    [SerializeField]
    private AudioClip _powerupAudioClip;
    [SerializeField]
    private AudioClip _explosionAudioClip;
    // GAME OBJs
    [SerializeField]
    private GameObject _shieldsAnimation;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject[] _playerHurtAnimations;
    //  VARs

    [SerializeField]
    private bool _isShieldUp = false;
    public bool IsShieldUp
    {
        get => _isShieldUp;
        set
        {
            _isShieldUp = value;
        }
    }
    [SerializeField]
    private int _shieldMax = 1;
    public int ShieldMax
    {
        get => _shieldMax;
    }
    [SerializeField]
    private int _shieldCurrent;
    public int ShieldCurrent
    {
        get => _shieldCurrent;
        set
        {
            if (value >= ShieldMax)
            {
                _shieldCurrent = ShieldMax;
                return;
            }
            else if (value < 0)
            {
                _shieldCurrent = 0;
                return;
            }
            else
            {
                _shieldCurrent = value;
            }
        }
    }
    [SerializeField]
    private int _score = 0;
    public int Score
    {
        get => _score;
        set
        {
            if (value < 0) return;
            _score = value;
        }
    }

    // Core Methods
    void Start()
    {
        transform.position = new Vector3(0f, -1 * YScreenBorder + 1, 0f);
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("Player::Audio Source is null");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) Debug.LogError("Player::UI Manager is null");
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("Player::SpawnManager is null");
    }
    void Update()
    {
        if (CheckIfFiring())
        {
            Fire();
            setPlayAudioClip(_projectileAudioClip);
        }
    }
    IEnumerator setPowerUpTimer(System.Action func, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        func();
    }
    public void addScore(int points = 10)
    {
        Score += points;
        _uiManager.changeScore(Score);
    }
    private void setPlayAudioClip(AudioClip _audioClip)
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
    // PowerUp Methods
    public void applyPowerUp(int id)
    // PowerUp ids are: 0 = Triple Shot, 1: Speed up, 2: Shields
    {
        switch (id)
        {
            case 0:
                setTripleShot();
                break;
            case 1:
                setSpeedUp();
                break;
            case 2:
                setShieldUp();
                break;
            default:
                break;
        }
        setPlayAudioClip(_powerupAudioClip);
    }
    void setSpeedUp()
    {
        IsSpeedUp = true;
        StartCoroutine(
            setPowerUpTimer(
            delegate () { IsSpeedUp = false; },
            5f)
        );
    }
    void setTripleShot()
    {
        IsTripleShot = true;
        StartCoroutine(setPowerUpTimer(
            delegate () { IsTripleShot = false; },
            5f));
    }
    private void setShieldUp()
    {
        if (IsShieldUp) return;
        ShieldCurrent = ShieldMax;
        IsShieldUp = true;
        _shieldsAnimation.SetActive(true);
    }
    private void shieldsUpDisable()
    {
        IsShieldUp = false;
        _shieldsAnimation.SetActive(false);
    }
    // Deal Damage Methods
    bool CheckIfFiring()
    {
        return Input.GetKeyDown(KeyCode.Space) && Time.time > CooldownTime;
    }
    // Health Methods
    public int calculateShields(int amount = 1)
    {
        if (amount < ShieldCurrent)
        {
            ShieldCurrent -= amount;
            return 0;
        }
        shieldsUpDisable();
        if (amount == ShieldCurrent) return 0;
        if (amount > ShieldCurrent)
        {
            amount -= ShieldCurrent;
            return amount;
        }
        else return 0;
    }
    public void decreaseLife(int amount = 1)
    {
        if (IsShieldUp)
        {
            addScore(Random.Range(20, 27));
            amount = calculateShields(amount);

        }
        decreseCurrentLife(amount);
        _uiManager.updateLivesSprite(CurrentLife);
        managePlayerHurtAnimations();
        if (!IsAlive) onDeath();
    }
    private void onDeath()
    {
        _spawnManager.onPlayerDeath();
        _uiManager.activateGameOver();
        setPlayAudioClip(_explosionAudioClip);
        Destroy(this.gameObject);
    }
    private void managePlayerHurtAnimations()
    {
        int randomIndex = Random.Range(0, 2);
        if (CurrentLife == 2)
        {
            _playerHurtAnimations[randomIndex].SetActive(true);
            return;
        }
        if (CurrentLife == 1)
        {
            if (checkIfPlayerAnimationActive())
            {
                _playerHurtAnimations[1].SetActive(true);
                return;
            }
            else
            {
                _playerHurtAnimations[0].SetActive(true);
            }
        }
    }
    private bool checkIfPlayerAnimationActive()
    {
        return _playerHurtAnimations[0].activeInHierarchy == true;
    }
};