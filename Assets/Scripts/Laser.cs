using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    private float _speed = 8.0f;
   
    // Update is called once per frame
    void Update()
    {
        //translate laser up infinitely
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if laser position is greater than 5 on the y then destory then object
        if(transform.position.y > 10f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
