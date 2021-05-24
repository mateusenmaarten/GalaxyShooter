using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;
    
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private Vector3 _laserOffset = new Vector3(0, 0.8f,0);

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private bool _tripleShotActive;

    [SerializeField]
    private bool _speedBoostActive;

    [SerializeField]
    private bool _shieldBoostActive;

    [SerializeField]
    private float _speedMultiplier = 1.5f;

    [SerializeField]
    private GameObject _shieldSprite;

    [SerializeField]
    private int _score = 0;

    SpawnManager _spawnManager;
    UIManager _uiManager;

    void Start()
    {
        transform.position = new Vector3(0, -3);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI_Manager is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 0f));

        if (transform.position.x < -10.5f)
        {
            transform.position = new Vector3(10.5f, transform.position.y, 0);
        }
        else if (transform.position.x > 10.5)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, 0);
        }
    }

    void Shoot()
    {
        _canFire = Time.time + _fireRate; 

        if (_tripleShotActive)
        {
            Instantiate(_tripleShot, transform.position + _laserOffset, Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + _laserOffset, Quaternion.identity);
        }
    }

    public void DamagePlayer()
    {
        if (_shieldBoostActive)
        {
            _shieldBoostActive = false;
            _shieldSprite.SetActive(false);
        }
        else
        {
            _lives--;
            _uiManager.UdateLives(_lives);
        }

        if (_lives < 1)
        {
            if (_spawnManager != null)
            {
                _spawnManager.onPlayerDeath();
            }
            Destroy(this.gameObject);
        }
    }

    //Powerups
    public void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine(5));
    }

    IEnumerator TripleShotPowerDownRoutine(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _tripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _playerSpeed *= _speedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine(5));
    }

    IEnumerator SpeedPowerDownRoutine(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _speedBoostActive = false;
        _playerSpeed /= _speedMultiplier;

    }

    public void ShieldActive()
    {
        _shieldBoostActive = true;
        _shieldSprite.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdatePlayerScore(_score);
    }
}
