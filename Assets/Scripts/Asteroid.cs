using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _RotateSpeed = 5.0f;
    [SerializeField]
    private float _speed = 3.0f;
    private Player _player;
    private CircleCollider2D _circleCollider2D;
    private Animator _animator;
    private AudioSource _audioData;
    [SerializeField]
    private AudioClip _ExplosionSound;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player object in Asteroid.cs is null.");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("Animator object in Enemy.cs is null");
        }
        _circleCollider2D = GetComponent<CircleCollider2D>();
        if (_circleCollider2D == null)
        {
            Debug.Log("BoxCollider2D object in Asteroid.cs is null.");
        }
        _audioData = GetComponent<AudioSource>();
        if (_audioData == null)
        {
            Debug.Log("AudioSource object in Asteroid.cs is null.");
        }
        else
        {
            _audioData.clip = _ExplosionSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _RotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 15.0f, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _audioData.Play();
            _animator.SetTrigger("onEnemyDeath");
            if (_player != null)
            {
                if (_player.getShieldStatus())
                {
                    _player.DeactivateShield();
                }
                else
                {
                    _player.damage();
                    Debug.Log("Lives : " + _player.getLives());
                }
            }
            Destroy(this.gameObject, 2.5f);
        }

        if (other.tag == "Laser")
        {
            _circleCollider2D.enabled = false;
            _animator.SetTrigger("onEnemyDeath");
            _audioData.Play();
            if (_player != null)
            {
                _player.updateScore();
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.5f);
        }
    }
}
