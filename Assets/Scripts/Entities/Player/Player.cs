using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // OTHER COMPONENTS
    private GlobalInfo globalInfo = GlobalInfo.Instance;
    private Life _life;
    private UIManager _uiManager;
    // GAME OBJs
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldsAnimation;
    private SpawnManager _spawnManager;
    //  VARs
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _speedUpMultiplier = 1.3f;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private bool _isTripleShot = false;
    public bool IsTripleShot
    {
        get => _isTripleShot;
        set
        {
            _isTripleShot = value;
        }
    }
    [SerializeField]
    private bool _isSpeedUp = false;
    public bool IsSpeedUp
    {
        get => _isSpeedUp;
        set
        {
            _isSpeedUp = value;
        }
    }
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

    private float _cooldownTime;

    // Core Methods
    void Start()
    {
        _life = transform.GetComponent<Life>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Player Component: UI Manager is Null");
        }
        transform.position = new Vector3(0f, -1 * globalInfo.YScreenBorder + 1, 0f);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Player Component: SpawnManager is Null");
        }
    }
    void Update()
    {
        CalculateMovement();
        if (CheckIfFiring()) Fire();
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
    // Movement Methods
    void CalculateMovement()
    {
        ifKeyPressedMoveObject();
        wrapIfOutOfScreen();
    }
    void ifKeyPressedMoveObject()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (System.Convert.ToBoolean(horizontalInput) || System.Convert.ToBoolean(verticalInput))
        {
            Vector3 vector = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(vector * calculateSpeed() * Time.deltaTime);
        };
    }
    float calculateSpeed()
    {
        return IsSpeedUp ? _speed * _speedUpMultiplier : _speed;
    }
    void wrapIfOutOfScreen()
    {
        float yPosition = transform.position.y;
        float xPosition = transform.position.x;
        // Y Border Block
        transform.position = new Vector3(xPosition, Mathf.Clamp(yPosition, -1 * globalInfo.YScreenBorder, globalInfo.YScreenBorder), 0);
        // X Border Wrap
        if (xPosition >= (globalInfo.XScreenBorder + 2))
        {
            transform.position = new Vector3(-1 * (globalInfo.XScreenBorder + 1), yPosition, 0);
        }
        else if (xPosition <= -1 * (globalInfo.XScreenBorder + 2))
        {
            transform.position = new Vector3((globalInfo.XScreenBorder + 1), yPosition, 0);
        }
    }
    // Deal Damage Methods
    bool CheckIfFiring()
    {
        return Input.GetKeyDown(KeyCode.Space) && Time.time > _cooldownTime;
    }
    void Fire()
    {
        _cooldownTime = Time.time + _fireRate;
        if (IsTripleShot)
        {
            FireTripleShot();
            return;
        }
        else
        {
            FireDefaultProjectile();
        }
    }
    void FireDefaultProjectile()
    {
        Instantiate(_laserPrefab, transform.position, Quaternion.identity);
    }
    void FireTripleShot()
    {
        Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    }
    // Health Methods
    public void decreaseLife(int amount = 1)
    {
        if (IsShieldUp)
        {
            if (amount < ShieldCurrent)
            {
                ShieldCurrent -= amount;
                return;
            }
            shieldsUpDisable();
            if (amount == ShieldCurrent)
            {
                return;
            }
            else if (amount > ShieldCurrent)
            {
                amount -= ShieldCurrent;
            }
        }
        _life.decreseCurrentLife(amount);
        _uiManager.updateLivesSprite(_life.CurrentLife);
        if (!_life.IsAlive) onDeath();
    }
    private void onDeath()
    {
        _spawnManager.onPlayerDeath();
        _uiManager.activateGameOver();
        Destroy(this.gameObject);
    }
};