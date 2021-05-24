using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Sprite[] _lives;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    // Update is called once per frame
    public void UpdatePlayerScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UdateLives(int currentLives)
    {
        _livesImage.sprite = _lives[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.text = "Game Over !";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
