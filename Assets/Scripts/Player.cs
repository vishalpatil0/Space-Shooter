using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _spee = 9.0f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTrippleShotActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _ShieldVisualizer;
    [SerializeField]
    private GameObject _ThrusterVisualizer;
    [SerializeField]
    private GameObject _RightEngine, _LeftEngine;
    private AudioSource _audioData;
    [SerializeField]
    private AudioClip _laserAudioClip;
    [SerializeField]
    private int _score = 0;
    private UIManager _UIManager;
    
    void Start()
    {
        //take the current position = new position (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is null");
        }
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_UIManager == null)
        {
            Debug.LogError("UIManger object in Player.cs is null");
        }
        _audioData = GetComponent<AudioSource>();
        if(_audioData == null)
        {
            Debug.LogError("AudioSource object in Player.cs is null");
        }
        else
        {
            _audioData.clip = _laserAudioClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();


        //if i hit space button
        //spawn gameobject
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            fireLaser();
        }
    }

    void calculateMovement()
    {
        //transform.Translate(Vector3.right * Time.deltaTime);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _spee * Time.deltaTime);

        
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0.0f);
        }

        if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0.0f);
        }

        if(transform.position.y < -2.04567f)
        {
            transform.position = new Vector3(transform.position.x, -2.04567f, 0.0f);
        }

        if(transform.position.y > 7.471572f)
        {
            transform.position = new Vector3(transform.position.x, 7.471572f, 0.0f);
        }
    }

    void fireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTrippleShotActive)
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0.0f, 0.8f, 0.0f), Quaternion.identity);
        }
        _audioData.Play();

    }

    public void damage()
    {
        if(_lives > 1 && _LeftEngine.activeSelf == true || _RightEngine.activeSelf == true)
        {
            _RightEngine.SetActive(true);
            _LeftEngine.SetActive(true);
        }
        else if(_lives > 1)
        {
            int whichEngine = Random.Range(1, 3);
            if (whichEngine == 1)
            {
                _LeftEngine.SetActive(true);
            }
            else
            {
                _RightEngine.SetActive(true);
            }
        }
        
        _lives--;
        if(_UIManager != null)
        {
            _UIManager.updateLives(_lives);
        }
        //check if we are dead
        if (_lives < 1)
        {

            //Communicate with spawn manager and let
            //them know to stop spawning
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
            GameManager gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
            if(gameManager != null)
            {
                gameManager.GameOver();
            }
            _UIManager.gameOver();
            
        }
    }
    public int getLives()
    {
        return _lives;
    }

    public void trippleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTrippleShotActive = false;
    }

    public void speedPowerUpActive()
    {
        _ThrusterVisualizer.SetActive(true);
        _spee += 5.0f;
        StartCoroutine(SpeedPowerUpRoutine());
    }

    IEnumerator SpeedPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _spee -= 5.0f;
        _ThrusterVisualizer.SetActive(false);
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _ShieldVisualizer.SetActive(true);
    }

    public void DeactivateShield()
    {
        _isShieldActive = false;
        _ShieldVisualizer.SetActive(false);
    }

    public bool getShieldStatus()
    {
        return _isShieldActive;
    }

    public void updateScore()
    {
        _score += 10;
    }

    public int getScore()
    {
        return _score;
    }
}
 