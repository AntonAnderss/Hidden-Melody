using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Player variables 
    public float PlayerSpeed = 5f;
    public float PlayerJumpForce = 5f;
    public float gravityForce = 9.81f;


    void Start()
    {

    }



    void Update()
    {



    //Movement Left RIght and jump
    if (Input.GetKey(KeyCode.A))
    {
        transform.position += Vector3.left * PlayerSpeed * Time.deltaTime;
    }

    if (Input.GetKey(KeyCode.D))
    {
        transform.position += Vector3.right * PlayerSpeed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.Space))
    {
        transform.position += Vector3.up * PlayerJumpForce * Time.deltaTime;
    }
        
        

    }
}
