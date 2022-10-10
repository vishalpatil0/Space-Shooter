using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _spee = 3.0f;
    //IDs for powerUP
    //0 = tripple shot
    //1 = speed
    //2 = shield
    [SerializeField]
    private int _PowerUPID;
    [SerializeField]
    private AudioClip _audioClip;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _spee * Time.deltaTime);

        if (transform.position.y < -3.8f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);f
            Debug.Log("Power up collected by player");
            
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                
                switch (_PowerUPID)
                {
                    case 0:
                        player.trippleShotActive();
                        break;
                    case 1:
                        player.speedPowerUpActive();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
