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
    private Text _gameOverText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    private void Start()
    {
        CurrentScoreText.text = "Score: " + 0;
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
        _gameOverText.gameObject.SetActive(true);
    }
}
