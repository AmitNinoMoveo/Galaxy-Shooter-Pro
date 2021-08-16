using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _currentScoreText;
    public Text CurrentScoreText
    {
        get => _currentScoreText;
        set
        {
            _currentScoreText = value;
        }
    }
    [SerializeField]
    private Text _gameOverTitle;
    [SerializeField]
    private Text _gameOverRestart;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    private GameManager _gameManager;
    private void Start()
    {
        CurrentScoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("UI Manager::GameManager component is null");
        }
    }
    public void changeScore(int newScoreAmount)
    {
        CurrentScoreText.text = "Score: " + newScoreAmount;
    }
    public void updateLivesSprite(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];
    }
    public void activateGameOver()
    {
        _gameOverTitle.gameObject.SetActive(true);
        _gameOverRestart.gameObject.SetActive(true);
        _gameManager.setGameOver();
    }
}
