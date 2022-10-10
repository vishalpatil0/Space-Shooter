using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    //public / private reference
    //data types -> {int, float, bool, string}
    // Start is called before the first frame update

    //speed variable
    [SerializeField]
    private float _speed = 2.5f;

    void Start()
    {
        //take the current position = new position (0,0,0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * Time.deltaTime);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //to move 5 frame per second
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //Below one line solution for above 2 lines
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
    }
}
