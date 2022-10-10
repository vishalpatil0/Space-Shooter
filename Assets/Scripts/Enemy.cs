using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player player;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private AudioSource _audioData;
    [SerializeField]
    private AudioClip _ExplosionSound;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.Log("Player object in Enemy.cs is null.");
        }
        _animator = GetComponent<Animator>();
        if( _animator == null)
        {
            Debug.Log("Animator object in Enemy.cs is null");
        }
        _boxCollider2D = GetComponent<BoxCollider2D>();
        if(_boxCollider2D == null)
        {
            Debug.Log("BoxCollider2D object in Enemy.cs is null.");
        }
        _audioData = GetComponent<AudioSource>();
        if(_audioData == null)
        {
            Debug.Log("AudioSource object in Enemy.cs is null.");
        }
        else
        {
            _audioData.clip = _ExplosionSound;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //move down at 4 meter per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if bottom of screen
        //respawn at top with random X position

        if (transform.position.y < -3.8f  && _boxCollider2D.enabled)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f), 8.0f, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        if(other.tag == "Player")
        {
            _audioData.Play();
            _boxCollider2D.enabled = false;
            _animator.SetTrigger("onEnemyDeath");
            if(player != null)
            {
                if(player.getShieldStatus())
                {
                    player.DeactivateShield();
                }
                else
                {
                    player.damage();
                    Debug.Log("Lives : " + player.getLives());
                }
            }
            Destroy(this.gameObject,2.5f);
        }

        if(other.tag == "Laser")
        {
            _boxCollider2D.enabled = false;
            _animator.SetTrigger("onEnemyDeath");
            if (player != null)
            {
                player.updateScore();
            }
            _audioData.Play();
            Destroy(other.gameObject);
            Destroy(this.gameObject,2.5f);
        }
    }
}
